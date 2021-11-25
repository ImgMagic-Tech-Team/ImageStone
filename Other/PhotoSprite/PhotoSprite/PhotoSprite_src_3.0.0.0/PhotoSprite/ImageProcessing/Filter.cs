using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 滤波器类，非滤镜类
  /// </summary>
  public class Filter : ImageInfo
  {
    /// <summary>
    /// 滤波方法
    /// </summary>
    public enum FilteringMethod
    {
      /// <summary>
      /// 均值滤波
      /// </summary>
      Mean,

      /// <summary>
      /// 中值滤波
      /// </summary>
      Median,

      /// <summary>
      /// 最大值滤波
      /// </summary>
      Maximum,

      /// <summary>
      /// 最小值滤波
      /// </summary>
      Minimum
    } // FilteringMethod


    /// <summary>
    /// 统计一个数字序列并返回一个特殊值
    /// </summary>
    /// <param name="sequence">数字序列</param>
    /// <param name="filteringMethod">滤波方法</param>
    /// <returns></returns>
    public int Count(int[] sequence, FilteringMethod filteringMethod)
    {
      int count = 0;
      int len = sequence.Length;

      switch (filteringMethod)
      {
        case FilteringMethod.Mean:
          int sum = 0;
          for (int i = 0; i < len; i++)
          {
            sum += sequence[i];
          } // i
          count = sum / len;
          break;

        case FilteringMethod.Median:
          bool isMovable = true;
          int t = 0;
          while (isMovable)
          {
            isMovable = false;
            for (int i = 1; i < len; i++)
            {
              if (sequence[i - 1] > sequence[i])
              {
                // 交换 sequence[i-1], sequence[i]
                t = sequence[i - 1];
                sequence[i - 1] = sequence[i];
                sequence[i] = t;

                isMovable = true;
              }
            } // i
          } // isMovable

          // 取中值
          count = sequence[len / 2];
          break;

        case FilteringMethod.Maximum:
          int max = sequence[0];
          for (int i = 1; i < len; i++)
          {
            if (sequence[i] > max)
              max = sequence[i];
          } // i
          count = max;
          break;

        case FilteringMethod.Minimum:
          int min = sequence[0];
          for (int i = 1; i < len; i++)
          {
            if (sequence[i] < min)
              min = sequence[i];
          } // i
          count = min;
          break;
      } // switch

      return count;
    } // end of Count


    /// <summary>
    /// N×N 窗口滤波
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="N">滤波窗口大小，N 为奇数</param>
    /// <param name="filteringMethod">滤波方法</param>
    /// <returns></returns>
    public Bitmap FilterNxN(Bitmap b, int N, FilteringMethod filteringMethod)
    {
      // 如果 N 为偶数，则变 N 为奇数
      if (N % 2 == 0) N++;

      // N×N 窗口数字序列
      int[] sequence = new int[N * N];

      // 窗口半径
      int radius = N / 2;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      // 图像实际处理区域
      int rectTop = radius;
      int rectBottom = height - radius;
      int rectLeft = radius;
      int rectRight = width - radius;

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;

        int stride = srcData.Stride;
        int offset = stride - width * BPP;

        // 移向最顶行，即第 radius 行
        src += stride * rectTop;
        dst += stride * rectTop;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // 移向最左列，即每行第 radius 列
          src += BPP * rectLeft;
          dst += BPP * rectLeft;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // Alpha
            dst[3] = src[3];

            // 处理 B, G, R 三分量
            for (int i = 0; i < 3; i++)
            {
              // 收集 N×N 窗口内数字序列
              for (int m = -radius; m <= radius; m++)
              {
                for (int n = -radius; n <= radius; n++)
                {
                  sequence[(m + radius) * N + (n + radius)] = src[i + m * stride + n * BPP];
                } // n
              } // m

              // 根据用户指定的统计方法计算数字序列
              dst[i] = (byte)Count(sequence, filteringMethod);
            } // i


            // 向后移一像素
            src += BPP;
            dst += BPP;
          } // x

          // 移向下一行
          // 这里得注意要多移 radius 列，因最右边还有 radius 列不必处理
          src += (offset + BPP * radius);
          dst += (offset + BPP * radius);
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of FilterNxN


    /// <summary>
    /// 十字型滤波
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="N">十字架大小，N为奇数</param>
    /// <param name="filteringMethod">滤波方法</param>
    /// <returns></returns>
    public Bitmap FilterCross(Bitmap b, int N, FilteringMethod filteringMethod)
    {
      // 如果 N 为偶数，则变 N 为奇数
      if (N % 2 == 0) N++;

      // 十字架数字序列
      int[] sequence = new int[N * 2 - 1];

      // 十字架半径
      int radius = N / 2;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      // 图像实际处理区域
      int rectTop = radius;
      int rectBottom = height - radius;
      int rectLeft = radius;
      int rectRight = width - radius;

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;

        int stride = srcData.Stride;
        int offset = stride - width * BPP;

        // 移向最顶行，即第 radius 行
        src += stride * rectTop;
        dst += stride * rectTop;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // 移向最左列，即每行第 radius 列
          src += BPP * rectLeft;
          dst += BPP * rectLeft;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // Alpha
            dst[3] = src[3];

            // 处理 B, G, R 三分量
            for (int i = 0; i < 3; i++)
            {
              int k = 0;
              // 收集十字架的数字序列
              for (int n = -radius; n <= radius; n++)
              {
                // 收集横行
                sequence[k++] = src[i + n * BPP];

                // 收集竖行
                if (n == 0) continue; // 不收集中心点
                sequence[k++] = src[i + n * stride];
              } // n

              // 根据用户指定的统计方法计算数字序列
              dst[i] = (byte)Count(sequence, filteringMethod);
            } // i


            // 向后移一像素
            src += BPP;
            dst += BPP;
          } // x

          // 移向下一行
          // 这里得注意要多移 radius 列，因最右边还有 radius 列不必处理
          src += (offset + BPP * radius);
          dst += (offset + BPP * radius);
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of FilterCross


    /// <summary>
    /// 自适应平滑滤波
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap FilterAutoFit(Bitmap b)
    {
      // 7 邻域数字序列sequence
      int[] S = new int[7];

      // 均值
      int[] A = new int[9];

      // 方差
      int[] K = new int[9];

      // 窗口半径
      int radius = 2;

      int width = b.Width;
      int height = b.Height;

      // 先用均值滤波方法初始化目标位图
      Bitmap dstImage = FilterNxN((Bitmap)b.Clone(), 3, FilteringMethod.Mean);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      // 图像实际处理区域
      int rectTop = radius;
      int rectBottom = height - radius;
      int rectLeft = radius;
      int rectRight = width - radius;

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;

        int stride = srcData.Stride;
        int stride2 = stride * 2;
        int offset = stride - width * BPP;

        // 移向最顶行，即第 radius 行
        src += stride * rectTop;
        dst += stride * rectTop;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // 移向最左列，即每行第 radius 列
          src += BPP * rectLeft;
          dst += BPP * rectLeft;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // Alpha
            dst[3] = src[3];

            // 处理 B, G, R 三分量
            for (int i = 0; i < 3; i++)
            {
              // 左上 7 邻域
              S[0] = src[i-stride2-BP2P]; S[1] = src[i-stride2-BPP];
              S[2] = src[i-stride -BP2P]; S[3] = src[i-stride -BPP]; S[4] = src[i-stride];
                                          S[5] = src[i - BPP];       S[6] = src[i];
              A[0] = Count(S, FilteringMethod.Mean);

              // 上 7 邻域
              S[0] = src[i-stride2-BPP]; S[1] = src[i-stride2]; S[2] = src[i-stride2+BPP];
              S[3] = src[i-stride -BPP]; S[4] = src[i- stride]; S[5] = src[i-stride +BPP];
                                         S[6] = src[i];
              A[1] = Count(S, FilteringMethod.Mean);

              // 右上 7 邻域
                                    S[0] = src[i-stride2+BPP]; S[1] = src[i-stride2+BP2P];
              S[2] = src[i-stride]; S[3] = src[i-stride +BPP]; S[4] = src[i-stride +BP2P];
              S[5] = src[i];        S[6] = src[i+BPP];
              A[2] = Count(S, FilteringMethod.Mean);

              // 左 7 邻域
              S[0] = src[i-stride-BP2P]; S[1] = src[i-stride-BPP];
              S[2] = src[i-BP2P];        S[3] = src[i-BPP];        S[4] = src[i];
              S[5] = src[i+stride-BP2P]; S[6] = src[i+stride-BPP];
              A[3] = Count(S, FilteringMethod.Mean);

              // 周围 9 邻域
              // 因为目标位图已经用均值滤波方法处理过了，故不必再取周围点进行运算
              A[4] = dst[i];

              // 右 7 邻域
                             S[0] = src[i-stride+BPP]; S[1] = src[i-stride+BP2P];
              S[2] = src[i]; S[3] = src[i+BPP];        S[4] = src[i+BP2P];
                             S[5] = src[i+stride+BPP]; S[6] = src[i+stride+BP2P];
              A[5] = Count(S, FilteringMethod.Mean);

              // 左下 7 邻域
                                          S[0] = src[i-BPP];         S[1] = src[i];
              S[2] = src[i+stride -BP2P]; S[3] = src[i+stride -BPP]; S[4] = src[i+stride];
              S[5] = src[i+stride2-BP2P]; S[6] = src[i+stride2-BPP];
              A[6] = Count(S, FilteringMethod.Mean);

              // 下 7 邻域
                                         S[0] = src[i];
              S[1] = src[i+stride -BPP]; S[2] = src[i+stride];  S[3] = src[i+stride +BPP];
              S[4] = src[i+stride2-BPP]; S[5] = src[i+stride2]; S[6] = src[i+stride2+BPP];
              A[7] = Count(S, FilteringMethod.Mean);

              // 右下 7 邻域
              S[0] = src[i];        S[1] = src[i+BPP];
              S[2] = src[i+stride]; S[3] = src[i+stride +BPP]; S[4] = src[i+stride +BP2P];
                                    S[5] = src[i+stride2+BPP]; S[6] = src[i+stride2+BP2P];
              A[8] = Count(S, FilteringMethod.Mean);

              // 求方差
              for (int k = 0; k < 9; k++)
              {
                K[k] = src[i] * src[i] - A[k] * A[k];
              } // k

              // 求最小方差
              int min = K[0]; // 最小方差
              int index = 0;  // 最小方差的数组索引
              for (int k = 0; k < 9; k++)
              {
                if (K[k] < min)
                {
                  min = K[k];
                  index = k;
                }
              } // k

              // 取最小方差
              dst[i] = (byte)A[index];
            } // i


            // 向后移一像素
            src += BPP;
            dst += BPP;
          } // x

          // 移向下一行
          // 这里得注意要多移 radius 列，因最右边还有 radius 列不必处理
          src += (offset + BPP * radius);
          dst += (offset + BPP * radius);
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of FilterAutoFit


  }
}
