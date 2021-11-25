using System;
using System.Drawing;

namespace PhotoSprite.ColorSpace
{
  /// <summary>
  /// YUV 色彩空间结构体
  /// </summary>
  public struct YUV
	{
    byte y, u, v;

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
    /// 获取或设置 U 分量
    /// </summary>
    public byte U
    {
      get
      {
        return u;
      }
      set
      {
        u = value;
      }
    }

    /// <summary>
    /// 获取或设置 V 分量
    /// </summary>
    public byte V
    {
      get
      {
        return v;
      }
      set
      {
        v = value;
      }
    }


    public override bool Equals(object obj)
    {
      return this == (YUV)obj;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }


    /// <summary>
    /// 判断 YUV 结构体是否相等
    /// </summary>
    /// <param name="lYuv">YUV 结构体 1</param>
    /// <param name="rYuv">YUV 结构体 2</param>
    /// <returns></returns>
    public static bool operator ==(YUV lYuv, YUV rYuv)
    {
      if ((lYuv.Y == rYuv.Y) &&
          (lYuv.U == rYuv.U) &&
          (lYuv.V == rYuv.V))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    /// <summary>
    /// 判断 YUV 结构体是否不相等
    /// </summary>
    /// <param name="lYuv">YUV 结构体 1</param>
    /// <param name="rYuv">YUV 结构体 2</param>
    /// <returns></returns>
    public static bool operator !=(YUV lYuv, YUV rYuv)
    {
      return !(lYuv == rYuv);
    }


    /// <summary>
    /// 根据 (Y, U, V) 分量建立 PhotoSprite.ColorSpace.YUV 结构体
    /// </summary>
    /// <param name="y">Y 分量</param>
    /// <param name="u">U 分量</param>
    /// <param name="v">V 分量</param>
    /// <returns></returns>
    public static YUV FromYuv(byte y, byte u, byte v)
		{
      YUV yuv = new YUV();

      yuv.Y = y;
      yuv.U = u;
      yuv.V = v;

      return yuv;
		} // end of FromYuv


    /// <summary>
    /// 根据 Color 结构体建立 PhotoSprite.ColorSpace.YUV 结构体
    /// </summary>
    /// <param name="color">RGB 颜色结构体</param>
    /// <returns></returns>
    public static YUV FromColor(Color color)
		{
      return FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
    } // end of FromColor


    /// <summary>
    /// 根据 (red, green, blue) 颜色分量建立 PhotoSprite.ColorSpace.YUV 结构体
    /// </summary>
    /// <param name="red">red 分量</param>
    /// <param name="green">green 分量</param>
    /// <param name="blue">blue 分量</param>
    /// <returns></returns>
    public static YUV FromRgb(byte red, byte green, byte blue)
    {
      byte y, u, v;

      y = (byte)(((66 * red + 129 * green + 25 * blue + 128) >> 8) + 16);
      u = (byte)(((-38 * red - 74 * green + 112 * blue + 128) >> 8) + 128);
      v = (byte)(((112 * red - 94 * green - 18 * blue + 128) >> 8) + 128);

      return FromYuv(y, u, v);
    } // end of FromRgb


    /// <summary>
    /// 获取 RGB 颜色值
    /// </summary>
    /// <returns></returns>
    public Color ToRgb()
    { 
			int C = y - 16;
			int D = u - 128;
			int E = v - 128;

			int R, G, B;

			R = Math.Max(Math.Min((( 298 * C           + 409 * E + 128) >> 8), 255), 0);
			G = Math.Max(Math.Min((( 298 * C - 100 * D - 208 * E + 128) >> 8), 255), 0);
			B = Math.Max(Math.Min((( 298 * C + 516 * D           + 128) >> 8), 255), 0);

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
