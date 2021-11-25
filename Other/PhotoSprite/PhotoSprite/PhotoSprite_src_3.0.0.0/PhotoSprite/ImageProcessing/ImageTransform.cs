using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 图像变换类
  /// </summary>
  public class ImageTransform : ImageInfo
  {
    /// <summary>
    /// 未知区域设置模式
    /// </summary>
    public enum AreasMode
    {
      /// <summary>
      /// 透明
      /// </summary>
      Transparent,

      /// <summary>
      /// 重复边缘像素
      /// </summary>
      RepeatEdgePixels,

      /// <summary>
      /// 四周环绕
      /// </summary>
      WrapAround
    }

    /// <summary>
    /// 修整模式
    /// </summary>
    public enum TrimMode : int
    { 
      /// <summary>
      /// 不修整任何边
      /// </summary>
      None = 0,

      /// <summary>
      /// 修整上边
      /// </summary>
      Top = 1,

      /// <summary>
      /// 修整下边
      /// </summary>
      Bottom = 2,

      /// <summary>
      /// 修整左边
      /// </summary>
      Left = 4,

      /// <summary>
      /// 修整右边
      /// </summary>
      Right = 8
    }


    /************************************************************
     * 
     * 平移、尺寸、裁剪
     * 旋转、翻转、转置、倾斜、修整
     * 
     ************************************************************/

    /// <summary>
    /// 图像平移
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="horizontal">水平偏移量</param>
    /// <param name="vertical">垂直偏移量</param>
    /// <param name="areaMode">平移后图像留下的未知区域设置方式</param>
    /// <returns></returns>
    public Bitmap Translate(Bitmap b, int horizontal, int vertical, AreasMode areaMode)
    {
      int width = b.Width;
      int height = b.Height;

      horizontal %= width;
      vertical %= height;

      Bitmap dstImage = new Bitmap(width, height);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      // 图像偏移部分
      Point diagonal = new Point(horizontal, vertical);
      g.DrawImage(b, diagonal);

      switch (areaMode)
      {
        case AreasMode.WrapAround:
          if (horizontal < 0)
            diagonal.X += width;
          else
            diagonal.X -= width;
          // 水平移出部分
          g.DrawImage(b, diagonal);

          if (vertical < 0)
            diagonal.Y += height;
          else
            diagonal.Y -= height;
          // 对角移出部分
          g.DrawImage(b, diagonal);

          // 垂直移出部分
          diagonal.X = horizontal;
          g.DrawImage(b, diagonal);
          break;

        case AreasMode.RepeatEdgePixels:
          // 水平移出部分
          diagonal.X += (horizontal < 0 ? width : -width);
          g.DrawImage(b,
            new Rectangle(diagonal.X, diagonal.Y, width, height),
            new Rectangle(horizontal < 0 ? width - 1 : 0, 0, 1, height),
            GraphicsUnit.Pixel
            );

          // 对角移出部分
          diagonal.Y += (vertical < 0 ? height : -height);
          g.DrawImage(b,
            new Rectangle(diagonal.X, diagonal.Y, width, height),
            new Rectangle(horizontal < 0 ? width - 1 : 0, vertical < 0 ? height - 1 : 0, 1, 1),
            GraphicsUnit.Pixel
            );

          // 垂直移出部分
          diagonal.X = horizontal;
          g.DrawImage(b,
           new Rectangle(diagonal.X, diagonal.Y, width, height),
           new Rectangle(0, vertical < 0 ? height - 1 : 0, width, 1),
           GraphicsUnit.Pixel
           );
          break;

        case AreasMode.Transparent:
          break;
      } // switch


      g.Save();
      g.Dispose();

      b.Dispose();

      return dstImage;
    } // end of Translate


    /// <summary>
    /// 图像尺寸调节
    /// </summary>
    /// <param name="b">原始图像</param>
    /// <param name="dstWidth">目标宽度</param>
    /// <param name="dstHeight">目标高度</param>
    /// <returns></returns>
    public Bitmap Resize(Bitmap b, int dstWidth, int dstHeight)
    {
      Bitmap dstImage = new Bitmap(dstWidth, dstHeight);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      // 设置插值模式
      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;

      // 设置平滑模式
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

      g.DrawImage(b, 
        new System.Drawing.Rectangle(0, 0, dstImage.Width, dstImage.Height), 
        new System.Drawing.Rectangle(0, 0, b.Width, b.Height), 
        System.Drawing.GraphicsUnit.Pixel);
      g.Save();
      g.Dispose();

      return dstImage;
    } // end of Resize


    /// <summary>
    /// 按指定的裁剪路径对图像进行裁剪
    /// </summary>
    /// <param name="b">原始图像</param>
    /// <param name="region">裁剪区域</param>
    /// <returns></returns>
    public Bitmap Crop(Bitmap b, Region region)
    {
      Graphics g = System.Drawing.Graphics.FromImage(b);

      // 获取区域边界
      RectangleF validRect = region.GetBounds(g);

      int x = (int)validRect.X;
      int y = (int)validRect.Y;
      int width = (int)validRect.Width;
      int height = (int)validRect.Height;

      // 对区域进行平移
      Region validRegion = region;
      validRegion.Translate(-x, -y);

      Bitmap dstImage = new Bitmap(width, height);
      Graphics dstGraphics = System.Drawing.Graphics.FromImage(dstImage);

      // 设置剪辑区域
      dstGraphics.SetClip(validRegion, CombineMode.Replace);

      // 绘图
      dstGraphics.DrawImage(b, new Rectangle(0, 0, width, height), 
        validRect, GraphicsUnit.Pixel);

      g.Dispose();
      dstGraphics.Dispose();
      b.Dispose();

      return dstImage;
    } // end of Crop


    /// <summary>
    /// 以逆时针方向为正方向对图像进行旋转
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="angle">旋转角度[0, 360]</param>
    /// <returns></returns>
    public Bitmap Rotate(Bitmap b, int angle)
    {
      angle = angle % 360;

      // 弧度转化
      double radian = angle * Math.PI / 180.0;
      double cos = Math.Cos(radian);
      double sin = Math.Sin(radian);

      // 原图宽高
      int w = b.Width;
      int h = b.Height;

      // 新图的宽高
      int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
      int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

      // 目标位图，即旋转后的图
      Bitmap dstImage = new Bitmap(W, H);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);
      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

      // 偏移量
      Point offset = new Point((W - w) / 2, (H - h) / 2);

      // 构造图像显示区域：让图像的中心点与窗口的中心点一致
      Rectangle rect = new Rectangle(offset.X, offset.Y, w, h);
      Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

      // 以图像的中心点旋转
      g.TranslateTransform(center.X, center.Y);
      g.RotateTransform(360 - angle);

      // 恢复图像在水平和垂直方向的平移
      g.TranslateTransform(-center.X, -center.Y);

      // 绘制旋转后的结果图
      g.DrawImage(b, rect);

      // 重置绘图的所有变换
      g.ResetTransform();

      g.Save();

      return dstImage;
    } // end of Rotate


    /// <summary>
    /// 对图像进行翻转变换，即镜像
    /// </summary>
    /// <param name="b">原始图像</param>
    /// <param name="isHorz">是否按水平方向进行翻转</param>
    /// <returns></returns>
    public Bitmap Flip(Bitmap b, bool isHorz)
    {
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

        if (isHorz)
        {
          // 水平翻转
          for (int y = 0; y < height; y++)
          {
            for (int x = 0; x < width; x++)
            {
              dst = (byte*)dstScan0 + (y * stride) + ((width - x - 1) * BPP);

              dst[0] = src[0]; // B
              dst[1] = src[1]; // G
              dst[2] = src[2]; // R
              dst[3] = src[3]; // A

              src += BPP;
            } // x

            src += offset;
          } // y
        }
        else
        {
          // 垂直翻转
          for (int y = 0; y < height; y++)
          {
            for (int x = 0; x < width; x++)
            {
              dst = (byte*)dstScan0 + ((height - y - 1) * stride) + (x * BPP);

              dst[0] = src[0]; // B
              dst[1] = src[1]; // G
              dst[2] = src[2]; // R
              dst[3] = src[3]; // A

              src += BPP;
            } // x

            src += offset;
          } // y
        } // isHorz
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      return dstImage;
    } // end of Flip


    /// <summary>
    /// 对图像进行转置变换
    /// </summary>
    /// <param name="b">原始图像</param>
    /// <returns></returns>
    public Bitmap Transpose(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(height, width);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, height, width),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int srcStride = srcData.Stride;
      int dstStride = dstData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = srcData.Stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            dst = (byte*)dstScan0 + (x * dstStride) + (y * BPP);

            dst[0] = src[0];
            dst[1] = src[1];
            dst[2] = src[2];
            dst[3] = src[3];

            src += BPP;
          } // x

          src += offset;
        } // y
      }

      dstImage.UnlockBits(dstData);
      b.UnlockBits(srcData);

      b.Dispose();

      return dstImage;
    } // end of Transpose


    /// <summary>
    /// 对图像进行水平方向倾斜变换，基准点为平行四边形左上顶点
    /// </summary>
    /// <param name="b">原始图像</param>
    /// <param name="horz">水平方向倾斜量</param>
    /// <returns></returns>
    public Bitmap SlantHorz(Bitmap b, int horz)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(Math.Abs(horz) + width, height);

      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

      Point[] dstPoints = new Point[3];

      // 分别计算平行四边形的左上、右上和左下三个顶点
      if (horz != 0)
      {
        if (horz > 0)
        {
          dstPoints[0] = new Point(        horz,      0);
          dstPoints[1] = new Point(width + horz,      0);
          dstPoints[2] = new Point(           0, height);
        }
        else
        {
          dstPoints[0] = new Point(    0,      0);
          dstPoints[1] = new Point(width,      0);
          dstPoints[2] = new Point(-horz, height);
        }
      }

      // 绘水平倾斜图
      g.DrawImage(b, dstPoints);

      g.Save();
      g.Dispose();

      b.Dispose();

      return dstImage;
    } // end of SlantHorz


    /// <summary>
    /// 对图像进行垂直方向倾斜变换，基准点为平行四边形左上顶点
    /// </summary>
    /// <param name="b">原始图像</param>
    /// <param name="vert">垂直方向倾斜量</param>
    /// <returns></returns>
    public Bitmap SlantVert(Bitmap b, int vert)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, Math.Abs(vert) + height);

      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

      Point[] dstPoints = new Point[3];

      // 分别计算平行四边形的左上、右上和左下三个顶点
      if (vert != 0)
      {
        if (vert > 0)
        {
          dstPoints[0] = new Point(    0,          vert);
          dstPoints[1] = new Point(width,             0);
          dstPoints[2] = new Point(    0, height + vert);
        }
        else
        {
          dstPoints[0] = new Point(    0,      0);
          dstPoints[1] = new Point(width,  -vert);
          dstPoints[2] = new Point(    0, height);
        }

      }

      // 绘水平倾斜图
      g.DrawImage(b, dstPoints);

      g.Save();
      g.Dispose();

      b.Dispose();

      return dstImage;
    } // end of SlantVert


    /// <summary>
    /// 修整
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="trimAway">修整范围</param>
    /// <returns></returns>
    public Bitmap Trim(Bitmap b, TrimMode trimAway)
    {
      int width = b.Width;
      int height = b.Height;

      // 原始图像大小
      int area = width * height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      int stride = data.Stride;
      System.IntPtr scan0 = data.Scan0;
      int offset = stride - width * BPP;

      // 修整后的有效区域
      int rectTop = 0;
      int rectBottom = height;
      int rectLeft = 0;
      int rectRight = width;

      unsafe
      {
        byte* p = (byte*)scan0;
        int i = 1;

        // 上
        if ((trimAway & TrimMode.Top) == TrimMode.Top)
        {
          p = (byte*)scan0;
          i = 1;
          while (i < area)
          {
            if (p[3] != 0)
            {
              // 获取上边界
              rectTop = i / width;
              break;
            }

            p += BPP;
            if (i % width == 0)
              p += offset;
            i++;
          } // while
        }

        // 下
        if ((trimAway & TrimMode.Bottom) == TrimMode.Bottom)
        {
          p = (byte*)scan0 + (height - 1) * stride + (width - 1) * BPP;
          i = 1;
          while (i < area)
          {
            if (p[3] != 0)
            {
              // 获取下边界
              rectBottom = height - i / width;
              break;
            }

            p -= BPP;
            if (i % width == 0)
              p -= offset;
            i++;
          } // while
        }

        // 左
        if ((trimAway & TrimMode.Left) == TrimMode.Left)
        {
          p = (byte*)scan0;
          i = 1;
          while (i < area)
          {
            if (p[3] != 0)
            {
              // 获取左边界
              rectLeft = i / height;
              break;
            }

            p += stride;
            if (i % height == 0)
            {
              p = (byte*)scan0;
              p += (i / height) * BPP;
            }
            i++;
          } // while
        }

        // 右
        if ((trimAway & TrimMode.Right) == TrimMode.Right)
        {
          p = (byte*)scan0 + (width - 1) * BPP;
          i = 1;
          while (i < area)
          {
            if (p[3] != 0)
            {
              // 获取右边界
              rectRight = width - i / height;
              break;
            }

            p += stride;
            if (i % height == 0)
            {
              p = (byte*)scan0 + (width - 1) * BPP;
              p -= (i / height) * BPP;
            }
            i++;
          } // while
        }
      }

      b.UnlockBits(data);


      // 修整有效区域后的图像
      Bitmap dst = new Bitmap(rectRight - rectLeft, rectBottom - rectTop);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
      g.DrawImage(b,
        new Rectangle(0, 0, dst.Width, dst.Height),
        new Rectangle(rectLeft, rectTop, dst.Width, dst.Height),
        GraphicsUnit.Pixel
        );
      g.Save();

      b.Dispose();

      return dst;
    } // end of Trim


  }
}
