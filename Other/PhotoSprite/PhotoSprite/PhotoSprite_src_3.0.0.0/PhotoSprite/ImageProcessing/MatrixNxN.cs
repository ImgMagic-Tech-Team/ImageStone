using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// N×N 转换矩阵
  /// </summary>
  public class MatrixNxN : ImageInfo
  {
    int size = 0;
    int radius = 0;
    int scale = 1;
    int kernelOffset = 0;
    int[,] kernel; // 卷积核

    /// <summary>
    /// 获取卷积核大小，即窗口宽度
    /// </summary>
    public int Size
    {
      get
      {
        return size;
      }
    }

    /// <summary>
    /// 获取或设置矩阵序列，其中包含矩阵元素、缩放比例和偏移量
    /// </summary>
    public int[] Sequence
    {
      get
      {
        int[] sequence = new int[kernel.Length + 2];
        for (int i = 0; i < size; i++)
        {
          for (int j = 0; j < size; j++)
          {
            sequence[i * size + j] = kernel[i, j];
          } // j
        } // i

        sequence[sequence.Length - 2] = scale;
        sequence[sequence.Length - 1] = kernelOffset;

        return sequence;
      }
      set
      {
        int [] sequence = value;

        // 窗口宽度
        size = (int)(Math.Sqrt(sequence.Length));

        // 窗口半径
        radius = size / 2;

        // 设置矩阵
        kernel = new int[size, size];
        for (int i = 0; i < size; i++)
        {
          for (int j = 0; j < size; j++)
          {
            kernel[i, j] = sequence[i * size + j];
          } // j
        } // i

        scale = sequence[sequence.Length - 2];
        kernelOffset = sequence[sequence.Length - 1];
      }
    } // Sequence

    /// <summary>
    /// 获取或设置卷积核，即窗口矩阵
    /// </summary>
    public int[,] Kernel
    {
      get
      {
        return kernel;
      }
      set
      {
        kernel = value;

        // 窗口宽度
        size = kernel.GetLength(0);

        // 窗口半径
        radius = size / 2;
      }
    }

    /// <summary>
    /// 获取或设置缩放比例
    /// </summary>
    public int Scale
    {
      get
      {
        return scale;
      }
      set
      {
        scale = value;
      }
    }

    /// <summary>
    /// 获取或设置偏移量
    /// </summary>
    public int Offset
    {
      get
      {
        return kernelOffset;
      }
      set
      {
        kernelOffset = value;
      }
    }


    /// <summary>
    /// 将图像按 N×N 窗口进行卷积转换
    /// </summary>
    /// <param name="srcImage">位图流</param>
    /// <returns></returns>
    public Bitmap Convolute(Bitmap srcImage)
    {
      // 避免被零除
      if (scale == 0) scale = 1;

      int width = srcImage.Width;
      int height = srcImage.Height;

      Bitmap dstImage = (Bitmap)srcImage.Clone();

      BitmapData srcData = srcImage.LockBits(new Rectangle(0, 0, width, height),
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

        int pixel = 0;

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
            // 如果当前像素为透明色，则跳过不处理
            if (src[3] > 0)
            {
              // 处理 B, G, R 三分量
              for (int i = 0; i < 3; i++)
              {
                pixel = 0;

                // 卷积运算
                for (int m = -radius; m <= radius; m++)
                {
                  for (int n = -radius; n <= radius; n++)
                  {
                    pixel += src[i + m * stride + n * BPP] * kernel[m + radius, n + radius];
                  } // n
                } // m

                pixel = pixel / scale + kernelOffset;

                if (pixel < 0) pixel = 0;
                if (pixel > 255) pixel = 255;

                dst[i] = (byte)pixel;
              } // i
            }

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

      srcImage.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      srcImage.Dispose();

      return dstImage;
    } // end of Convolute


  }
}
