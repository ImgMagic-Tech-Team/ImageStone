using System;
using System.Drawing;

namespace PhotoSprite.ColorSpace
{
  /// <summary>
  /// CMYK 色彩空间结构体
  /// </summary>
  public struct CMYK
  {
    byte c, m, y, k;

    /// <summary>
    /// 获取或设置 C 分量
    /// </summary>
    public byte C
    {
      get
      {
        return c;
      }
      set
      {
        c = value;
      }
    }

    /// <summary>
    /// 获取或设置 M 分量
    /// </summary>
    public byte M
    {
      get
      {
        return m;
      }
      set
      {
        m = value;
      }
    }

    /// <summary>
    /// 获取或设置 Y 分量
    /// </summary>
    public byte Y
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
    /// 获取或设置 K 分量
    /// </summary>
    public byte K
    {
      get
      {
        return k;
      }
      set
      {
        k = value;
      }
    }


    public override bool Equals(object obj)
    {
      return this == (CMYK)obj;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }


    /// <summary>
    /// 判断 CMYK 结构体是否相等
    /// </summary>
    /// <param name="lCmyk">CMYK 结构体 1</param>
    /// <param name="rCmyk">CMYK 结构体 2</param>
    /// <returns></returns>
    public static bool operator ==(CMYK lCmyk, CMYK rCmyk)
    {
      if ((lCmyk.C == rCmyk.C) &&
          (lCmyk.M == rCmyk.M) &&
          (lCmyk.Y == rCmyk.Y) &&
          (lCmyk.K == rCmyk.K))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    /// <summary>
    /// 判断 CMYK 结构体是否不相等
    /// </summary>
    /// <param name="lCmyk">CMYK 结构体 1</param>
    /// <param name="rCmyk">CMYK 结构体 2</param>
    /// <returns></returns>
    public static bool operator !=(CMYK lCmyk, CMYK rCmyk)
    {
      return !(lCmyk == rCmyk);
    }


    /// <summary>
    /// 根据 (c, m, y, k) 分量建立 PhotoSprite.ColorSpace.CMYK 结构体
    /// </summary>
    /// <param name="c">C 分量</param>
    /// <param name="m">M 分量</param>
    /// <param name="y">Y 分量</param>
    /// <param name="k">K 分量</param>
    public static CMYK FromCmyk(byte c, byte m, byte y, byte k)
    {
      CMYK cmyk = new CMYK();

      cmyk.c = c;
      cmyk.m = m;
      cmyk.y = y;
      cmyk.k = k;

      return cmyk;
    } // end of FromCmyk


    /// <summary>
    /// 根据 Color 结构体建立 PhotoSprite.ColorSpace.CMYK 结构体
    /// </summary>
    /// <param name="color">RGB 颜色结构体</param>
    /// <returns></returns>
    public static CMYK FromColor(Color color)
    {
      return FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
    } // end of FromColor


    /// <summary>
    /// 根据 (red, green, blue) 颜色分量建立 PhotoSprite.ColorSpace.CMYK 结构体
    /// </summary>
    /// <param name="red">red 分量</param>
    /// <param name="green">green 分量</param>
    /// <param name="blue">blue 分量</param>
    /// <returns></returns>
    public static CMYK FromRgb(byte red, byte green, byte blue)
    {
      byte c, m, y, k;

      c = (byte)(255 - red);
      m = (byte)(255 - green);
      y = (byte)(255 - blue);

      k = Math.Min(c, Math.Min(m, y));
      c -= k;
      m -= k;
      y -= k;

      return FromCmyk(c, m, y, k);
    } // end of FromRgb


    /// <summary>
    /// 获取 RGB 颜色值
    /// </summary>
    /// <returns></returns>
    public Color ToRgb()
    {
      int R, G, B;

      R = 255 - c - k;
      G = 255 - m - k;
      B = 255 - y - k;

      return Color.FromArgb(R, G, B);
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


  }
}
