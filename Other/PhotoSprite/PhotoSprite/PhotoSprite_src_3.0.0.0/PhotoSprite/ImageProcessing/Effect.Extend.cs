using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  partial class Effect
  {
    /************************************************************
     * 
     * 剪纸、铅笔画、素描、连环画
     * 碧绿、棕褐、渲染
     * 冰冻、熔铸
     * 暗调、对调、怪调
     * 
     ************************************************************/


    /// <summary>
    /// 剪纸
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="threshold">阈值[0， 255]</param>
    /// <param name="bgColor">背景色</param>
    /// <param name="fgColor">前景色</param>
    /// <returns></returns>
    public Bitmap PaperCut(Bitmap b, byte threshold, Color bgColor, Color fgColor)
    {
      GrayProcessing gp = new GrayProcessing();

      // 对图像按指定阈值进行二值化
      byte[,] GrayArray = gp.BinaryArray(b, threshold);

      // 对图像进行上色
      return gp.BinaryImage(GrayArray, bgColor, fgColor);
    } // end of PaperCut


    /// <summary>
    /// 铅笔画
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="threshold">颜色阈值[0, 100]</param>
    /// <returns></returns>
    public Bitmap Pencil(Bitmap b, int threshold)
    {
      if (threshold < 0) threshold = 0;
      if (threshold > 100) threshold = 100;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
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

        // 源图像周围八点指针
        byte* nw; byte* n; byte* ne;
        byte* w; byte* e;
        byte* sw; byte* s; byte* se;

        int R, G, B;

        // 移向第一行
        src += stride;
        dst += stride;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // 移向每行第 1 列
          src += BPP;
          dst += BPP;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // 取周围八个点的颜色值
            nw = src - stride - BPP;   // (x-1, y-1)
            n = src - stride;          // (x  , y-1)
            ne = src - stride + BPP;   // (x+1, y-1)

            w = src - BPP;             // (x-1, y)
            e = src + BPP;             // (x+1, y)

            sw = src + stride - BPP;   // (x-1, y+1)
            s = src + stride;          // (x  , y+1)
            se = src + stride + BPP;   // (x+1, y+1)

            // 计算当前像素点与周围八点平均值的差
            R = nw[2] + n[2] + ne[2] + w[2] + e[2] + sw[2] + s[2] + se[2];
            R = src[2] - R / 8;
            G = nw[1] + n[1] + ne[1] + w[1] + e[1] + sw[1] + s[1] + se[1];
            G = src[1] - G / 8;
            B = nw[0] + n[0] + ne[0] + w[0] + e[0] + sw[0] + s[0] + se[0];
            B = src[0] - B / 8;

            // 取差值的绝对值
            if (R < 0) R = -R;
            if (G < 0) G = -G;
            if (B < 0) B = -B;

            // 如果差值大于指定阈值，则在当前位置描点
            if (R >= threshold || G >= threshold || B >= threshold)
            {
              dst[0] = dst[1] = dst[2] = 0;
            }
            else
            {
              dst[0] = dst[1] = dst[2] = 255;
            }

            dst[3] = src[3];  // A

            // 向后移一像素
            src += BPP;
            dst += BPP;
          } // x

          // 移向下一行
          // 这里得注意要多移 1 列，因最右列不必处理
          src += offset + BPP;
          dst += offset + BPP;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Pencil


    /// <summary>
    /// 素描
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="threshold">颜色阈值[0, 100]</param>
    /// <returns></returns>
    public Bitmap Sketch(Bitmap b, int threshold)
    {
      if (threshold < 0) threshold = 0;
      if (threshold > 100) threshold = 100;

      // 先求灰度
      GrayProcessing gp = new GrayProcessing();
      b = gp.Gray(b, GrayProcessing.GrayMethod.WeightAveraging);

      int width = b.Width;
      int height = b.Height;

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      // 图像实际处理区域
      int rectTop = 0;
      int rectBottom = height - 1;
      int rectLeft = 0;
      int rectRight = width - 1;

      unsafe
      {
        byte* dst = (byte*)srcData.Scan0;
        // 取当前像素点右下角像素
        byte* src = dst + srcData.Stride + BPP;

        int offset = srcData.Stride - width * BPP;

        int diff;

        for (int y = rectTop; y < rectBottom; y++)
        {
          for (int x = rectLeft; x < rectRight; x++)
          {
            // 计算相邻两点的灰度差
            diff = dst[0] - src[0];
            if (diff < 0)
              diff = -diff;

            // 如果灰度差大于指定阈值，则颜色跳变严重，需描下轮廓，
            // 即把该点置为黑色，否则置为白色
            if (diff >= threshold)
              dst[0] = dst[1] = dst[2] = 0;
            else
              dst[0] = dst[1] = dst[2] = 255;

            // 向后移一像素
            src += BPP;
            dst += BPP;
          } // x

          // 移向下一行
          // 这里得注意要多移 1 列，因最右列不必处理
          src += offset + BPP;
          dst += offset + BPP;
        } // y
      }

      b.UnlockBits(srcData);

      return b;
    } // end of Sketch


    /// <summary>
    /// 连环画
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Comic(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            // R = |g – b + g + r| * r / 256;
            pixel = G - B + G + R;
            if (pixel < 0) pixel = -pixel;
            pixel = pixel * R / 256;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            // G = |b – g + b + r| * r / 256;
            pixel = B - G + B + R;
            if (pixel < 0) pixel = -pixel;
            pixel = pixel * R / 256;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            // B = |b – g + b + r| * g / 256;
            pixel = B - G + B + R;
            if (pixel < 0) pixel = -pixel;
            pixel = pixel * G / 256;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      // 对图像进行灰度化
      GrayProcessing gp = new GrayProcessing();
      return gp.Gray(b, GrayProcessing.GrayMethod.WeightAveraging);
    } // end of Comic


    /// <summary>
    /// 碧绿
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Aqua(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // R = (g - b) * (g - b) / 128;
            // G = (r - b) * (r - b) / 128;
            // B = (r - g) * (r - g) / 128;

            R = p[2];
            G = p[1];
            B = p[0];

            pixel = (G - B) * (G - B) / 128;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            pixel = (R - B) * (R - B) / 128;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            pixel = (R - G) * (R - G) / 128;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          }  // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Aqua


    /// <summary>
    /// 棕褐色、旧相片
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Sepia(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // R = 0.393 * r + 0.769 * g + 0.189 * b;
            // G = 0.349 * r + 0.686 * g + 0.168 * b;
            // B = 0.272 * r + 0.534 * g + 0.131 * b;
            R = (25756 * p[2] + 50397 * p[1] + 12386 * p[0]) >> 16;
            G = (22872 * p[2] + 44958 * p[1] + 11010 * p[0]) >> 16;
            B = (17826 * p[2] + 34996 * p[1] + 8585 * p[0]) >> 16;

            if (R < 0) R = 0;
            if (R > 255) R = 255;

            if (G < 0) G = 0;
            if (G > 255) G = 255;

            if (B < 0) B = 0;
            if (B > 255) B = 255;

            p[2] = (byte)R;
            p[1] = (byte)G;
            p[0] = (byte)B;

            p += BPP;
          } // x
          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Sepia


    /// <summary>
    /// 颜色渲染
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="red">红色分量[0, 255]</param>
    /// <param name="green">绿色分量[0, 255]</param>
    /// <param name="blue">蓝色分量[0, 255]</param>
    /// <returns></returns>
    public Bitmap Colorize(Bitmap b, int red, int green, int blue)
    {
      if (red < 0) red = 0;
      if (red > 255) red = 255;
      if (green < 0) green = 0;
      if (green > 255) green = 255;
      if (blue < 0) blue = 0;
      if (blue > 255) blue = 255;

      // 灰度化
      GrayProcessing gp = new GrayProcessing();
      b = gp.Gray(b, GrayProcessing.GrayMethod.WeightAveraging);

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2] * red / 255;
            G = p[1] * green / 255;
            B = p[0] * blue / 255;

            p[2] = (byte)R;
            p[1] = (byte)G;
            p[0] = (byte)B;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Colorize


    /// <summary>
    /// 冰冻
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Ice(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            // R = |r - g - b| * 3 / 2;
            pixel = R - G - B;
            pixel = pixel * 3 / 2;
            if (pixel < 0) pixel = -pixel;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            // G = |g - b - r| * 3 / 2;
            pixel = G - B - R;
            pixel = pixel * 3 / 2;
            if (pixel < 0) pixel = -pixel;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            // B = |b - r - g| * 3 / 2;
            pixel = B - R - G;
            pixel = pixel * 3 / 2;
            if (pixel < 0) pixel = -pixel;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Ice


    /// <summary>
    /// 熔铸
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Molten(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            pixel = R * 128 / (G + B + 1);
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            pixel = G * 128 / (B + R + 1);
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            pixel = B * 128 / (R + G + 1);
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          }  // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Molten


    /// <summary>
    /// 暗调
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Darkness(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            p[2] = (byte)(R * R / 255);
            p[1] = (byte)(G * G / 255);
            p[0] = (byte)(B * B / 255);

            p += BPP;
          }  // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Darkness


    /// <summary>
    /// 对调
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Subtense(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            p[2] = (byte)(G * B / 255); // R
            p[1] = (byte)(B * R / 255); // G
            p[0] = (byte)(R * G / 255); // B

            p += BPP;
          }  // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Subtense


    /// <summary>
    /// 怪调
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Whim(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        double R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            pixel = Math.Sin(Math.Atan2(G, B)) * 255;
            p[2] = (byte)pixel;

            pixel = Math.Sin(Math.Atan2(B, R)) * 255;
            p[1] = (byte)pixel;

            pixel = Math.Sin(Math.Atan2(R, G)) * 255;
            p[0] = (byte)pixel;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Whim


    /************************************************************
     * 
     * 挤压、球面、漩涡、波浪
     * 摩尔纹
     * 
     ************************************************************/


    /// <summary>
    /// 对图像进行挤压特效处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">挤压幅度[1, 32]</param>
    /// <returns></returns>
    public Bitmap Pinch(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 32) degree = 32;

      int width = b.Width;
      int height = b.Height;
      int midX = width / 2;
      int midY = height / 2;

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        int X, Y;
        int offsetX, offsetY;

        // 弧度、半径
        double radian, radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 当前点与图像中心点的偏移量
            offsetX = x - midX;
            offsetY = y - midY;

            // 弧度
            radian = Math.Atan2(offsetY, offsetX);

            // 半径
            radius = Math.Sqrt(offsetX * offsetX + offsetY * offsetY);
            radius = Math.Sqrt(radius) * degree;

            // 映射实际像素点
            X = (int)(radius * Math.Cos(radian)) + midX;
            Y = (int)(radius * Math.Sin(radian)) + midY;

            // 边界约束
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Pinch


    /// <summary>
    /// 对图像进行球面特效处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Spherize(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;
      int midX = width / 2;
      int midY = height / 2;
      // Max(midX, midY)
      double maxMidXY = (midX > midY ? midX : midY);

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        int X, Y;
        int offsetX, offsetY;

        // 弧度、半径
        double radian, radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 当前点与图像中心点的偏移量
            offsetX = x - midX;
            offsetY = y - midY;

            // 弧度
            radian = Math.Atan2(offsetY, offsetX);

            // 注意，这里并非实际半径
            radius = (offsetX * offsetX + offsetY * offsetY) / maxMidXY;

            // 映射实际像素点
            X = (int)(radius * Math.Cos(radian)) + midX;
            Y = (int)(radius * Math.Sin(radian)) + midY;

            // 边界约束
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Spherize


    /// <summary>
    /// 对图像进行漩涡特效处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">漩涡幅度[1, 100]</param>
    /// <returns></returns>
    public Bitmap Swirl(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 100) degree = 100;
      double swirlDegree = degree / 1000.0;

      int width = b.Width;
      int height = b.Height;
      int midX = width / 2;
      int midY = height / 2;

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        int X, Y;
        int offsetX, offsetY;

        // 弧度、半径
        double radian, radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 当前点与图像中心点的偏移量
            offsetX = x - midX;
            offsetY = y - midY;

            // 弧度
            radian = Math.Atan2(offsetY, offsetX);

            // 半径，即两点间的距离
            radius = Math.Sqrt(offsetX * offsetX + offsetY * offsetY);

            // 映射实际像素点
            X = (int)(radius * Math.Cos(radian + swirlDegree * radius)) + midX;
            Y = (int)(radius * Math.Sin(radian + swirlDegree * radius)) + midY;

            // 边界约束
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Swirl


    /// <summary>
    /// 对图像进行波浪特效处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">波浪振幅[1, 32]</param>
    /// <returns></returns>
    public Bitmap Wave(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 32) degree = 32;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        int X, Y;
        // 圆周率 2*pi
        double PI2 = Math.PI * 2.0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            X = (int)(degree * Math.Sin(PI2 * y / 128.0)) + x;
            Y = (int)(degree * Math.Cos(PI2 * x / 128.0)) + y;

            // 边界约束
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Wave


    /// <summary>
    /// 对图像进行摩尔纹特效处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">强度[1, 16]</param>
    /// <returns></returns>
    public Bitmap MoireFringe(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 16) degree = 16;

      int width = b.Width;
      int height = b.Height;
      int midX = width / 2;
      int midY = height / 2;

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        int X, Y;
        int offsetX, offsetY;

        // 弧度、半径
        double radian, radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 当前点与图像中心点的偏移量
            offsetX = x - midX;
            offsetY = y - midY;

            // 弧度
            radian = Math.Atan2(offsetX, offsetY);

            // 半径
            radius = Math.Sqrt(offsetX * offsetX + offsetY * offsetY);

            // 映射实际像素点
            X = (int)(radius * Math.Sin(radian + degree * radius));
            Y = (int)(radius * Math.Sin(radian + degree * radius));

            // 边界约束
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      // 将处理后的纹理图与原图像进行色彩混合
      return Inosculate(b, dstImage, 128);
    } // end of MoireFringe


    /************************************************************
     * 
     * 扩散、查找边缘、照亮边缘、灯光、马赛克、油画、曝光
     * 
     ************************************************************/


    /// <summary>
    /// 扩散
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">扩散幅度[1, 32]</param>
    /// <returns></returns>
    public Bitmap Diffuse(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 32) degree = 32;

      int width = b.Width;
      int height = b.Height;

      // 目标图像
      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        Random myRand = new Random();
        int X, Y;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 获取当前像素点周围的一随机点
            X = x + myRand.Next(-degree, degree);
            Y = y + myRand.Next(-degree, degree);

            // 如果越界，则用图像边界上的点代替
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            // 指向源图像的随机点
            src = (byte*)srcScan0 + (Y * stride) + (X * BPP);

            dst[3] = src[3];
            dst[2] = src[2];
            dst[1] = src[1];
            dst[0] = src[0];

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Diffuse


    /// <summary>
    /// 对图像进行查找边缘处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="angle">角度[0, 360]</param>
    /// <returns></returns>
    public Bitmap FindEdge(Bitmap b, int angle)
    {
      // 角度转弧度
      double radian = (double)angle * 2.0 * Math.PI / 360.0;

      // 每一权的弧度间隔
      double pi4 = Math.PI / 4.0;

      // 对图像进行卷积变换
      Matrix3x3 m = new Matrix3x3();

      m.TopLeft = Convert.ToInt32(Math.Cos(radian + pi4) * 256);
      m.TopMid = Convert.ToInt32(Math.Cos(radian + 2.0 * pi4) * 256);
      m.TopRight = Convert.ToInt32(Math.Cos(radian + 3.0 * pi4) * 256);

      m.MidLeft = Convert.ToInt32(Math.Cos(radian) * 256);
      m.Center = 0;
      m.MidRight = Convert.ToInt32(Math.Cos(radian + 4.0 * pi4) * 256);

      m.BottomLeft = Convert.ToInt32(Math.Cos(radian - pi4) * 256);
      m.BottomMid = Convert.ToInt32(Math.Cos(radian - 2.0 * pi4) * 256);
      m.BottomRight = Convert.ToInt32(Math.Cos(radian - 3.0 * pi4) * 256);

      m.Scale = 256;

      b = m.Convolute(b);

      // 对图像进行负像处理
      Adjustment a = new Adjustment();
      b = a.Invert(b);

      return b;
    } // end of FindEdge


    /// <summary>
    /// 照亮边缘
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap GlowingEdge(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      // 图像实际处理区域
      // 不考虑最右 1 列，和最下 1 行
      int rectTop = 0;
      int rectBottom = height - 1;
      int rectLeft = 0;
      int rectRight = width - 1;

      unsafe
      {
        int stride = data.Stride;
        int offset = stride - width * BPP;

        byte* p = (byte*)data.Scan0;
        byte* bottom = p + stride;
        byte* right = p + BPP;

        int pixel;

        for (int y = rectTop; y < rectBottom; y++)
        {
          for (int x = rectLeft; x < rectRight; x++)
          {
            // 指向当前点下方像素点
            bottom = p + stride;

            // 指向当前点右边像素点
            right = p + BPP;

            for (int i = 0; i < 3; i++)
            {
              pixel = 
                (p[i] - bottom[i]) * (p[i] - bottom[i]) + 
                (p[i] - right[i]) * (p[i] - right[i]);
              pixel = (int)(Math.Sqrt(pixel) * 2);

              if (pixel < 0) pixel = 0;
              if (pixel > 255) pixel = 255;

              p[i] = (byte)pixel;
            } // i

            // 向后移一像素
            p += BPP;
          } // x

          // 移向下一行
          // 这里得注意要多移 1 列，因最右列不必处理
          p += offset + BPP;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of GlowingEdge


    /// <summary>
    /// 灯光
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="power">光照强度</param>
    /// <returns></returns>
    public Bitmap Lighting(Bitmap b, int power)
    {
      int width = b.Width;
      int height = b.Height;

      // 光源中心点 X, Y 坐标
      int x0 = width / 2;
      int y0 = height / 2;

      // 光源辐射半径
      int radius = (int)Math.Sqrt(x0 * x0 + y0 * y0);

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int pixel, brightness;

        int distance, squareDistance;
        int squareRadius = radius * radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 求当前点与光源中心点的距离
            squareDistance = (x - x0) * (x - x0) + (y - y0) * (y - y0);

            if (squareDistance < squareRadius)
            {
              distance = (int)Math.Sqrt(squareDistance);

              // 当前点的光照强度
              brightness = power * (radius - distance) / radius;

              for (int i = 0; i < 3; i++)
              { 
                pixel = p[i] + brightness;
                if (pixel > 255) pixel = 255;
                p[i] = (byte)pixel;
              } // i
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Lighting


    /// <summary>
    /// 对图像进行马赛克特效处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">块大小[1, 32]</param>
    /// <returns></returns>
    public Bitmap Mosaic(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 32) degree = 32;

      // 块大小
      int block = degree;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int srcStride = srcData.Stride;
      int dstStride = dstData.Stride;

      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        // 方块累加和
        int sum = 0;

        // 方块积
        int product = block * block;

        byte brightness = 0;

        for (int y = 0; y < height; y += block)
        {
          for (int x = 0; x < width; x += block)
          {
            // 处理 B, G, R 三分量，不考虑 A 分量
            for (int i = 0; i < 3; i++)
            {
              // 求小块内的亮度平均值
              sum = 0;
              for (int yB = 0; yB < block && (y + yB) < height; yB++)
              {
                for (int xB = 0; xB < block && (x + xB) < width; xB++)
                {
                  src = (byte*)srcScan0 + ((y + yB) * srcStride) + ((x + xB) * BPP);

                  sum += src[i];
                } // xB
              } // yB
              brightness = (byte)(sum / product);

              // 将亮度值赋予目标图像
              for (int yB = 0; yB < block && (y + yB) < height; yB++)
              {
                for (int xB = 0; xB < block && (x + xB) < width; xB++)
                {
                  dst = (byte*)dstScan0 + ((y + yB) * dstStride) + ((x + xB) * BPP);

                  dst[i] = brightness;
                } // xB
              } // yB
            } // i
          } // x
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Mosaic


    /// <summary>
    /// 油画
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="brushSize">画刷尺寸[1, 8]</param>
    /// <param name="coarseness">粗糙度[1, 255]</param>
    /// <returns></returns>
    public Bitmap OilPainting(Bitmap b, int brushSize, byte coarseness)
    {
      if (brushSize < 1) brushSize = 1;
      if (brushSize > 8) brushSize = 8;

      if (coarseness < 1) coarseness = 1;
      if (coarseness > 255) coarseness = 255;

      int width = b.Width;
      int height = b.Height;

      int lenArray = coarseness + 1;
      int[] CountIntensity = new int[lenArray];
      uint[] RedAverage = new uint[lenArray];
      uint[] GreenAverage = new uint[lenArray];
      uint[] BlueAverage = new uint[lenArray];
      uint[] AlphaAverage = new uint[lenArray];

      // 产生灰度数组
      GrayProcessing gp = new GrayProcessing();
      Bitmap grayImage = gp.Gray( (Bitmap)b.Clone(), GrayProcessing.GrayMethod.WeightAveraging );
      byte[,] Gray = gp.Image2Array(grayImage);
      grayImage.Dispose();

      // 目标图像
      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        for (int y = 0; y < height; y++)
        {
          // 油画渲染范围上下边界
          int top = y - brushSize;
          int bottom = y + brushSize + 1;

          if (top < 0) top = 0;
          if (bottom >= height) bottom = height - 1;

          for (int x = 0; x < width; x++)
          {
            // 油画渲染范围左右边界
            int left = x - brushSize;
            int right = x + brushSize + 1;

            if (left < 0) left = 0;
            if (right >= width) right = width - 1;

            // 初始化数组
            for (int i = 0; i < lenArray; i++)
            {
              CountIntensity[i] = 0;
              RedAverage[i] = 0;
              GreenAverage[i] = 0;
              BlueAverage[i] = 0;
              AlphaAverage[i] = 0;
            } // i


            // 下面这个内循环类似于外面的大循环
            // 也是油画特效处理的关键部分
            src = (byte*)srcScan0 + top * stride + left * BPP;
            int offsetBlock = stride - (right - left) * BPP;

            for (int j = top; j < bottom; j++)
            {
              for (int i = left; i < right; i++)
              {
                byte intensity = (byte)(coarseness * Gray[i, j] / 255);
                CountIntensity[intensity]++;

                AlphaAverage[intensity] += src[3];
                RedAverage[intensity] += src[2];
                GreenAverage[intensity] += src[1];
                BlueAverage[intensity] += src[0];

                src += BPP;
              } // i

              src += offsetBlock;
            } // j

            // 求最大值，并记录下数组索引
            byte chosenIntensity = 0;
            int maxInstance = CountIntensity[0];
            for (int i = 1; i < lenArray; i++)
            {
              if (CountIntensity[i] > maxInstance)
              {
                chosenIntensity = (byte)i;
                maxInstance = CountIntensity[i];
              }
            } // i

            dst[3] = (byte)(AlphaAverage[chosenIntensity] / maxInstance);
            dst[2] = (byte)(RedAverage[chosenIntensity] / maxInstance);
            dst[1] = (byte)(GreenAverage[chosenIntensity] / maxInstance);
            dst[0] = (byte)(BlueAverage[chosenIntensity] / maxInstance);

            dst += BPP;
          } // x

          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of OilPainting


    /// <summary>
    /// 曝光
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Solarize(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            if (p[0] < 128) p[0] = (byte)(p[0] ^ 0xFF); // B
            if (p[1] < 128) p[1] = (byte)(p[1] ^ 0xFF); // G
            if (p[2] < 128) p[2] = (byte)(p[2] ^ 0xFF); // R

            p += BPP;
          }// x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Solarize


    /************************************************************
     * 
     * 图像融合
     * 
     ************************************************************/


    /// <summary>
    /// 图像融合
    /// </summary>
    /// <param name="bgImage">背景图像</param>
    /// <param name="fgImage">前景图像</param>
    /// <param name="transparency">前景透明度[0, 255]</param>
    /// <returns></returns>
    public Bitmap Inosculate(Bitmap bgImage, Bitmap fgImage, byte transparency)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      BitmapData bgData = bgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData fgData = fgImage.LockBits(new Rectangle(0, 0, width, height), 
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* bg = (byte*)bgData.Scan0;
        byte* fg = (byte*)fgData.Scan0;
        
        int offset = bgData.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            if (fg[3] != 0)
            {
              // pixel = FG * transparency / 255 + BG * (255 - transparency) / 255
              bg[0] = (byte)((fg[0] - bg[0]) * transparency / 255 + bg[0]);
              bg[1] = (byte)((fg[1] - bg[1]) * transparency / 255 + bg[1]);
              bg[2] = (byte)((fg[2] - bg[2]) * transparency / 255 + bg[2]);
            }

            bg += BPP;
            fg += BPP;
          } // x

          bg += offset;
          fg += offset;
        } // y
      }

      bgImage.UnlockBits(bgData);
      fgImage.UnlockBits(fgData);

      Bitmap dstImage = (Bitmap)bgImage.Clone();

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Inosculate


    /// <summary>
    /// 将两张图像合成为魔术图
    /// </summary>
    /// <param name="bgImage">背景图像</param>
    /// <param name="fgImage">前景图像</param>
    /// <param name="transparency">透明度[0, 255]，即隐藏状况</param>
    /// <param name="contrast">前景图与背景图的对比度[-100, 100]</param>
    public Bitmap Magic(Bitmap bgImage, Bitmap fgImage, byte transparency, int contrast)
    {
      Adjustment a = new Adjustment();
      Effect e = new Effect();

      // 按像素交错翻转
      bgImage = a.Interleaving(bgImage);

      // 处理对比度
      bgImage = a.Contrast(bgImage, contrast);

      // 前景和背景进行混合处理
      Bitmap dstImage = e.Inosculate(bgImage, fgImage, transparency);

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Magic


    /************************************************************
     * 
     * 自定义、去红眼、艺术字符
     * 
     ************************************************************/


    /// <summary>
    /// 自定义
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="sequence">卷积核元素序列</param>
    /// <returns></returns>
    public Bitmap Custom(Bitmap b, int[] sequence)
    {
      MatrixNxN m = new MatrixNxN();
      m.Sequence = sequence;

      return m.Convolute(b);
    } // end of Custom


    /// <summary>
    /// 自定义
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="kernel">卷积核</param>
    /// <param name="scale">缩放比例</param>
    /// <param name="offset">偏移量</param>
    /// <returns></returns>
    public Bitmap Custom(Bitmap b, int[,] kernel, int scale, int offset)
    {
      MatrixNxN m = new MatrixNxN();
      m.Kernel = kernel;
      m.Scale = scale;
      m.Offset = offset;

      return m.Convolute(b);
    } // end of Custom


    /// <summary>
    /// 去除红眼
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="tolerence">容差[0, 100]</param>
    /// <param name="red">红色分量[0, 255]</param>
    /// <param name="green">绿色分量[0, 255]</param>
    /// <param name="blue">蓝色分量[0, 255]</param>
    /// <returns></returns>
    public Bitmap RedEyeRemoval(Bitmap b, int tolerence, int red, int green, int blue)
    {
      if (tolerence < 0) tolerence = 0;
      if (tolerence > 100) tolerence = 100;

      if (red < 0) red = 0;
      if (red > 255) red = 255;
      if (green < 0) green = 0;
      if (green > 255) green = 255;
      if (blue < 0) blue = 0;
      if (blue > 255) blue = 255;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        byte R, G, B;
        int gray;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            // R 分量与 G, B 两分量差距越大，则红色成分越多
            int difference = R - (G > B ? G : B);

            // 如果当前像素颜色分量差距在指定容差范围内，并且红色成份足够高
            // 则认为当前像素需要进行红眼去除处理
            if ((difference > tolerence) && (R > 100))
            {
              // 求灰度
              gray = (19661 * R + 38666 * G + 7209 * B) >> 16;

              // 用指定的颜色替换掉红色
              p[2] = (byte)(gray * red / 255);
              p[1] = (byte)(gray * green / 255);
              p[0] = (byte)(gray * blue / 255);
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of RedEyeRemoval


    /// <summary>
    /// 艺术文字，将图片转换为字符
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="text">待显示的文字</param>
    /// <param name="blockWidth">块宽</param>
    /// <param name="blockHeight">块高</param>
    /// <returns></returns>
    public string Image2Text(Bitmap b, string text, int blockWidth, int blockHeight)
    {
      int width = b.Width;
      int height = b.Height;

      // 首先将图像灰度化
      GrayProcessing gp = new GrayProcessing();
      b = gp.Gray(b, GrayProcessing.GrayMethod.WeightAveraging);
      byte[,] Gray = gp.Image2Array(b);

      // 用于显示的灰度字符
      char[] ArtChar = text.ToCharArray();
      int len = ArtChar.Length;

      // 统计每个字符的点数
      int[] CharDots = new int[len];
      Watermark w = new Watermark();
      for (int i = 0; i < len; i++)
      {
        CharDots[i] = w.CountTextDots(ArtChar[i]);
      } // i

      // 对字符点数进行冒泡法排序
      for (int i = 0; i < len - 1; i++)
      {
        for (int j = i + 1; j < len; j++)
        {
          if (CharDots[j] < CharDots[i])
          {
            // 交换点数
            int t = CharDots[j];
            CharDots[j] = CharDots[i];
            CharDots[i] = t;

            // 交换字符
            char c = ArtChar[j];
            ArtChar[j] = ArtChar[i];
            ArtChar[i] = c;
          }
        } // j
      } // i

      // 最大点数
      int maxGray = CharDots[len - 1];

      // 累加块灰度
      int sum = 0;

      // 块积
      double product = maxGray / (double)(blockWidth * blockHeight * 255);

      // 图案化后的字符串，即返回字符串
      string artString = "";

      for (int y = 0; y < height; y += blockHeight)
      {
        for (int x = 0; x < width; x += blockWidth)
        {
          sum = 0;

          for (int yB = 0; yB < blockHeight && (y + yB) < height; yB++)
          {
            for (int xB = 0; xB < blockWidth && (x + xB) < width; xB++)
            {
              // 累加小块内的灰度
              sum += Gray[x + xB, y + yB];
            } // xB
          } // yB

          // gray = maxGray * (sum / (blockWidth * blockHeight)) / 255
          int gray = (int)(sum * product);

          // 寻找灰度最接近的字符
          int k = 0;
          while (CharDots[k] < (maxGray - gray))
            k++;

          // 进行灰度映射
          artString += ArtChar[k];
        } // x

        artString += "\r\n";
      } // y

      return artString;
    } // end of Image2Text


  }
}
