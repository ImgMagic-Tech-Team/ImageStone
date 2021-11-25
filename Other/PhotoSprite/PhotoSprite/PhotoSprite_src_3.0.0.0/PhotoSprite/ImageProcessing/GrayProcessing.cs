using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 灰度处理类
  /// </summary>
  public class GrayProcessing : ImageInfo
  {
    /// <summary>
    /// 灰度方法
    /// </summary>
    public enum GrayMethod
    {
      /// <summary>
      /// 加权平均法
      /// </summary>
      WeightAveraging,

      /// <summary>
      /// 最大值法
      /// </summary>
      Maximum,

      /// <summary>
      /// 平均值法
      /// </summary>
      Average
    }


    /************************************************************
     * 
     * 初始化二维数组、位图转数组、数组转位图
     * 
     ************************************************************/


    /// <summary>
    /// 初始化一个二维数组
    /// </summary>
    /// <param name="width">数组宽</param>
    /// <param name="height">数组高</param>
    /// <param name="init">初始值</param>
    /// <returns></returns>
    public byte[,] InitArray(int width, int height, byte init)
    {
      byte[,] dst = new byte[width, height];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          dst[x, y] = init;
        } // x
      } // y

      return dst;
    } // end of InitArray


    /// <summary>
    /// 将灰度位图流转换为二维数组
    /// </summary>
    /// <param name="b">灰度位图流</param>
    /// <returns></returns>
    public byte[,] Image2Array(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      // 申请一个二维数组
      byte[,] GrayArray = new byte[width, height];

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 因为已经是灰度色，这里只取蓝色分量作为灰度
            GrayArray[x, y] = p[0];

            p += BPP;
          } //  x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return GrayArray;
    } // end of Image2Array


    /// <summary>
    /// 将二维数组转换为灰度位图流
    /// </summary>
    /// <param name="GrayArray">灰度数组</param>
    /// <returns></returns>
    public Bitmap Array2Image(byte[,] GrayArray)
    {
      int width = GrayArray.GetLength(0);
      int height = GrayArray.GetLength(1);

      Bitmap b = new Bitmap(width, height);
      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            p[0] = p[1] = p[2] = GrayArray[x, y];
            p[3] = 255;

            p += BPP;
          } //  x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Array2Image


    /************************************************************
     * 
     * 位图转二维数组、二值数组转位图、阈值化、自适应阈值、灰度
     * 
     ************************************************************/


    /// <summary>
    /// 将位图流转换为二值数组
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="threshold">阈值</param>
    /// <returns></returns>
    public byte[,] BinaryArray(Bitmap b, byte threshold)
    {
      int width = b.Width;
      int height = b.Height;

      //先将位图灰度化
      b = Gray(b, GrayMethod.WeightAveraging);

      // 将灰度图转化为灰度数组
      byte[,] GrayArray = Image2Array(b);

      b.Dispose();

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          // 当前像素颜色灰度值与指定阈值相比较
          if (GrayArray[x, y] >= threshold)
            GrayArray[x, y] = 255;
          else
            GrayArray[x, y] = 0;
        } //  x
      } // y

      return GrayArray;
    } // end of BinaryArray


    /// <summary>
    /// 将二值数组转换为双色位图流
    /// </summary>
    /// <param name="GrayArray">二值数组</param>
    /// <param name="bgColor">背景色</param>
    /// <param name="fgColor">前景色</param>
    /// <returns></returns>
    public Bitmap BinaryImage(byte[,] GrayArray, Color bgColor, Color fgColor)
    {
      // 获取二维数组的宽高
      int width = GrayArray.GetLength(0);
      int height = GrayArray.GetLength(1);

      // 背景色
      byte bgAlpha = (byte)bgColor.A;
      byte bgRed = (byte)bgColor.R;
      byte bgGreen = (byte)bgColor.G;
      byte bgBlue = (byte)bgColor.B;

      // 前景色
      byte fgAlpha = (byte)fgColor.A;
      byte fgRed = (byte)fgColor.R;
      byte fgGreen = (byte)fgColor.G;
      byte fgBlue = (byte)fgColor.B;

      Bitmap b = new Bitmap(width, height);
      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 设置图像前景、背景颜色
            if (GrayArray[x, y] <128 )
            {
              p[3] = fgAlpha;
              p[2] = fgRed;
              p[1] = fgGreen;
              p[0] = fgBlue;
            }
            else
            {
              p[3] = bgAlpha;
              p[2] = bgRed;
              p[1] = bgGreen;
              p[0] = bgBlue;
            }

            p += BPP;
          } //  x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of BinaryImage


    /// <summary>
    /// 阈值化
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="threshold">阈值</param>
    /// <returns></returns>
    public Bitmap Thresholding(Bitmap b, byte threshold)
    {
      byte[,] GrayArray = BinaryArray(b, threshold);
      Bitmap dstImage = BinaryImage(GrayArray, Color.White, Color.Black);

      return dstImage;
    } // end of Thresholding 


    /// <summary>
    /// 自适应阈值
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap AutoFitThreshold(Bitmap b)
    {
      // 图像灰度化
      GrayProcessing gp = new GrayProcessing();
      b = gp.Gray(b, GrayMethod.WeightAveraging);

      // 建立直方图，并获取灰度统计信息
      Histogram histogram = new Histogram(b);
      int [] GrayLevel = histogram.Red.Value;

      int peak1, peak2, valley;
      int peak1Index, peak2Index, valleyIndex;

      // 取双峰
      peak1 = peak2 = GrayLevel[0];
      peak1Index = peak2Index = 0;
      for (int i = 1; i < 256; i++)
      {
        // 如果产生新的高峰，则将第一峰退居第二峰，新的高峰升为第一峰
        if (GrayLevel[i] > peak1)
        {
          peak2 = peak1;
          peak2Index = peak1Index;

          peak1 = GrayLevel[i];
          peak1Index = i;
        }
      } // i

      // 判断两个峰值索引
      int max = peak1Index;
      int min = peak2Index;
      if (max < min)
      {
        int t = max;
        max = min;
        min = t;
      }

      // 取峰谷
      valley = GrayLevel[min];
      valleyIndex = min;
      for (int i = min; i < max; i++)
      {
        if (GrayLevel[i] < valley)
        {
          valley = GrayLevel[i];
          valleyIndex = i;
        }
      } // i

      // 根据找到的谷值对图像进行二值化
      return Thresholding(b, (byte)valleyIndex);
    } // end of AutoFitThreshold 


    /// <summary>
    /// 图像灰度变换
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="grayMethod">灰度方法</param>
    /// <returns></returns>
    public Bitmap Gray(Bitmap b, GrayMethod grayMethod)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        byte R, G, B, gray;

        switch (grayMethod)
        {
          case GrayMethod.Maximum:
            for (int y = 0; y < height; y++)
            {
              for (int x = 0; x < width; x++)
              {
                R = p[2];
                G = p[1];
                B = p[0];

                // gray = Max( R, G, B )
                gray = (gray = B >= G ? B : G) >= R ? gray : R;

                p[0] = p[1] = p[2] = gray;

                p += BPP;
              } // x
              p += offset;
            } // y
            break;

          case GrayMethod.Average:
            for (int y = 0; y < height; y++)
            {
              for (int x = 0; x < width; x++)
              {
                R = p[2];
                G = p[1];
                B = p[0];

                // gray = ( R + G + B ) / 3
                gray = (byte)((R + G + B) / 3);

                p[0] = p[1] = p[2] = gray;

                p += BPP;
              } // x
              p += offset;
            } // y
            break;

          case GrayMethod.WeightAveraging:
          default:
            for (int y = 0; y < height; y++)
            {
              for (int x = 0; x < width; x++)
              {
                R = p[2];
                G = p[1];
                B = p[0];

                // gray = 0.3*R + 0.59*G + 0.11*B
                gray = (byte)((19661 * R + 38666 * G + 7209 * B) >> 16);

                p[0] = p[1] = p[2] = gray;

                p += BPP;
              } // x
              p += offset;
            } // y
            break;
        } // switch
      }

      b.UnlockBits(data);

      return b;
    } // end of Gray


  }
}
