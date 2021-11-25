using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PhotoSprite.ColorSpace;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 色彩改变类
  /// </summary>
  public class ColorChange : ImageInfo
  {
    /************************************************************
     * 
     * 替换颜色、擦除颜色
     * 
     ************************************************************/


    /// <summary>
    /// 将原始颜色替换为目标颜色
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="srcColor">原始颜色</param>
    /// <param name="dstColor">目标颜色</param>
    public static Bitmap ReplaceColor(Bitmap b, Color srcColor, Color dstColor)
    {
      int width = b.Width;
      int height = b.Height;

      byte srcR = srcColor.R;
      byte srcG = srcColor.G;
      byte srcB = srcColor.B;
      byte dstA = dstColor.A;
      byte dstR = dstColor.R;
      byte dstG = dstColor.G;
      byte dstB = dstColor.B;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 注意：这里暂不考虑 Alpha
            if (p[2] == srcR && p[1] == srcG && p[0] == srcB)
            {
              p[3] = dstA;
              p[2] = dstR;
              p[1] = dstG;
              p[0] = dstB;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of ReplaceColor


    /// <summary>
    /// 擦除颜色
    /// </summary>
    /// <param name="srcImage">位图流</param>
    /// <param name="rect">擦除区域</param>
    /// <returns></returns>
    public static Bitmap EraseColor(Bitmap srcImage, Rectangle rect)
    {
      if (!((rect.X >= 0 && rect.X + rect.Width <= srcImage.Width) &&
        (rect.Y >= 0 && rect.Y + rect.Height <= srcImage.Height)))
        return srcImage;

      int width = rect.Right - rect.Left;
      int height = rect.Bottom - rect.Top;
      int x0 = width / 2 + 1;
      int y0 = height / 2 + 1;
      int a = x0;
      int b = y0;
      int A = a * a;
      int B = b * b;
      int C = A * B;

      BitmapData data = srcImage.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            if ((B * (x - x0) * (x - x0) + A * (y - y0) * (y - y0)) <= C)
            {
              p[3] = p[2] = p[1] = p[0] = 0;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      srcImage.UnlockBits(data);

      return srcImage;
    } // end of EraseColor


  }
}
