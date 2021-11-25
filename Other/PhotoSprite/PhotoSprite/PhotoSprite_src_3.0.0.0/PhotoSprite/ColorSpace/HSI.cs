using System;
using System.Drawing;

namespace PhotoSprite.ColorSpace
{
  /// <summary>
  /// HSI 色彩空间结构体
  /// </summary>
  public struct HSI
  {
    double h, s, i;

    /// <summary>
    /// 获取或设置色调[0, 360]
    /// </summary>
    public double H
    {
      get
      {
        return h;
      }
      set
      {
        h = (float)((int)(value) % 360);
      }
    }

    /// <summary>
    /// 获取或设置饱和度[0, 1]
    /// </summary>
    public double S
    {
      get
      {
        return s;
      }
      set
      {
        s = value;
        if (s < 0.0) s = 0.0f;
        if (s > 1.0) s = 1.0f;
      }
    }

    /// <summary>
    /// 获取或设置亮度[0, 1]
    /// </summary>
    public double I
    {
      get
      {
        return i;
      }
      set
      {
        i = value;
        if (i < 0.0) i = 0.0;
        if (i > 1.0) i = 1.0;
      }
    }


    public override bool Equals(object obj)
    {
      return this == (HSI)obj;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }


    /// <summary>
    /// 判断 HSI 结构体是否相等
    /// </summary>
    /// <param name="lHsi">HSI 结构体 1</param>
    /// <param name="rHsi">HSI 结构体 2</param>
    /// <returns></returns>
    public static bool operator ==(HSI lHsi, HSI rHsi)
    {
      if ((lHsi.H == rHsi.H) &&
          (lHsi.S == rHsi.S) &&
          (lHsi.I == rHsi.I))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    /// <summary>
    /// 判断 HSI 结构体是否不相等
    /// </summary>
    /// <param name="lHsi">HSI 结构体 1</param>
    /// <param name="rHsi">HSI 结构体 2</param>
    /// <returns></returns>
    public static bool operator !=(HSI lHsi, HSI rHsi)
    {
      return !(lHsi == rHsi);
    }


    /// <summary>
    /// 根据 (h, s, i) 分量建立 PhotoSprite.ColorSpace.HSI 结构体
    /// </summary>
    /// <param name="h">色调[0, 360]</param>
    /// <param name="s">饱和度[0, 1]</param>
    /// <param name="i">亮度[0, 1]</param>
    /// <returns></returns>
    public static HSI FromHsi(double h, double s, double i)
    {
      HSI hsi = new HSI();

      hsi.H = h;
      hsi.S = s;
      hsi.I = i;

      return hsi;
    } // end of FromHsi


    /// <summary>
    /// 根据 Color 结构体建立 PhotoSprite.ColorSpace.HSI 结构体
    /// </summary>
    /// <param name="color">RGB 颜色结构体</param>
    /// <returns></returns>
    public static HSI FromColor(Color color)
    {
      return FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
    } // end of FromColor


    /// <summary>
    /// 根据 (red, green, blue) 颜色分量建立 PhotoSprite.ColorSpace.HSI 结构体
    /// </summary>
    /// <param name="red">red 分量</param>
    /// <param name="green">green 分量</param>
    /// <param name="blue">blue 分量</param>
    /// <returns></returns>
    public static HSI FromRgb(byte red, byte green, byte blue)
    {
      double h, s, i;

      double v1 = 0.7071 * (green - blue);
      double v2 = 0.81650 * red - 0.40824 * (green + blue);

      h = Math.Atan2(v1, v2);
      s = Math.Sqrt(v1 * v1 + v2 * v2);
      i = 0.57735 * (red + green + blue);

      return FromHsi(h, s, i);
    } // end of FromRgb


    /// <summary>
    /// 获取 RGB 颜色值
    /// </summary>
    /// <returns></returns>
    public Color ToRgb()
    {
      double cos = s * Math.Cos(h);
      double sin = s * Math.Sin(h);

      int r = (int)(0.81650 * sin + 0.57735 * i);
      int g = (int)(0.7071 * cos - 0.40824 * sin + 0.57735 * i);
      int b = (int)(-0.7071 * cos - 0.40824 * sin + 0.57735 * i);

      return Color.FromArgb(r, g, b);
    } // end of ToRgb


    /// <summary>
    /// 获取 RGB 结构中 red 分量值
    /// </summary>
    /// <returns></returns>
    public byte GetRed()
    {
      return (byte)ToRgb().R;
    }


    /// <summary>
    /// 获取 RGB 结构中 green 分量值
    /// </summary>
    /// <returns></returns>
    public byte GetGreen()
    {
      return (byte)ToRgb().G;
    }


    /// <summary>
    /// 获取 RGB 结构中 blue 分量值
    /// </summary>
    /// <returns></returns>
    public byte GetBlue()
    {
      return (byte)ToRgb().B;
    }


  } // end of class HSI

}
