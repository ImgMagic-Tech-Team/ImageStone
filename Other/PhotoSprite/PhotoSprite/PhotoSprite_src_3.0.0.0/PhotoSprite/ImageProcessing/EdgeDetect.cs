using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 边缘检测类
  /// </summary>
  public class EdgeDetect : ImageInfo
  {
    /************************************************************
     * 
     * Roberts, Sobel, Prewitt, Kirsch, GaussLaplacian
     * 水平检测、垂直检测、边缘增强、边缘均衡化
     * 
     ************************************************************/


    /// <summary>
    /// 对两幅图像进行梯度运算
    /// </summary>
    /// <param name="b1">位图 1</param>
    /// <param name="b2">位图 2</param>
    /// <returns></returns>
    private Bitmap Gradient(Bitmap b1, Bitmap b2)
    {
      //Algebra a = new Algebra();

      //// 对两幅图像进行求大运算
      //return a.AlgebraOperate(b1, b2, Algebra.AlgebraMethod.Maximize);
      int width = b1.Width;
      int height = b1.Height;

      BitmapData data1 = b1.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData data2 = b2.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p1 = (byte*)data1.Scan0;
        byte* p2 = (byte*)data2.Scan0;

        int offset = data1.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              int power = (int)Math.Sqrt((p1[i] * p1[i] + p2[i] * p2[i]));
              p1[i] = (byte)(power > 255 ? 255 : power);
            } // i

            p1 += BPP;
            p2 += BPP;
          } // x

          p1 += offset;
          p2 += offset;
        } // y
      }

      b1.UnlockBits(data1);
      b2.UnlockBits(data2);

      Bitmap dstImage = (Bitmap)b1.Clone();

      b1.Dispose();
      b2.Dispose();

      return dstImage;
    } // end of Gradient


    /// <summary>
    /// 按 Roberts 算子进行边缘检测
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Roberts(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;

        int A, B;   // A(x-1, y-1)    B(x, y-1)
        int C, D;   // C(x-1,   y)    D(x,   y)

        // 指向第一行
        src += stride;
        dst += stride;

        // 不处理最上边和最左边
        for (int y = 1; y < height; y++)
        {
          // 指向每行第一列
          src += BPP;
          dst += BPP;
          for (int x = 1; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              A = src[i - stride - BPP];
              B = src[i - stride];
              C = src[i - BPP];
              D = src[i];

              dst[i] = (byte)(Math.Sqrt((A - D) * (A - D) + (B - C) * (B - C)));
            } // i

            dst[3] = src[3];

            src += BPP;
            dst += BPP;
          } // x

          src += offset;
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Roberts


    /// <summary>
    /// 按 Sobel 算子进行边缘检测
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Sobel(Bitmap b)
    {
      Matrix3x3 m = new Matrix3x3();

      //   -1 -2 -1
      //    0  0  0
      //    1  2  1
      m.Init(0);
      m.TopLeft = m.TopRight = -1;
      m.BottomLeft = m.BottomRight = 1;
      m.TopMid = -2;
      m.BottomMid = 2;
      Bitmap b1 = m.Convolute((Bitmap)b.Clone());

      //   -1  0  1
      //   -2  0  2
      //   -1  0  1
      m.Init(0);
      m.TopLeft = m.BottomLeft = -1;
      m.TopRight = m.BottomRight = 1;
      m.MidLeft = -2;
      m.MidRight = 2;
      Bitmap b2 = m.Convolute((Bitmap)b.Clone());

      //    0  1  2
      //   -1  0  1
      //   -2 -1  0
      m.Init(0);
      m.TopMid = m.MidRight = 1;
      m.MidLeft = m.BottomMid = -1;
      m.TopRight = 2;
      m.BottomLeft = -2;
      Bitmap b3 = m.Convolute((Bitmap)b.Clone());

      //   -2 -1  0
      //   -1  0  1
      //    0  1  2
      m.Init(0);
      m.TopMid = m.MidLeft = -1;
      m.MidRight = m.BottomMid = 1;
      m.TopLeft = -2;
      m.BottomRight = 2;
      Bitmap b4 = m.Convolute((Bitmap)b.Clone());

      // 梯度运算
      b = Gradient(Gradient(b1, b2), Gradient(b3, b4));

      b1.Dispose(); b2.Dispose(); b3.Dispose(); b4.Dispose();

      return b;
    } // end of Sobel


    /// <summary>
    /// 按 Prewitt 算子进行边缘检测
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Prewitt(Bitmap b)
    {
      Matrix3x3 m = new Matrix3x3();

      //   -1 -1 -1
      //    0  0  0
      //    1  1  1
      m.Init(0);
      m.TopLeft = m.TopMid = m.TopRight = -1;
      m.BottomLeft = m.BottomMid = m.BottomRight = 1;
      Bitmap b1 = m.Convolute((Bitmap)b.Clone());

      //   -1  0  1
      //   -1  0  1
      //   -1  0  1
      m.Init(0);
      m.TopLeft = m.MidLeft = m.BottomLeft = -1;
      m.TopRight = m.MidRight = m.BottomRight = 1;
      Bitmap b2 = m.Convolute((Bitmap)b.Clone());

      //   -1 -1  0
      //   -1  0  1
      //    0  1  1
      m.Init(0);
      m.TopLeft = m.MidLeft = m.TopMid = -1;
      m.BottomMid = m.BottomRight = m.MidRight = 1;
      Bitmap b3 = m.Convolute((Bitmap)b.Clone());

      //    0  1  1
      //   -1  0  1
      //   -1 -1  0
      m.Init(0);
      m.TopMid = m.TopRight = m.MidRight = 1;
      m.MidLeft = m.BottomLeft = m.BottomMid = -1;
      Bitmap b4 = m.Convolute((Bitmap)b.Clone());

      // 梯度运算
      b = Gradient(Gradient(b1, b2), Gradient(b3, b4));

      b1.Dispose(); b2.Dispose(); b3.Dispose(); b4.Dispose();

      return b;
    } // end of Prewitt


    /// <summary>
    /// 按 Kirsch 算子进行边缘检测
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Kirsch(Bitmap b)
    {
      Matrix3x3 m = new Matrix3x3();

      //    5  5  5
      //   -3  0 -3
      //   -3 -3 -3
      m.Init(-3);
      m.Center = 0;
      m.TopLeft = m.TopMid = m.TopRight = 5;
      Bitmap b1 = m.Convolute((Bitmap)b.Clone());

      //   -3  5  5
      //   -3  0  5
      //   -3 -3 -3
      m.Init(-3);
      m.Center = 0;
      m.TopMid = m.TopRight = m.MidRight = 5;
      Bitmap b2 = m.Convolute((Bitmap)b.Clone());

      //   -3 -3  5
      //   -3  0  5
      //   -3 -3  5
      m.Init(-3);
      m.Center = 0;
      m.TopRight = m.MidRight = m.BottomRight = 5;
      Bitmap b3 = m.Convolute((Bitmap)b.Clone());

      //   -3 -3 -3
      //   -3  0  5
      //   -3  5  5
      m.Init(-3);
      m.Center = 0;
      m.MidRight = m.BottomRight = m.BottomMid = 5;
      Bitmap b4 = m.Convolute((Bitmap)b.Clone());

      //   -3 -3 -3
      //   -3  0 -3
      //    5  5  5
      m.Init(-3);
      m.Center = 0;
      m.BottomLeft = m.BottomMid = m.BottomRight = 5;
      Bitmap b5 = m.Convolute((Bitmap)b.Clone());

      //   -3 -3 -3
      //    5  0 -3
      //    5  5 -3
      m.Init(-3);
      m.Center = 0;
      m.MidLeft = m.BottomLeft = m.BottomMid = 5;
      Bitmap b6 = m.Convolute((Bitmap)b.Clone());

      //    5 -3 -3
      //    5  0 -3
      //    5 -3 -3
      m.Init(-3);
      m.Center = 0;
      m.TopLeft = m.MidLeft = m.BottomLeft = 5;
      Bitmap b7 = m.Convolute((Bitmap)b.Clone());

      //    5  5 -3
      //    5  0 -3
      //   -3 -3 -3
      m.Init(-3);
      m.Center = 0;
      m.TopLeft = m.MidLeft = m.TopMid = 5;
      Bitmap b8 = m.Convolute((Bitmap)b.Clone());

      // 梯度运算
      Bitmap b9 = Gradient(Gradient(b1, b2), Gradient(b3, b4));
      Bitmap b10 = Gradient(Gradient(b5, b6), Gradient(b7, b8));
      b = Gradient(b9, b10);

      b1.Dispose(); b2.Dispose(); b3.Dispose(); b4.Dispose();
      b5.Dispose(); b6.Dispose(); b7.Dispose(); b8.Dispose();
      b9.Dispose(); b10.Dispose();

      return b;
    } // end of Kirsch


    /// <summary>
    /// 按 GaussLaplacian 算子进行边缘检测
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public Bitmap GaussLaplacian(Bitmap b)
    {
      int[,] kernel = { 
        {-2, -4, -4, -4, -2},
        {-4,  0,  8,  0, -4},
        {-4,  8, 24,  8, -4},
        {-4,  0,  8,  0, -4},
        {-2, -4, -4, -4, -2}
      };

      MatrixNxN m = new MatrixNxN();
      m.Kernel = kernel;

      return m.Convolute(b);
    } // end of GaussLaplacian


    /// <summary>
    /// 按水平边缘检测算子进行边缘检测
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public Bitmap EdgeDetectHorizontal(Bitmap b)
    {
      int[,] kernel = { 
        { 0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0},
        {-1, -1, -1, -1, -1, -1, -1},
        { 0,  0,  0,  0,  0,  0,  0},
        { 1,  1,  1,  1,  1,  1,  1},
        { 0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0},
      };

      MatrixNxN m = new MatrixNxN();
      m.Kernel = kernel;

      return m.Convolute(b);
    } // end of EdgeDetectHorizontal


    /// <summary>
    /// 按垂直边缘检测算子进行边缘检测
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public Bitmap EdgeDetectVertical(Bitmap b)
    {
      int[,] kernel = { 
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
      };

      MatrixNxN m = new MatrixNxN();
      m.Kernel = kernel;

      return m.Convolute(b);
    } // end of EdgeDetectVertical


    /// <summary>
    /// 边缘增强
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="threshold">阈值</param>
    /// <returns></returns>
    public Bitmap EdgeEnhance(Bitmap b, int threshold)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      // 图像实际处理区域
      // 不考虑最左 1 列和最右 1 列
      // 不考虑最上 1 行和最下 1 行
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
        int maxPixel = 0;


        // 指向第 1 行
        src += stride;
        dst += stride;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // 指向每行第 1 列像素
          src += BPP;
          dst += BPP;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // Alpha
            dst[3] = src[3];

            // 处理 B, G, R 三分量
            for (int i = 0; i < 3; i++)
            {
              // 右上-左下
              maxPixel = src[i - stride + BPP] - src[i + stride - BPP];
              if (maxPixel < 0) maxPixel = -maxPixel;

              // 左上-右下
              pixel = src[i - stride - BPP] - src[i + stride + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 上-下
              pixel = src[i - stride] - src[i + stride];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 左-右
              pixel = src[i - BPP] - src[i + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 进行阈值判断
              if (maxPixel < threshold) maxPixel = 0;
              
              dst[i] = (byte)maxPixel;
            }

            // 向后移一像素
            src += BPP;
            dst += BPP;
          } // x

          // 移向下一行
          // 这里得注意要多移 1 列，因最右边还有 1 列不必处理
          src += offset + BPP;
          dst += offset + BPP;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of EdgeEnhance


    /// <summary>
    /// 边缘均衡化
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="threshold">阈值</param>
    /// <returns></returns>
    public Bitmap EdgeHomogenize(Bitmap b, int threshold)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      // 图像实际处理区域
      // 不考虑最左 1 列和最右 1 列
      // 不考虑最上 1 行和最下 1 行
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
        int maxPixel = 0;

        // 指向第 1 行
        src += stride;
        dst += stride;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // 指向每行第 1 列像素
          src += BPP;
          dst += BPP;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // Alpha
            dst[3] = src[3];

            // 处理 B, G, R 三分量
            for (int i = 0; i < 3; i++)
            {
              // 上
              maxPixel = src[i] - src[i - stride];
              if (maxPixel < 0) maxPixel = -maxPixel;

              // 右上
              pixel = src[i] - src[i - stride + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 右
              pixel = src[i] - src[i + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 右下
              pixel = src[i] - src[i + stride + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 下
              pixel = src[i] - src[i + stride];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 左下
              pixel = src[i] - src[i + stride - BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 左
              pixel = src[i] - src[i - BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 左上
              pixel = src[i] - src[i - stride - BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // 进行阈值判断
              if (maxPixel < threshold) maxPixel = 0;

              dst[i] = (byte)maxPixel;
            }

            // 向后移一像素
            src += BPP;
            dst += BPP;
          } // x

          // 移向下一行
          // 这里得注意要多移 1 列，因最右边还有 1 列不必处理
          src += offset + BPP;
          dst += offset + BPP;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of EdgeHomogenize


  }
}
