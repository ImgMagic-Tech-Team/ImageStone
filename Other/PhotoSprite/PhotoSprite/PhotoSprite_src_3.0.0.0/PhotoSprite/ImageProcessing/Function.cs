using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// PhotoSprite 常用函数类
  /// </summary>
  public class Function
  {
    /**************************************************
     * 
     * 自定义枚举类型  
     * 
     * 对齐方式、适应模式
     * 
     **************************************************/

    /// <summary>
    /// 对齐方式
    /// </summary>
    public enum AlignMode
    {
      /// <summary>
      /// 左上
      /// </summary>
      TopLeft,

      /// <summary>
      /// 左
      /// </summary>
      MidLeft,

      /// <summary>
      /// 左下
      /// </summary>
      BottomLeft,

      /// <summary>
      /// 上
      /// </summary>
      TopMid,

      /// <summary>
      /// 居中
      /// </summary>
      Center,

      /// <summary>
      /// 下
      /// </summary>
      BottomMid,

      /// <summary>
      /// 右上
      /// </summary>
      TopRight,

      /// <summary>
      /// 右
      /// </summary>
      MidRight,

      /// <summary>
      /// 右下
      /// </summary>
      BottomRight
    }


    /// <summary>
    /// 适应模式
    /// </summary>
    public enum FitMode
    {
      /// <summary>
      /// 适应指定宽度
      /// </summary>
      Width,

      /// <summary>
      /// 适应指定高度
      /// </summary>
      Height,

      /// <summary>
      /// 按指定宽高进行自适应，即约束宽高比
      /// </summary>
      WidthHeight,

      /// <summary>
      /// 适应指定百分率
      /// </summary>
      Percent,

      /// <summary>
      /// 不适应，即采用用户指定宽高
      /// </summary>
      None
    }


    /************************************************************
     * 
     * 拉伸图像、定位对象、颜色转换
     * 
     ************************************************************/

    /// <summary>
    /// 按照指定适应模式计算图像目标尺寸
    /// </summary>
    /// <param name="bgSize">背景尺寸</param>
    /// <param name="fgSize">前景尺寸</param>
    /// <param name="percent">如果指定了百分率适应模式，则此参数有效</param>
    /// <param name="fit">适应模式</param>
    /// <returns></returns>
    public Size ScaleToFit(Size bgSize,Size fgSize, double percent, FitMode fit)
    {
      Size dstSize = fgSize;

      switch (fit)
      {
        case FitMode.Width:
          dstSize.Height = fgSize.Width * bgSize.Height / bgSize.Width;
          break;

        case FitMode.Height:
          dstSize.Width = fgSize.Height * bgSize.Width / bgSize.Height;
          break;

        case FitMode.Percent:
          dstSize.Width = Convert.ToInt32(bgSize.Width * percent);
          dstSize.Height = Convert.ToInt32(bgSize.Height * percent);
          break;

        case FitMode.WidthHeight:
          if ((fgSize.Width * bgSize.Height) < (bgSize.Width * fgSize.Height))   // 等同于 ( (fgSize.Width/fgSize.Height) < (bgSize.Width/bgSize.Height) )
          {
            dstSize.Height = fgSize.Width * bgSize.Height / bgSize.Width;
          }
          else
          {
            dstSize.Width = fgSize.Height * bgSize.Width / bgSize.Height;
          }
          break;

        default: // FitMode.None
          break;
      }

      return dstSize;
    } // end of ScaleToFit


    /// <summary>
    /// 根据用户指定的对齐模式，将前景图像定位到背景图像上
    /// </summary>
    /// <param name="W">背景图像宽度</param>
    /// <param name="H">背景图像高度</param>
    /// <param name="w">前景图像宽度</param>
    /// <param name="h">前景图像高度</param>
    /// <param name="align">对齐模式</param>
    /// <returns></returns>
    public Point Locate(int W, int H, int w, int h, AlignMode align)
    {
      int x = 0;
      int y = 0;

      switch (align)
      {
        case AlignMode.TopLeft:
          x = 0;
          y = 0;
          break;

        case AlignMode.MidLeft:
          x = 0;
          y = (H - h) / 2;
          break;

        case AlignMode.BottomLeft:
          x = 0;
          y = H - h;
          break;

        case AlignMode.TopMid:
          x = (W - w) / 2;
          y = 0;
          break;

        case AlignMode.Center:
          x = (W - w) / 2;
          y = (H - h) / 2;
          break;

        case AlignMode.BottomMid:
          x = (W - w) / 2;
          y = H - h;
          break;

        case AlignMode.TopRight:
          x = W - w;
          y = 0;
          break;

        case AlignMode.MidRight:
          x = W - w;
          y = (H - h) / 2;
          break;

        case AlignMode.BottomRight:
          x = W - w;
          y = H - h;
          break;
      } // align

      return new Point(x, y);
    } // end of Locate


    /// <summary>
    /// 将十六进制颜色字符串转换成标准颜色结构体
    /// </summary>
    /// <param name="color">十六进制颜色字符串，如 "#FF0000", "#FFFF0000"</param>
    /// <returns></returns>
    public static System.Drawing.Color HexString2Color(string color)
    {
      color = color.ToLower();

      Color returnColor = new Color();

      Regex regexColor = new Regex(@"^#([0-9a-fA-F]{6})|([0-9a-fA-F]{8})$");
      if (regexColor.IsMatch(color))
      {
        int A, R, G, B;
        if (color.Length == 7)
        {
          R = Convert.ToInt32(color.Substring(1, 2), 16);
          G = Convert.ToInt32(color.Substring(3, 2), 16);
          B = Convert.ToInt32(color.Substring(5, 2), 16);
          returnColor = Color.FromArgb(R, G, B);
        }
        else
        {
          A = Convert.ToInt32(color.Substring(1, 2), 16);
          R = Convert.ToInt32(color.Substring(3, 2), 16);
          G = Convert.ToInt32(color.Substring(5, 2), 16);
          B = Convert.ToInt32(color.Substring(7, 2), 16);
          returnColor = Color.FromArgb(A, R, G, B);
        }
      }

      return returnColor;
    } // end of HexString2Color


    /// <summary>
    /// 将标准颜色结构体转换成十六进制颜色字符串
    /// </summary>
    /// <param name="color">标准颜色结构体</param>
    /// <returns></returns>
    public static string Color2HexString(Color color)
    {
      string a = "0" + Convert.ToString(color.A, 16);
      string r = "0" + Convert.ToString(color.R, 16);
      string g = "0" + Convert.ToString(color.G, 16);
      string b = "0" + Convert.ToString(color.B, 16);

      a = a.Substring(a.Length - 2).ToUpper();
      r = r.Substring(r.Length - 2).ToUpper();
      g = g.Substring(g.Length - 2).ToUpper();
      b = b.Substring(b.Length - 2).ToUpper();

      return a + r + g + b;
    } // end of Color2HexString


    /// <summary>
    /// 根据折线段的端点集建立256级亮度映射表
    /// </summary>
    /// <param name="Points">折线段端点集</param>
    /// <returns></returns>
    public byte[] GetMap(Point[] Points)
    {
      byte[] Map = new byte[256];
      for (int i = 0; i < 256; i++)
      {
        Map[i] = (byte)i;
      } // i

      int count = Points.Length;
      // 亮度曲线至少有两点
      if (count < 2)
        return Map;

      for (int i = 0; i < count - 1; i++)
      {
        Point point = Points[i];
        Point next = Points[i + 1];

        if (point.X == next.X)
        {
          Map[point.X] = (byte)next.Y;
          continue;
        }

        // 斜率: k = ( y2 - y1 ) / ( x2 - x1 );
        // y = k * x - k * x1 + y1;
        double k = (double)(next.Y - point.Y) / (double)(next.X - point.X);
        double c = point.Y - k * point.X;
        for (int x = point.X; x <= next.X; x++)
        {
          int y = (int)(k * x + c);
          Map[x] = (byte)y;
        } // x
      } // i

      return Map;
    } // end of GetMap


    /// <summary>
    /// 根据指定的首点和尾点返回由这两点所确定直线中的一系列点集
    /// </summary>
    /// <param name="start">首点</param>
    /// <param name="end">尾点</param>
    /// <returns></returns>
    public static Point[] GetLinePoints(Point start, Point end)
    {
      Point[] coords = null;

      int x1 = start.X;
      int y1 = start.Y;

      int x2 = end.X;
      int y2 = end.Y;

      int dx = x2 - x1;
      int dy = y2 - y1;

      int dxAbs = Math.Abs(dx);
      int dyAbs = Math.Abs(dy);

      int px = x1;
      int py = y1;

      int sdx = Math.Sign(dx);
      int sdy = Math.Sign(dy);

      int x = 0;
      int y = 0;

      if (dxAbs == dyAbs)
      {
        coords = new Point[dxAbs + 1];

        for (int i = 0; i <= dxAbs; i++)
        {
          coords[i] = new Point(px, py);
          px += sdx;
          py += sdy;
        }
      }

      if (dxAbs > dyAbs)
      {
        coords = new Point[dxAbs + 1];

        for (int i = 0; i <= dxAbs; i++)
        {
          y += dyAbs;

          if (y >= dxAbs)
          {
            y -= dxAbs;
            py += sdy;
          }

          coords[i] = new Point(px, py);
          px += sdx;
        } // i
      }

      if (dxAbs < dyAbs)
      {
        coords = new Point[dyAbs + 1];

        for (int i = 0; i <= dyAbs; i++)
        {
          x += dxAbs;

          if (x >= dyAbs)
          {
            x -= dyAbs;
            px += sdx;
          }

          coords[i] = new Point(px, py);
          py += sdy;
        } // i
      }

      return coords;
    } // end of GetLinePoints


    /// <summary>
    /// 根据两个点建立起一个基于它们的矩形
    /// </summary>
    /// <param name="a">矩形的一个顶点</param>
    /// <param name="b">矩形的另一个顶点</param>
    /// <returns></returns>
    public static Rectangle PointsToRectangle(Point a, Point b)
    {
      int x = Math.Min(a.X, b.X);
      int y = Math.Min(a.Y, b.Y);
      int width = Math.Abs(a.X - b.X) + 1;
      int height = Math.Abs(a.Y - b.Y) + 1;

      return new Rectangle(x, y, width, height);
    }


    /// <summary>
    /// 判断一个点是否在矩形内
    /// </summary>
    /// <param name="p">点</param>
    /// <param name="r">矩形</param>
    /// <returns></returns>
    public static bool IsPointInRectangle(Point p, Rectangle r)
    {
      if ((p.X < r.X) || (p.Y < r.Y) || (p.X >= r.Right) || (p.Y >= r.Bottom))
      {
        return false;
      }
      else
      {
        return true;
      }
    }


    /// <summary>
    /// 判断一个点是否在矩形内，如果不在，则将其置到矩形内
    /// </summary>
    /// <param name="point">点</param>
    /// <param name="rect">矩形</param>
    /// <returns></returns>
    public static Point PointInRectangle(Point point, Rectangle rect)
    {
      if (point.X < 0) point.X = 0;
      if (point.Y < 0) point.Y = 0;
      if (point.X >= rect.Width) point.X = rect.Width - 1;
      if (point.Y >= rect.Height) point.Y = rect.Height - 1;

      return point;
    }


    /// <summary>
    /// 根据指定的颜色容差来判定两种颜色是否相同
    /// </summary>
    /// <param name="destination">目标颜色</param>
    /// <param name="standard">标准颜色</param>
    /// <param name="tolerance">容差</param>
    /// <returns></returns>
    public static bool CheckColor(Color destination, Color standard, int tolerance)
    {
      int ds = 0;

      int red = destination.R - standard.R;
      ds += red * red;

      int green = destination.G - standard.G;
      ds += green * green;

      int blue = destination.B - standard.B;
      ds += blue * blue;

      return (ds < tolerance * tolerance);
    }


    /// <summary>
    /// 根据指定的四个点进行重组，使得这四个点朝一个方向
    /// </summary>
    /// <param name="a">点 A 坐标</param>
    /// <param name="b">点 B 坐标</param>
    /// <param name="c">点 C 坐标</param>
    /// <param name="d">点 D 坐标</param>
    /// <returns></returns>
    public static PointF[] MakePolygon(PointF a, PointF b, PointF c, PointF d)
    {
      PointF dirA = new PointF(a.X - b.X, a.Y - b.Y);
      PointF dirB = new PointF(c.X - d.X, c.Y - d.Y);

      if (dirA.X * dirB.X + dirA.Y * dirB.Y > 0)
      {
        return new PointF[] { a, b, d, c };
      }
      else
      {
        return new PointF[] { a, b, c, d };
      }
    }


  }
}
