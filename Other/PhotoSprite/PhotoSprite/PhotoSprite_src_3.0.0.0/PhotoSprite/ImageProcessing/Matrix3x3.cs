using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 3×3 转换矩阵
  /// </summary>
  public class Matrix3x3 : ImageInfo
  {
    int topLeft = 0, topMid = 0, topRight = 0;
    int midLeft = 0, center = 1, midRight = 0;
    int bottomLeft = 0, bottomMid = 0, bottomRight = 0;

    int scale = 1;
    int kernelOffset = 0;

    /// <summary>
    /// 获取或设置左上点权值
    /// </summary>
    public int TopLeft
    {
      get
      {
        return topLeft;
      }
      set
      {
        topLeft = value;
      }
    }

    /// <summary>
    /// 获取或设置正上点权值
    /// </summary>
    public int TopMid
    {
      get
      {
        return topMid;
      }
      set
      {
        topMid = value;
      }
    }

    /// <summary>
    /// 获取或设置右上点权值
    /// </summary>
    public int TopRight
    {
      get
      {
        return topRight;
      }
      set
      {
        topRight = value;
      }
    }

    /// <summary>
    /// 获取或设置左点权值
    /// </summary>
    public int MidLeft
    {
      get
      {
        return midLeft;
      }
      set
      {
        midLeft = value;
      }
    }

    /// <summary>
    /// 获取或设置中心点权值
    /// </summary>
    public int Center
    {
      get
      {
        return center;
      }
      set
      {
        center = value;
      }
    }

    /// <summary>
    /// 获取或设置右点权值
    /// </summary>
    public int MidRight
    {
      get
      {
        return midRight;
      }
      set
      {
        midRight = value;
      }
    }

    /// <summary>
    /// 获取或设置左下点权值
    /// </summary>
    public int BottomLeft
    {
      get
      {
        return bottomLeft;
      }
      set
      {
        bottomLeft = value;
      }
    }

    /// <summary>
    /// 获取或设置正下点权值
    /// </summary>
    public int BottomMid
    {
      get
      {
        return bottomMid;
      }
      set
      {
        bottomMid = value;
      }
    }

    /// <summary>
    /// 获取或设置右下点权值
    /// </summary>
    public int BottomRight
    {
      get
      {
        return bottomRight;
      }
      set
      {
        bottomRight = value;
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
    /// 初始化窗口所有点为同一权值
    /// </summary>
    /// <param name="degree">权值</param>
    public void Init(int degree)
    {
      topLeft = topMid = topRight = 
      midLeft = center = midRight =
      bottomLeft = bottomMid = bottomRight = degree;
    } // end of Init


    /// <summary>
    /// 将图像按 3X3 窗口进行卷积转换
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
      // 图像最外围一圈不处理
      int rectTop = 1;
      int rectBottom = height - 1;
      int rectLeft = 1;
      int rectRight = width - 1;

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;

        int stride = srcData.Stride;
        int offset = stride - width * BPP;

        int pixel = 0;

        // 移向第一行
        src += stride;
        dst += stride;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // 移向每行第一列
          src += BPP;
          dst += BPP;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // 如果当前像素为透明色，则跳过不处理
            if (src[3] > 0)
            {
              // 处理 B, G, R 三分量
              for (int i = 0; i < 3; i++)
              {
                pixel =
                  src[i - stride - BPP] * topLeft + 
                  src[i - stride] * topMid + 
                  src[i - stride + BPP] * topRight +
                  src[i - BPP] * midLeft + 
                  src[i] * center + 
                  src[i + BPP] * midRight +
                  src[i + stride - BPP] * bottomLeft + 
                  src[i + stride] * bottomMid + 
                  src[i + stride + BPP] * bottomRight;
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
          // 这里得注意要多移一列，因最右列不处理
          src += (offset + BPP);
          dst += (offset + BPP);
        } // y
      }

      srcImage.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      srcImage.Dispose();

      return dstImage;
    } // end of Convolute


  }
}
