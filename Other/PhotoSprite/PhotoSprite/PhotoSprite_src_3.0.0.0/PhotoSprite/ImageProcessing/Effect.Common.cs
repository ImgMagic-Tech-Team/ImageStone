using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 滤镜特效类
  /// </summary>
  public partial class Effect : ImageInfo
  {
    /// <summary>
    /// 八方向枚举
    /// </summary>
    public enum Direction : int
    {
      /// <summary>
      /// 北，North
      /// </summary>
      N = 1,

      /// <summary>
      ///  东北，North east
      /// </summary>
      NE = 2,

      /// <summary>
      /// 东，East
      /// </summary>
      E = 3,

      /// <summary>
      /// 东南，South east
      /// </summary>
      SE = 4,

      /// <summary>
      /// 南，South
      /// </summary>
      S = 5,

      /// <summary>
      /// 西南，South west
      /// </summary>
      SW = 6,

      /// <summary>
      /// 西，West
      /// </summary>
      W = 7,

      /// <summary>
      /// 西北，North west
      /// </summary>
      NW = 8
    }


    /************************************************************
     * 
     * 平滑、模糊、锐化、浮雕、杂点
     * 
     ************************************************************/


    /// <summary>
    /// 对图像进行平滑处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Smooth(Bitmap b)
    {
      //    1  1  1
      //    1  1  1
      //    1  1  1 / 9
      Matrix3x3 m = new Matrix3x3();
      m.Init(1);
      m.Scale = 9;

      return m.Convolute(b);
    } // end of Smooth


    /// <summary>
    /// 对图像采用高斯模板进行模糊处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap GaussBlur(Bitmap b)
    {
      //    1  2  1
      //    2  4  2
      //    1  2  1 / 16
      Matrix3x3 m = new Matrix3x3();
      m.Init(1);
      m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
      m.Center = 4;
      m.Scale = 16;

      return m.Convolute(b);
    } // end of GaussBlur


    /// <summary>
    /// 运动模糊
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="angle">角度方向[0, 360]</param>
    /// <param name="distance">距离[1, 200]</param>
    /// <returns></returns>
    public Bitmap MotionBlur(Bitmap b, int angle, int distance)
    {
      angle %= 360;

      if (distance < 1) distance = 1;
      if (distance > 200) distance = 200;

      // 弧度、距离
      double radian = ((double)angle + 180.0) * Math.PI / 180.0;
      int dx = Convert.ToInt32((double)distance * Math.Cos(radian));
      int dy = Convert.ToInt32((double)distance * Math.Sin(radian));

      // 首点、尾点
      Point start = new Point(-dx / 2, dy / 2);
      Point end = new Point(dx / 2, -dy / 2);

      // 获取由首尾点组成的线段中的所有点的集合
      Point[] points = Function.GetLinePoints(start, end);

      int width = b.Width;
      int height = b.Height;

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

        int X = 0, Y = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            long bSum = 0;
            long gSum = 0;
            long rSum = 0;
            long aSum = 0;
            int div = 1;
            int validPoint = 0;

            foreach (Point point in points)
            {
              X = x + point.X;
              Y = y + point.Y;

              if ((X >= 0 && X < width) && (Y >= 0 && Y < height))
              {
                src = (byte*)srcScan0 + Y * stride + X * BPP;

                if (src[3] > 0)
                {
                  bSum += src[0] * src[3]; // B * A
                  gSum += src[1] * src[3]; // G * A
                  rSum += src[2] * src[3]; // R * A
                  aSum += src[3];          // A
                  div += src[3];
                }

                validPoint++;
              }
            } // point

            dst[3] = (byte)(aSum /= validPoint);
            dst[2] = (byte)(rSum /= div);
            dst[1] = (byte)(gSum /= div);
            dst[0] = (byte)(bSum /= div);

            dst += BPP;
          } // x

          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of MotionBlur


    /// <summary>
    /// 径向模糊
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="angle">径向角度[0, 360]</param>
    /// <returns></returns>
    public Bitmap RadialBlur(Bitmap b, int angle)
    {
      // 角度、弧度
      angle %= 360;
      double radian = (double)angle * Math.PI / 180.0;
      double radian2 = radian * radian;

      int width = b.Width;
      int height = b.Height;

      int midX = width / 2;
      int midY = height / 2;

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

        int alpha, red, green, blue;
        int validPoint;
        int X = 0, Y = 0;
        double xOffset, yOffset, xOffsetCCW, yOffsetCCW, xOffsetCW, yOffsetCW;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 初始化颜色统计变量
            alpha = dst[3];
            red   = dst[2] * dst[3];
            green = dst[1] * dst[3];
            blue  = dst[0] * dst[3];
            validPoint = 1;

            xOffsetCCW = xOffsetCW = x - midX;
            yOffsetCCW = yOffsetCW = y - midY;

            for (int i = 0; i < 64; i++)
            {
              // 逆时针（正向）偏移
              xOffset = xOffsetCCW;
              yOffset = yOffsetCCW;
              xOffsetCCW = xOffset - radian * yOffset / 64.0 - radian2 * xOffset / 8192.0;
              yOffsetCCW = yOffset + radian * xOffset / 64.0 - radian2 * yOffset / 8192.0;

              X = (int)xOffsetCCW + midX;
              Y = (int)yOffsetCCW + midY;

              if ((X >= 0 && X < width) && (Y >= 0 && Y < height))
              {
                src = (byte*)srcScan0 + Y * stride + X * BPP;

                alpha += src[3];          // A
                red   += src[2] * src[3]; // R * A
                green += src[1] * src[3]; // G * A
                blue  += src[0] * src[3]; // B * A
                validPoint++;
              }

              // 顺时针（反向）偏移
              xOffset = xOffsetCW;
              yOffset = yOffsetCW;
              xOffsetCW = xOffset + radian * yOffset / 64.0 - radian2 * xOffset / 8192.0;
              yOffsetCW = yOffset - radian * xOffset / 64.0 - radian2 * yOffset / 8192.0;

              X = (int)xOffsetCW + midX;
              Y = (int)yOffsetCW + midY;

              if ((X >= 0 && X < width) && (Y >= 0 && Y < height))
              {
                src = (byte*)srcScan0 + Y * stride + X * BPP;

                alpha += src[3];          // A
                red   += src[2] * src[3]; // R * A
                green += src[1] * src[3]; // G * A
                blue  += src[0] * src[3]; // B * A
                validPoint++;
              }
            } // i

            blue  = blue  / alpha;
            green = green / alpha;
            red   = red   / alpha;
            alpha = alpha / validPoint;

            if (alpha > 255) alpha = 255;
            if (red   > 255) red   = 255;
            if (green > 255) green = 255;
            if (blue  > 255) blue  = 255;

            dst[3] = (byte)alpha;
            dst[2] = (byte)red;
            dst[1] = (byte)green;
            dst[0] = (byte)blue;

            dst += BPP;
          } // x

          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of RadialBlur


    /// <summary>
    /// 对图像进行锐化处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Sharpen(Bitmap b)
    {
      //    0 -1  0
      //   -1  5 -1
      //    0 -1  0 / 1
      Matrix3x3 m = new Matrix3x3();
      m.Init(0);
      m.Center = 5;
      m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = -1;
      m.Scale = 1;

      return m.Convolute(b);
    } // end of Sharpen


    /// <summary>
    /// 对图像进行加强锐化处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap SharpenMore(Bitmap b)
    {
      //   -1 -1 -1
      //   -1  9 -1
      //   -1 -1 -1 / 1
      Matrix3x3 m = new Matrix3x3();
      m.Init(-1);
      m.Center = 9;

      return m.Convolute(b);
    } // end of SharpenMore


    /// <summary>
    /// 对图像进行锐化处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">锐化度[1, 100]</param>
    /// <returns></returns>
    public Bitmap Sharpen(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 100) degree = 100;

      int width = b.Width;
      int height = b.Height;

      //   -1 -1 -1
      //   -1  8 -1
      //   -1 -1 -1 / 1
      Matrix3x3 m = new Matrix3x3();
      m.Init(-1);
      m.Center = 8;
      Bitmap dstImage = m.Convolute((Bitmap)b.Clone());

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height), 
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height), 
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;
        int offset = srcData.Stride - width * BPP;

        int pixel = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 处理 B, G, R 三分量，不考虑 A 分量
            for (int i = 0; i < 3; i++)
            {
              pixel = src[i] + dst[i] * degree / 10;

              if (pixel < 0) pixel = 0;
              if (pixel > 255) pixel = 255;

              dst[i] = (byte)pixel;
            } // i

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
    } // end of Sharpen


    /// <summary>
    /// 对图像进行钝化蒙板处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">钝化度[1, 100]</param>
    /// <returns></returns>
    public Bitmap UnsharpMask(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 100) degree = 100;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();
      for (int i = 0; i < degree; i++)
      {
        // 进行高斯模糊处理
        dstImage = GaussBlur(dstImage);
      } // i

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;
        int offset = srcData.Stride - width * BPP;

        int pixel = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 处理 B, G, R 三分量，不考虑 A 分量
            for (int i = 0; i < 3; i++)
            {
              pixel = src[i] * 2 - dst[i];

              if (pixel < 0) pixel = 0;
              if (pixel > 255) pixel = 255;

              dst[i] = (byte)pixel;
            } // i

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
    } // end of UnsharpMask


    /// <summary>
    /// 对图像进行浮雕模板处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Emboss(Bitmap b)
    {
      // -1 0 -1
      //  0 4  0
      // -1 0 -1 / 1 + 127
      Matrix3x3 m = new Matrix3x3();
      m.Init(-1);
      m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 0;
      m.Center = 4;
      m.Offset = 127;

      return m.Convolute(b);
    } // end of Emboss


    /// <summary>
    /// 对图像进行八方向浮雕处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="direction">雕刻方向</param>
    /// <returns></returns>
    public Bitmap Emboss(Bitmap b, Direction direction)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      // 图像实际处理区域
      // 注意：图像最外围一圈不处理
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

        byte* reference = src;

        // 方向选择
        switch (direction)
        {
          case Direction.N:
            reference -= stride;
            break;

          case Direction.NE:
            reference -= stride;
            reference += BPP;
            break;

          case Direction.E:
            reference += BPP;
            break;

          case Direction.SE:
            reference += stride;
            reference += BPP;
            break;

          case Direction.S:
            reference += stride;
            break;

          case Direction.SW:
            reference += stride;
            reference -= BPP;
            break;

          case Direction.W:
            reference -= BPP;
            break;

          case Direction.NW:
            reference -= stride;
            reference -= BPP;
            break;
        } // switch

        for (int y = rectTop; y < rectBottom; y++)
        {
          // 移向每行第一列
          src += BPP;
          reference += BPP;
          dst += BPP;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // 当前点与周围八点中用户选定方向处的像素差值
            for (int i = 0; i < 3; i++)
            {
              pixel = src[i] - reference[i] + 128;
              if (pixel < 0) pixel = -pixel;
              if (pixel < 64) pixel = 64;
              if (pixel > 255) pixel = 255;

              dst[i] = (byte)pixel;
            } // i
            dst[3] = src[3];

            // 向后移一像素
            src += BPP;
            reference += BPP;
            dst += BPP;
          } // x

          // 移向下一行
          // 这里得注意要多移 1 列，因最右边不必处理
          src += (offset + BPP);
          reference += (offset + BPP);
          dst += (offset + BPP);
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Emboss


    /// <summary>
    /// 对图像进行彩色浮雕处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="angle">角度[0, 360]</param>
    /// <returns></returns>
    public Bitmap Relief(Bitmap b, int angle)
    {
      // 角度转弧度
      double radian = (double)angle * Math.PI / 180.0;

      // 每一权的弧度间隔
      double pi4 = Math.PI / 4.0;

      // 对图像进行卷积变换
      Matrix3x3 m = new Matrix3x3();

      m.TopLeft = Convert.ToInt32(Math.Cos(radian + pi4) * 256);
      m.TopMid = Convert.ToInt32(Math.Cos(radian + 2.0 * pi4) * 256);
      m.TopRight = Convert.ToInt32(Math.Cos(radian + 3.0 * pi4) * 256);

      m.MidLeft = Convert.ToInt32(Math.Cos(radian) * 256);
      m.Center = 256;
      m.MidRight = Convert.ToInt32(Math.Cos(radian + 4.0 * pi4) * 256);

      m.BottomLeft = Convert.ToInt32(Math.Cos(radian - pi4) * 256);
      m.BottomMid = Convert.ToInt32(Math.Cos(radian - 2.0 * pi4) * 256);
      m.BottomRight = Convert.ToInt32(Math.Cos(radian - 3.0 * pi4) * 256);

      m.Scale = 256;

      return m.Convolute(b);
    } // end of Relief


    /// <summary>
    /// 对图像进行灰度浮雕处理
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="angle">角度[0, 360]</param>
    /// <returns></returns>
    public Bitmap Emboss(Bitmap b, int angle)
    {
      // 角度转弧度
      double radian = (double)angle * Math.PI / 180.0;

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
      m.Offset = 128;

      b = m.Convolute(b);

      // 对图像进行灰度变换
      GrayProcessing gp = new GrayProcessing();
      b = gp.Gray(b, GrayProcessing.GrayMethod.WeightAveraging);

      return b;
    } // end of Emboss


    /// <summary>
    /// 新增杂点
    /// </summary>
    /// <param name="b">位图</param>
    /// <param name="degree">杂点幅度[0, 255]</param>
    /// <returns></returns>
    public Bitmap AddNoise(Bitmap b, int degree)
    {
      if (degree < 0) degree = 0;
      if (degree > 255) degree = 255;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, noise;
        Random myRand = new Random();

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            noise = myRand.Next(-degree, degree);
            R = p[2] + noise;
            G = p[1] + noise;
            B = p[0] + noise;

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
          }
          p += offset;
        }
      }
      b.UnlockBits(data);

      return b;
    } // end of AddNoise


    /// <summary>
    /// 撒雪花，椒盐噪声
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">抛撒概率[0, 100]</param>
    /// <returns></returns>
    public Bitmap Sprinkle(Bitmap b, int degree)
    {
      if (degree < 0) degree = 0;
      if (degree > 100) degree = 100;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        Random myRand = new Random();

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            if (myRand.Next(0, 100) < degree)
            {
              p[2] = (byte)myRand.Next(0, 255);
              p[1] = (byte)myRand.Next(0, 255);
              p[0] = (byte)myRand.Next(0, 255);
            }

            p += BPP;
          } // x
          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Sprinkle


  } // end of class Effect


}
