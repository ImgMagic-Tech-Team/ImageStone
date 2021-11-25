using System;
using System.Drawing;

namespace PhotoSprite.ColorSpace
{
  /// <summary>
  /// HSL 色彩空间结构体
  /// </summary>
  public struct HSL
  {
    float h, s, l;

    /// <summary>
    /// 获取或设置色调[0, 360]
    /// </summary>
    public float Hue
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
    public float Saturation
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
    public float Luminance
    {
      get
      {
        return l;
      }
      set
      {
        l = value;
        if (l < 0.0f) l = 0.0f;
        if (l > 1.0f) l = 1.0f;
      }
    }


    public override bool Equals(object obj)
    {
      return this == (HSL)obj;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }


    /// <summary>
    /// 判断 HSL 结构体是否相等
    /// </summary>
    /// <param name="lHsl">HSL 结构体 1</param>
    /// <param name="rHsl">HSL 结构体 2</param>
    /// <returns></returns>
    public static bool operator ==(HSL lHsl, HSL rHsl)
    {
      if ((lHsl.Hue == rHsl.Hue) &&
          (lHsl.Saturation == rHsl.Saturation) &&
          (lHsl.Luminance == rHsl.Luminance))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    /// <summary>
    /// 判断 HSL 结构体是否不相等
    /// </summary>
    /// <param name="lHsl">HSL 结构体 1</param>
    /// <param name="rHsl">HSL 结构体 2</param>
    /// <returns></returns>
    public static bool operator !=(HSL lHsl, HSL rHsl)
    {
      return !(lHsl == rHsl);
    }


    /// <summary>
    /// 根据 (hue, saturation, luminance) 分量建立 PhotoSprite.ColorSpace.HSL 结构体
    /// </summary>
    /// <param name="hue">色调[0, 360]</param>
    /// <param name="saturation">饱和度[0, 1]</param>
    /// <param name="luminance">亮度[0, 1]</param>
    /// <returns></returns>
    public static HSL FromHsl(float hue, float saturation, float luminance)
    {
      HSL hsl = new HSL();

      hsl.Hue = hue;
      hsl.Saturation = saturation;
      hsl.Luminance = luminance;

      return hsl;
    } // end of FromHsl


    /// <summary>
    /// 根据 Color 结构体建立 PhotoSprite.ColorSpace.HSL 结构体
    /// </summary>
    /// <param name="color">RGB 颜色结构体</param>
    /// <returns></returns>
    public static HSL FromColor(Color color)
    {
      HSL hsl = new HSL();

      hsl.Hue = color.GetHue();
      hsl.Saturation = color.GetSaturation();
      hsl.Luminance = color.GetBrightness();

      return hsl;
    } // end of FromColor


    /// <summary>
    /// 根据 (red, green, blue) 颜色分量建立 PhotoSprite.ColorSpace.HSL 结构体
    /// </summary>
    /// <param name="red">red 分量</param>
    /// <param name="green">green 分量</param>
    /// <param name="blue">blue 分量</param>
    /// <returns></returns>
    public static HSL FromRgb(byte red, byte green, byte blue)
    {
      return FromColor(Color.FromArgb(red, green, blue));
    } // end of FromRgb


    /// <summary>
    /// 获取 RGB 颜色值
    /// </summary>
    /// <returns></returns>
    public Color ToRgb()
    {
      double r = 0, g = 0, b = 0;

      double t1, t2;

      double normalisedH = h / 360.0;

      if (l == 0)
      {
        r = g = b = 0;
      }
      else
      {
        if (s == 0)
        {
          r = g = b = l;
        }
        else
        {
          t2 = ((l <= 0.5) ? l * (1.0 + s) : l + s - l * s);
          t1 = 2.0 * l - t2;

          double[] T3 = new double[] { 
              normalisedH + 1.0 / 3.0, 
              normalisedH, 
              normalisedH - 1.0 / 3.0 };

          double[] C = new double[3];

          for (int i = 0; i < 3; i++)
          {
            if (T3[i] < 0) T3[i] += 1.0;
            if (T3[i] > 1) T3[i] -= 1.0;

            if (6.0 * T3[i] < 1.0)
              C[i] = t1 + (t2 - t1) * T3[i] * 6.0;
            else if (2.0 * T3[i] < 1.0)
              C[i] = t2;
            else if (3.0 * T3[i] < 2.0)
              C[i] = (t1 + (t2 - t1) * ((2.0 / 3.0) - T3[i]) * 6.0);
            else
              C[i] = t1;
          } // i

          r = C[0];
          g = C[1];
          b = C[2];
        } // s==0
      } // l==0

      return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
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


  } // end of class HSL

}
