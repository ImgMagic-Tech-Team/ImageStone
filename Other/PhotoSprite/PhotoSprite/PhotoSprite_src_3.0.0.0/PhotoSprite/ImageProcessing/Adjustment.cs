using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PhotoSprite.ColorSpace;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 图像调整类
  /// </summary>
  public class Adjustment : ImageInfo
  {
    /************************************************************
     * 
     * 色彩平衡、亮度、对比度、HSL 调整、Gamma 矫正
     * 
     ************************************************************/


    /// <summary>
    /// 图像色彩平衡
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="red">红色分量[-255, 255]</param>
    /// <param name="green">绿色分量[-255, 255]</param>
    /// <param name="blue">蓝色分量[-255, 255]</param>
    /// <returns></returns>
    public Bitmap ColorBalance(Bitmap b, int red, int green, int blue)
    {
      if (red < -255) red = -255;
      if (red > 255) red = 255;
      if (green < -255) green = -255;
      if (green > 255) green = 255;
      if (blue < -255) blue = -255;
      if (blue > 255) blue = 255;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            pixel = p[2] + red;
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            pixel = p[1] + green;
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            pixel = p[0] + blue;
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of ColorBalance


    /// <summary>
    /// 图像亮度调整
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">亮度值[-255, 255]</param>
    /// <returns></returns>
    public Bitmap Brightness(Bitmap b, int degree)
    {
      if (degree < -255) degree = -255;
      if (degree > 255) degree = 255;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int pixel = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 处理像素 B, G, R 亮度三分量
            for (int i = 0; i < 3; i++)
            {
              pixel = p[i] + degree;

              if (pixel < 0) pixel = 0;
              if (pixel > 255) pixel = 255;

              p[i] = (byte)pixel;
            } // i

            p += BPP;
          }  // x
          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Brightness


    /// <summary>
    /// 图像对比度调整
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">对比度[-100, 100]</param>
    /// <returns></returns>
    public Bitmap Contrast(Bitmap b, int degree)
    {
      if (degree < -100) degree = -100;
      if (degree > 100) degree = 100;

      double pixel = 0;
      double contrast = (100.0 + degree) / 100.0;
      contrast *= contrast;

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
            // 处理指定位置像素的对比度
            for (int i = 0; i < 3; i++)
            {
              pixel = ((p[i] / 255.0 - 0.5) * contrast + 0.5) * 255;
              if (pixel < 0) pixel = 0;
              if (pixel > 255) pixel = 255;
              p[i] = (byte)pixel;
            } // i

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Contrast


    /// <summary>
    /// 按指定的色调、饱和度、亮度对图像进行调整
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="hue">色调[-180, 180]</param>
    /// <param name="saturation">饱和度[-1, 1]</param>
    /// <param name="luminance">亮度[-1, 1]</param>
    /// <returns></returns>
    public Bitmap AdjustHsl(Bitmap b, float hue, float saturation, float luminance)
    {
      if (hue < -180.0f) hue = -180.0f;
      if (hue > 180.0f) hue = 180f;
      if (saturation < -1.0f) saturation = -1.0f;
      if (saturation > 1.0f) saturation = 1.0f;
      if (luminance < -1.0f) luminance = -1.0f;
      if (luminance > 1.0f) luminance = 1.0f;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height, b.PixelFormat);

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
            HSL hsl = HSL.FromRgb(p[2], p[1], p[0]);
            hsl.Hue += hue;
            hsl.Saturation += saturation;
            hsl.Luminance += luminance;

            p[0] = hsl.GetBlue();
            p[1] = hsl.GetGreen();
            p[2] = hsl.GetRed();

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of AdjustHsl


    /// <summary>
    /// 图像 Gamma 矫正
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="degree">Gamma 矫正量[0.1, 5.0]</param>
    /// <returns></returns>
    public Bitmap GammaCorrect(Bitmap b, double degree)
    {
      if (degree < 0.1) degree = 0.1;
      if (degree > 5.0) degree = 5.0;

      byte[] Gamma = new byte[256];
      double g = 1 / degree;

      // 建立 Gamma 矫正映射表
      for (int i = 0; i < 256; i++)
      {
        int pixel = (int)((255.0 * Math.Pow(i / 255.0, g)) + 0.5);
        Gamma[i] = (byte)((pixel > 255) ? 255 : pixel);
      } // i

      // 根据 Gamma 矫正映射表对图像色彩进行映射处理
      return Mapping(b, Gamma, ChannelMode.White);
    } // end of GammaCorrect


    /************************************************************
     * 
     * 负像、交错负像、伪彩色
     * 
     ************************************************************/


    /// <summary>
    /// 反转，负像
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Invert(Bitmap b)
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
            // 与255进行异或运算，相当于按位非
            p[2] ^= 0xFF;
            p[1] ^= 0xFF;
            p[0] ^= 0xFF;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Invert


    /// <summary>
    /// 交叉反转
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Interleaving(Bitmap b)
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
            // 将指定位置的像素颜色反转
            if ((x + y) % 2 == 0)
            {
              p[0] ^= 0xFF;
              p[1] ^= 0xFF;
              p[2] ^= 0xFF;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }
      b.UnlockBits(data);

      return b;
    } // end of Interleaving


    /// <summary>
    /// 按函数曲线形式映射伪彩色
    /// </summary>
    /// <param name="b">灰度位图流</param>
    /// <returns></returns>
    public Bitmap PseudoColor(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, gray;
        R = G = B = gray = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 因为已经是灰度色，
            // 这里只取蓝色分量作为灰度进行运算
            gray = p[0];

            // 伪彩色处理
            switch (gray / 64)
            {
              case 0:
                R = 0;
                G = 4 * gray;
                B = 255;
                break;

              case 1:
                R = 0;
                G = 255;
                B = 511 - 4 * gray;
                break;

              case 2:
                R = 4 * gray - 511;
                G = 255;
                B = 0;
                break;

              case 3:
                R = 255;
                G = 1023 - 4 * gray;
                B = 0;
                break;
            }

            p[0] = (byte)B;
            p[1] = (byte)G;
            p[2] = (byte)R;

            p += BPP;
          } // x
          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of PseudoColor


    /// <summary>
    /// 按色彩表形式映射伪彩色
    /// </summary>
    /// <param name="b">灰度位图流</param>
    /// <param name="colorTable">色彩映射表</param>
    /// <returns></returns>
    public Bitmap PseudoColor(Bitmap b, Color[] colorTable)
    {
      int width = b.Width;
      int height = b.Height;

      // 颜色对照表
      int lenColorTable = colorTable.Length;
      uint[] ColorTable = new uint[lenColorTable];

      // 将颜色转换为数字
      for (int i = 0; i < lenColorTable; i++)
      {
        ColorTable[i] = (uint)((colorTable[i].A << 24) |
          (colorTable[i].R << 16) |
          (colorTable[i].G << 8) |
          (colorTable[i].B << 0));
      } // i

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int gray = 0;
        uint color = 0;

        // 避免在下面的循环中，出现色彩表索引越界
        lenColorTable--;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // 因为已经是灰度色，
            // 这里只取蓝色分量作为灰度进行运算
            gray = p[0];

            // 转化灰度级，并映射到色彩表
            color = ColorTable[lenColorTable * gray / 255];

            p[3] = (byte)(color >> 24); // A
            p[2] = (byte)(color >> 16); // R
            p[1] = (byte)(color >> 8);  // G
            p[0] = (byte)(color);       // B

            p += BPP;
          } // x
          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of PseudoColor


    /************************************************************
     * 
     * 轮换通道、提取通道、过滤通道
     * 
     ************************************************************/


    /// <summary>
    /// 轮换通道
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap RotateChannel(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;
        int offset = srcData.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            dst[2] = src[1]; // R <- G
            dst[1] = src[0]; // G <- B
            dst[0] = src[2]; // B <- R

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
    } // end of RotateChannel


    /// <summary>
    /// 通道模式
    /// </summary>
    public enum ChannelMode : int
    { 
      /// <summary>
      /// 蓝色通道
      /// </summary>
      Blue = 1,

      /// <summary>
      /// 绿色通道
      /// </summary>
      Green = 2,

      /// <summary>
      /// 红色通道
      /// </summary>
      Red = 4,

      /// <summary>
      /// Alpha 通道
      /// </summary>
      Alpha = 8,

      /// <summary>
      /// 青色 = 绿色 + 蓝色
      /// </summary>
      Cyan = 3,

      /// <summary>
      /// 品红 = 红色 + 蓝色
      /// </summary>
      Megenta = 5,

      /// <summary>
      /// 黄色 = 红色 + 绿色
      /// </summary>
      Yellow = 6,

      /// <summary>
      /// 白色 = 红色 + 绿色 + 蓝色
      /// </summary>
      White = 7
    }
    

    /// <summary>
    /// 提取通道
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="channelMode">通道模式[A, R, G, B]</param>
    /// <returns></returns>
    public Bitmap ExtractChannel(Bitmap b, ChannelMode channelMode)
    {
      int channel = (int)Math.Log((double)channelMode,2.0);
       
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;
        int offset = srcData.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            dst[0] = dst[1] = dst[2] = src[channel];

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
    } // end of ExtractChannel


    /// <summary>
    /// 过滤通道
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="channelMode">通道模式</param>
    /// <returns></returns>
    public Bitmap FilterChannel(Bitmap b, ChannelMode channelMode)
    {
      int channel = (int)channelMode;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int i = 0; i < 3; i++)
        {
          if (((int)Math.Pow(2, i) & channel) > 0)
            continue;

          p = (byte*)data.Scan0;
          for (int y = 0; y < height; y++)
          {
            for (int x = 0; x < width; x++)
            {
              p[i] = 0;

              p += BPP;
            } // x

            p += offset;
          } // y
        } // i
      }

      b.UnlockBits(data);

      return b;
    } // end of FilterChannel


    /************************************************************
     * 
     * 映射
     * 
     ************************************************************/


    /// <summary>
    /// 图像色彩映射
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="Map">映射表</param>
    /// <param name="channelMode">通道模式</param>
    /// <returns></returns>
    public Bitmap Mapping(Bitmap b, byte[] Map, ChannelMode channelMode)
    {
      int channel = (int)channelMode;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int i = 0; i < 3; i++)
        {
          if (((int)Math.Pow(2, i) & channel) == 0)
            continue;

          p = (byte*)data.Scan0;
          for (int y = 0; y < height; y++)
          {
            for (int x = 0; x < width; x++)
            {
              p[i] = Map[p[i]];

              p += BPP;
            } // x

            p += offset;
          } // y
        } // i
      }

      b.UnlockBits(data);

      return b;
    } // end of Mapping


  }
}
