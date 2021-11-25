using System;
using System.Drawing;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 逻辑运算类
  /// </summary>
  public class Logic : GrayProcessing
  {
    /// <summary>
    /// 逻辑运算方法
    /// </summary>
    public enum LogicMethod
    {
      /// <summary>
      /// 与运算
      /// </summary>
      And,

      /// <summary>
      /// 或运算
      /// </summary>
      Or,

      /// <summary>
      /// 异或运算
      /// </summary>
      Xor
    }

    /// <summary>
    /// 获取或设置背景区域
    /// </summary>
    public Region BackgroundRegion
    {
      get
      {
        return bgRegion;
      }
      set
      {
        bgRegion = value;
      }
    }
    private Region bgRegion = new Region();

    /// <summary>
    /// 获取或设置前景区域
    /// </summary>
    public Region ForegroundRegion
    {
      get
      {
        return fgRegion;
      }
      set
      {
        fgRegion = value;
      }
    }
    private Region fgRegion = new Region();


    /// <summary>
    /// 图像逻辑运算
    /// </summary>
    /// <param name="bgImage">二值背景</param>
    /// <param name="fgImage">二值前景</param>
    /// <param name="logicMethod">逻辑运算方法</param>
    /// <returns></returns>
    public Bitmap LogicOperate(Bitmap bgImage, Bitmap fgImage, LogicMethod logicMethod)
    {
      Bitmap dstImage = (Bitmap)bgImage.Clone();
      Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      // 计算有效区域
      Region validRegion = bgRegion;
      validRegion.Intersect(fgRegion);
      RectangleF validRect = validRegion.GetBounds(g);
      RectangleF fgRect = fgRegion.GetBounds(g);

      RegionClip bgRegionClip = new RegionClip(validRegion);
      Bitmap background = bgRegionClip.Hold((Bitmap)bgImage.Clone());

      RegionClip fgRegionClip = new RegionClip(validRegion);
      validRegion.Translate(-fgRect.X, -fgRect.Y);
      Bitmap foreground = fgRegionClip.Hold((Bitmap)fgImage.Clone());
      validRegion.Translate(fgRect.X, fgRect.Y);

      // 先将原始二值图转化为二维数组
      byte[,] bgGray = Image2Array(background);
      byte[,] fgGray = Image2Array(foreground);

      // 进行逻辑运算处理后的灰度二维数组
      byte[,] dstGray = null;

      // 进行逻辑运算
      switch (logicMethod)
      {
        case LogicMethod.And:
          dstGray = LogicAnd(bgGray, fgGray);
          break;

        case LogicMethod.Or:
          dstGray = LogicOr(bgGray, fgGray);
          break;

        case LogicMethod.Xor:
          dstGray = LogicXor(bgGray, fgGray);
          break;
      }

      // 将二值数组转化为灰度图
      Bitmap validImage = Array2Image(dstGray);

      g.DrawImage(validImage, validRect,
        new Rectangle(0, 0, (int)validRect.Width, (int)validRect.Height), GraphicsUnit.Pixel);

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of LogicOperate


    /// <summary>
    /// 图像逻辑与运算
    /// </summary>
    /// <param name="bg">背景二值化数组</param>
    /// <param name="fg">前景二值化数组</param>
    /// <returns></returns>
    private byte[,] LogicAnd(byte[,] bg, byte[,] fg)
    {
      int width = bg.GetLength(0);
      int height = bg.GetLength(1);

      // 初始化目标数组为 255，即白色
      byte[,] dst = InitArray(width, height, 255);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          dst[x, y] = (byte)(bg[x, y] & fg[x, y]);
        } // x
      } // y

      return dst;
    } // end of LogicAnd


    /// <summary>
    /// 图像逻辑或运算
    /// </summary>
    /// <param name="bg">背景二值化数组</param>
    /// <param name="fg">前景二值化数组</param>
    /// <returns></returns>
    private byte[,] LogicOr(byte[,] bg, byte[,] fg)
    {
      int width = bg.GetLength(0);
      int height = bg.GetLength(1);

      // 初始化目标数组为 255，即白色
      byte[,] dst = InitArray(width, height, 255);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          dst[x, y] = (byte)(bg[x, y] | fg[x, y]);
        } // x
      } // y

      return dst;
    } // end of LogicOr


    /// <summary>
    /// 逻辑非运算
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <returns></returns>
    public Bitmap LogicNot(Bitmap b)
    {
      // 对二值图像直接进行负像处理
      Adjustment a = new Adjustment();
      b = a.Invert(b);

      return b;
    } // end of LogicNot


    /// <summary>
    /// 图像逻辑异或运算
    /// </summary>
    /// <param name="bg">背景二值化数组</param>
    /// <param name="fg">前景二值化数组</param>
    /// <returns></returns>
    private byte[,] LogicXor(byte[,] bg, byte[,] fg)
    {
      int width = bg.GetLength(0);
      int height = bg.GetLength(1);

      // 初始化目标数组为 255，即白色
      byte[,] dst = InitArray(width, height, 255);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          dst[x, y] = (byte)(bg[x, y] ^ fg[x, y]);
        } // x
      } // y

      return dst;
    } // end of LogicXor


  }
}
