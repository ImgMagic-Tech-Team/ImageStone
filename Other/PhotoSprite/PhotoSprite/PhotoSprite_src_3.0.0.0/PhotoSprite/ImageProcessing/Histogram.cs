using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 直方图类
  /// </summary>
  public class Histogram : ImageInfo
  {
    /// <summary>
    /// 支持绘制的色彩模式
    /// </summary>
    public enum ColorMode
    {
      /// <summary>
      /// 红色
      /// </summary>
      Red,

      /// <summary>
      /// 绿色
      /// </summary>
      Green,

      /// <summary>
      /// 蓝色
      /// </summary>
      Blue
    }

    private Statistics red;
    private Statistics green;
    private Statistics blue;

    private Bitmap b;
    private int width = 0;
    private int height = 0;

    /// <summary>
    /// 获取红色分量统计资料
    /// </summary>
    public Statistics Red
    {
      get
      {
        return red;
      }
    }

    /// <summary>
    /// 获取绿色分量统计资料
    /// </summary>
    public Statistics Green
    {
      get
      {
        return green;
      }
    }

    /// <summary>
    /// 获取蓝色分量统计资料
    /// </summary>
    public Statistics Blue
    {
      get
      {
        return blue;
      }
    }


    /// <summary>
    /// 建立图像直方图
    /// </summary>
    /// <param name="b">位图流</param>
    public Histogram(Bitmap b)
    {
      this.b = b;

      width = b.Width;
      height = b.Height;

      CountRgb();
    } // end of Histogram


    /// <summary>
    /// 统计色彩 R、G、B 三分量频率
    /// </summary>
    private void CountRgb()
    {
      int[] Red = new int[256];
      int[] Green = new int[256];
      int[] Blue = new int[256];

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
            Red[p[2]]++;
            Green[p[1]]++;
            Blue[p[0]]++;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      // 生成 R、G、B 三分量统计类
      this.red = new Statistics(Red);
      this.green = new Statistics(Green);
      this.blue = new Statistics(Blue);
    } // end of CountRgb


    /// <summary>
    /// 绘制直方图
    /// </summary>
    /// <param name="diagramHeight">图表高度</param>
    /// <param name="viewByLog">按 Log 函数绘制直方图</param>
    /// <param name="colorMode">色彩模式</param>
    /// <returns></returns>
    public Bitmap DrawDiagram(int diagramHeight, bool viewByLog, ColorMode colorMode)
    {
      // 获取亮度概率
      double[] Probability = this.Red.Probability;

      // 亮度概率最大值
      double maxProbability = Probability[this.Red.MaxIndex];

      // 用于绘制直方图的颜色
      Color color = Color.Red;

      switch (colorMode)
      {
        case ColorMode.Red:
          Probability = this.Red.Probability;
          maxProbability = Probability[this.Red.MaxIndex];
          color = Color.Red;
          break;

        case ColorMode.Green:
          Probability = this.Green.Probability;
          maxProbability = Probability[this.Green.MaxIndex];
          color = Color.Green;
          break;

        case ColorMode.Blue:
          Probability = this.Blue.Probability;
          maxProbability = Probability[this.Blue.MaxIndex];
          color = Color.Blue;
          break;
      } // switch

      Pen pen = new Pen(color, 1);
      Bitmap dstImage = new Bitmap(256, diagramHeight);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);
      int y = 0;

      // 绘制直方图
      for (int i = 0; i < 256; i++)
      {
        // 当前亮度级与最大亮度级的比率
        double percent = Probability[i] / maxProbability;

        if (viewByLog)
        {
          // 因为不同色彩亮度级概率密度分布不同，分布曲线可能平缓，也可能陡峭，
          // 鉴于这种差异，我们对色彩概率进行对数处理，以拉开分布情况，便于查阅
          y = (int)(diagramHeight * (1 - Math.Log(100 * percent + 1, 101)));
        }
        else
        {
          y = (int)(diagramHeight * (1 - percent));
        }

        g.DrawLine(pen, i, y, i, diagramHeight);
      } // i

      g.Save();
      g.Dispose();

      return dstImage;
    } // end of DrawDiagram


    /// <summary>
    /// 绘制经过直方图均衡化处理的图像
    /// </summary>
    /// <returns></returns>
    public Bitmap Equalizer()
    {
      // 图像各通道均衡化后映射表
      byte[] RedMap = this.Red.Equalizer;
      byte[] GreenMap = this.Green.Equalizer;
      byte[] BlueMap = this.Blue.Equalizer;

      Bitmap dstImage = (Bitmap)b.Clone();

      // 通道映射
      Adjustment a = new Adjustment();
      dstImage = a.Mapping(dstImage, RedMap, Adjustment.ChannelMode.Red);
      dstImage = a.Mapping(dstImage, GreenMap, Adjustment.ChannelMode.Green);
      dstImage = a.Mapping(dstImage, BlueMap, Adjustment.ChannelMode.Blue);

      return dstImage;
    } // end of Equalizer


  }
}

