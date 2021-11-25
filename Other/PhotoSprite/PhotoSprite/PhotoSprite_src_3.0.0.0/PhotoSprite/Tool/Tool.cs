using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PhotoSprite.Tool
{
  public class Tool
  {
    protected Color foreColor = Color.Red;
    protected Color backColor = Color.Transparent;
    protected int fontSize = 12;
    protected FontFamily fontFamily;
    protected FontStyle fontStyle;
    protected int brushWidth = 1;
    protected int tolerance = 30;
    protected HatchStyle hatchStyle;
    protected bool hasBrushStyle = false;

    /// <summary>
    /// 获取或设置画刷图案
    /// </summary>
    public HatchStyle FillStyle
    {
      get
      {
        return hatchStyle;
      }
      set
      {
        hatchStyle = value;
      }
    }

    /// <summary>
    /// 获取或设置一个 bool 值，以指定画刷是否含有图案
    /// </summary>
    public bool HasBrushStyle
    {
      get
      {
        return hasBrushStyle;
      }
      set
      {
        hasBrushStyle = value;
      }
    }

    /// <summary>
    /// 获取或设置字体簇
    /// </summary>
    public FontFamily Family
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
    /// 获取或设置字体风格
    /// </summary>
    public FontStyle Style
    {
      get
      {
        return fontStyle;
      }
      set
      {
        fontStyle = value;
      }
    }

    /// <summary>
    /// 获取或设置字号
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
    /// 获取或设置画笔宽度
    /// </summary>
    public int BrushWidth
    {
      get
      {
        return brushWidth;
      }
      set
      {
        brushWidth = value;
      }
    }

    /// <summary>
    /// 获取或设置前景颜色
    /// </summary>
    public Color ForeColor
    {
      get
      {
        return foreColor;
      }
      set
      {
        foreColor = value;
      }
    }

    /// <summary>
    /// 获取或设置背景颜色
    /// </summary>
    public Color BackColor
    {
      get
      {
        return backColor;
      }
      set
      {
        backColor = value;
      }
    }

    /// <summary>
    /// 获取或设置颜色容差
    /// </summary>
    public int Tolerance
    {
      get
      {
        return tolerance;
      }
      set
      {
        tolerance = value;
      }
    }

  }
}
