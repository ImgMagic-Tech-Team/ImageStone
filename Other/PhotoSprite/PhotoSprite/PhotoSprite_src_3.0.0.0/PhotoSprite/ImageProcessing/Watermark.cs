using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 图像水印处理类
  /// </summary>
  public class Watermark : ImageInfo
  {
    /**************************************************
     * 
     * 水印文字定位
     * 
     * X 坐标、Y 坐标、对齐方式
     * 
     **************************************************/

    /// <summary>
    /// 获取或设置水印文字 X 坐标
    /// </summary>
    public int X
    {
      get
      {
        return x;
      }
      set
      {
        x = value;
      }
    }
    /// <summary>
    /// X 坐标
    /// </summary>
    int x = 0;


    /// <summary>
    /// 获取或设置水印文字 Y 坐标
    /// </summary>
    public int Y
    {
      get
      {
        return y;
      }
      set
      {
        y = value;
      }
    }
    /// <summary>
    /// Y 坐标
    /// </summary>
    int y = 0;


    /// <summary>
    /// 水印文字对齐方式
    /// </summary>
    public Function.AlignMode Align
    {
      get
      {
        return align;
      }
      set
      {
        align = value;
      }
    }
    /// <summary>
    /// 对齐方式
    /// </summary>
    Function.AlignMode align = Function.AlignMode.TopLeft;


    /**************************************************
     * 
     * 文字属性
     * 
     * 字体簇、大小、字形、字体颜色
     * 
     **************************************************/

    /// <summary>
    /// 获取或设置字体簇
    /// </summary>
    public string FontFamily
    {
      get
      {
        return fontFamily;
      }
      set
      {
        fontFamily = value;
      }
    }
    /// <summary>
    /// 字体簇
    /// </summary>
    string fontFamily = "Arial";

    /// <summary>
    /// 获取或设置字体大小
    /// </summary>
    public int FontSize
    {
      get
      {
        return fontSize;
      }
      set
      {
        fontSize = value;
      }
    }
    /// <summary>
    /// 字体大小
    /// </summary>
    int fontSize = 30;

    /// <summary>
    /// 获取或设置字体风格
    /// </summary>
    public System.Drawing.FontStyle FontWeight
    {
      get
      {
        return fontWeight;
      }
      set
      {
        fontWeight = value;
      }
    }
    /// <summary>
    /// 字体风格
    /// </summary>
    System.Drawing.FontStyle fontWeight = FontStyle.Regular;

    /// <summary>
    /// 获取或设置字体颜色
    /// </summary>
    public Color FontColor
    {
      get
      {
        return fontColor;
      }
      set
      {
        fontColor = value;
      }
    }
    /// <summary>
    /// 字体颜色
    /// </summary>
    Color fontColor = Color.Red;


    /************************************************************
     * 
     * 统计像素点、水印文字
     * 
     ************************************************************/


    /// <summary>
    /// 统计出一个字符含多少个像素点
    /// </summary>
    /// <param name="character">字符</param>
    /// <returns></returns>
    public int CountTextDots(char character)
    {
      Bitmap b = new Bitmap(20, 20);
      Graphics g = Graphics.FromImage(b);
      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

      Font font = new Font(new FontFamily("宋体"), 12, FontStyle.Regular);
      SolidBrush brush = new SolidBrush( Color.White);

      g.DrawString(character.ToString(), font, brush, 0, 0);
      g.Save();

      GrayProcessing gp = new GrayProcessing();
      byte[,] Gray = gp.BinaryArray(b, 128);

      int count = 0;

      for (int y = 0; y < 20; y++)
      {
        for (int x = 0; x < 20; x++)
        {
          // 统计白点
          if (Gray[x, y] > 128)
            count++;
        } // x
      } // y

      b.Dispose();

      return count;
    } // end of CountTextDots


    /// <summary>
    /// 水印文字
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="text">水印文字</param>
    /// <param name="isHorz">是否按水平方式在图像上写文字</param>
    /// <returns></returns>
    public Bitmap WaterText(Bitmap b, string text, bool isHorz)
    {
      int W = b.Width;
      int H = b.Height;

      Graphics g = Graphics.FromImage(b);

      //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;  // 高质量插值
      //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;   // 高质量平滑
      //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;  // 消除锯齿

      // 确定是水平还是垂直显示文字
      System.Drawing.StringFormat stringFormat = new StringFormat();
      if (!isHorz)
      {
        // 垂直显示文字
        stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
      }

      // 产生水印文字
      Font waterFont = new Font(fontFamily, fontSize, fontWeight, GraphicsUnit.Pixel);

      // 颜色
      SolidBrush brushColor = new SolidBrush(fontColor);

      // 根据指定的对齐方式对文字进行定位
      Point point = new Point(x, y);

      // 绘制水印文字
      g.DrawString(text, waterFont, brushColor, point.X, point.Y, stringFormat);

      g.Save();
      g.Dispose();

      return b;
    } // end of WaterText

    
  }
}
