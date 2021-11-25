using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PhotoSprite.ColorSpace;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite
{
  public partial class ColorPickerDialog : Form
  {
    byte alpha, red, green, blue;
    int hue, saturation, luminance;
    bool locked = false;

    /// <summary>
    /// 获取或设置 ARGB 颜色
    /// </summary>
    public Color ArgbColor
    {
      get
      {
        return Color.FromArgb(alpha, red, green, blue);
      }
      set
      {
        Color color = value;

        this.alpha = color.A;
        this.red = color.R;
        this.green = color.G;
        this.blue = color.B;

        this.redUpDown.Value = this.red;
        this.greenUpDown.Value = this.green;
        this.blueUpDown.Value = this.blue;

        UpdateByRgb();
      }
    }

    /// <summary>
    /// 获取或设置 HSL 颜色
    /// </summary>
    public HSL HslColor
    {
      get
      {
        return HSL.FromHsl(hue, saturation / 100.0f, luminance / 100.0f);
      }
      set
      {
        HSL hsl = value;

        this.hue = (int)hsl.Hue;
        this.saturation = (int)(hsl.Saturation * 100);
        this.luminance = (int)(hsl.Luminance * 100);

        this.hueTrackBar.Value = this.hue;
        this.saturationTrackBar.Value = this.saturation;
        this.luminanceTrackBar.Value = this.luminance;

        this.hueUpDown.Value = this.hue;
        this.saturationUpDown.Value = this.saturation;
        this.luminanceUpDown.Value = this.luminance;

        UpdateByHsl();
      }
    }

    /// <summary>
    /// 根据指定的颜色绘制出一个色彩按钮
    /// </summary>
    /// <param name="color">颜色</param>
    /// <returns></returns>
    public static Bitmap DrawColorButton(Color color)
    {
      int width = 16;
      int height = 16;

      Bitmap icon = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(icon);
      if (color.A > 127)
      {
        g.FillRectangle(new SolidBrush(color), new Rectangle(0, 0, width, height));
      }
      else
      {
        // 绘制透明色，用斜叉显示
        g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, width, height));
        g.DrawLine(new Pen(new SolidBrush(Color.Red), 2), new Point(0, height), new Point(width, 0));
      }
      g.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1), new Rectangle(0, 0, width - 1, height - 1));

      return icon;
    }

    public ColorPickerDialog()
    {
      InitializeComponent();
    }

    private void ColorPickerDialog_Load(object sender, EventArgs e)
    {
      // 建立 Hue 图
      CreateImages(true, true, true);

      // 透明图小方块
      this.transparencyLabel.Image = DrawColorButton(Color.Transparent);
    }

    /// <summary>
    /// 按 HSL 进行数据更新
    /// </summary>
    private void UpdateByHsl()
    {
      if (!locked)
      {
        locked = true;

        Color color = HSL.FromHsl(hue, saturation / 100.0f, luminance / 100.0f).ToRgb();
        this.redUpDown.Value = this.red = color.R;
        this.greenUpDown.Value = this.green = color.G;
        this.blueUpDown.Value = this.blue = color.B;

        this.rgbHexTextBox.Text = Function.Color2HexString(color).Substring(2);

        locked = false;
      }
    } // end of UpdateByHsl

    /// <summary>
    /// 按 RGB 进行数据更新
    /// </summary>
    private void UpdateByRgb()
    {
      if (!locked)
      {
        locked = true;

        Color color = Color.FromArgb(alpha, red, green, blue);
        string hex = Function.Color2HexString(color);
        if (alpha == 255)
          this.rgbHexTextBox.Text = hex.Substring(2); // 显示 6 位
        else
          this.rgbHexTextBox.Text = hex;  // 显示 8 位

        HSL hsl = HSL.FromColor(color);
        this.hueTrackBar.Value = (int)hsl.Hue;
        this.saturationTrackBar.Value = (int)(hsl.Saturation * 100);
        this.luminanceTrackBar.Value = (int)(hsl.Luminance * 100);

        this.hueUpDown.Value = (int)hsl.Hue;
        this.saturationUpDown.Value = (int)(hsl.Saturation * 100);
        this.luminanceUpDown.Value = (int)(hsl.Luminance * 100);

        locked = false;
      }
    } // end of UpdateByRgb

    /// <summary>
    /// 按 16 进制颜色进行数据更新
    /// </summary>
    private void UpdateByHexColor()
    {
      if (!locked)
      {
        locked = true;

        this.redUpDown.Value = this.red;
        this.greenUpDown.Value = this.green;
        this.blueUpDown.Value = this.blue;

        HSL hsl = HSL.FromRgb(red, green, blue);
        this.hueTrackBar.Value = (int)hsl.Hue;
        this.saturationTrackBar.Value = (int)(hsl.Saturation * 100);
        this.luminanceTrackBar.Value = (int)(hsl.Luminance * 100);

        this.hueUpDown.Value = (int)hsl.Hue;
        this.saturationUpDown.Value = (int)(hsl.Saturation * 100);
        this.luminanceUpDown.Value = (int)(hsl.Luminance * 100);

        locked = false;
      }
    } // end of UpdateByHexColor

    private void DrawHueImage()
    {
      int width = this.huePictureBox.Width;
      int height = this.huePictureBox.Height;

      // H 图
      Bitmap hueImage = new Bitmap(width, height);
      Graphics g = Graphics.FromImage(hueImage);
      for (int i = 0; i < 360; i += 2)
      {
        HSL hsl = HSL.FromHsl((float)i, .5f, .5f);
        g.DrawLine(new Pen(hsl.ToRgb()), 0, 179 - i / 2, width, 179 - i / 2);
      }
      this.huePictureBox.Image = hueImage;
    }

    private void DrawSaturationImage()
    {
      int width = this.saturationPictureBox.Width;
      int height = this.saturationPictureBox.Height;

      // S 图
      Bitmap saturationImage = new Bitmap(width, height);
      Graphics g = Graphics.FromImage(saturationImage);
      for (int i = 0; i < 360; i += 2)
      {
        HSL hsl = HSL.FromHsl(hue, (360 - i) / 360.0f, .5f);
        g.DrawLine(new Pen(hsl.ToRgb()), 0, i / 2, width, i / 2);
      }
      this.saturationPictureBox.Image = saturationImage;
    }

    private void DrawLuminanceImage()
    {
      int width = this.luminancePictureBox.Width;
      int height = this.luminancePictureBox.Height;

      // L 图
      Bitmap luminanceImage = new Bitmap(width, height);
      Graphics g = Graphics.FromImage(luminanceImage);
      for (int i = 0; i < 360; i += 2)
      {
        HSL hsl = HSL.FromHsl(hue, saturation / 100.0f, (360 - i) / 360.0f);
        g.DrawLine(new Pen(hsl.ToRgb()), 0, i / 2, width, i / 2);
      }
      this.luminancePictureBox.Image = luminanceImage;
    }

    /// <summary>
    /// 生成 HSL 条状图
    /// </summary>
    private void CreateImages(bool showHue, bool showSaturation, bool showLuminance)
    {
      if (showHue) DrawHueImage();
      if (showSaturation) DrawSaturationImage();
      if (showLuminance) DrawLuminanceImage();

      // 颜色预览
      this.colorPreviewLabel.BackColor = Color.FromArgb(red, green, blue);
    }


    /******************************
    * 
    * 滚动条
    * 
    ******************************/

    private void hueTrackBar_Scroll(object sender, EventArgs e)
    {
      this.hueUpDown.Value = this.hue = this.hueTrackBar.Value;
      UpdateByHsl();
      CreateImages(false, true, true);
    }

    private void saturationTrackBar_Scroll(object sender, EventArgs e)
    {
      this.saturationUpDown.Value = this.saturation = this.saturationTrackBar.Value;
      UpdateByHsl();
      CreateImages(false, false, true);
    }

    private void luminanceTrackBar_Scroll(object sender, EventArgs e)
    {
      this.luminanceUpDown.Value = this.luminance = this.luminanceTrackBar.Value;
      UpdateByHsl();
      CreateImages(false, false, false);
    }

    /******************************
    * 
    * 数据
    * 
    ******************************/

    private void hueUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.hue = this.hueTrackBar.Value = (int)this.hueUpDown.Value;
      UpdateByHsl();
      CreateImages(false, true, true);
    }

    private void saturationUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.saturation = this.saturationTrackBar.Value = (int)this.saturationUpDown.Value;
      UpdateByHsl();
      CreateImages(false, false, true);
    }

    private void luminanceUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.luminance = this.luminanceTrackBar.Value = (int)this.luminanceUpDown.Value;
      UpdateByHsl();
    }

    private void redUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.red = (byte)this.redUpDown.Value;
      UpdateByRgb();
    }

    private void greenUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.green = (byte)this.greenUpDown.Value;
      UpdateByRgb();
    }

    private void blueUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.blue = (byte)this.blueUpDown.Value;
      UpdateByRgb();
    }

    private void rgbHexTextBox_TextChanged(object sender, EventArgs e)
    {
      string text = this.rgbHexTextBox.Text;
      if (!(text.Length == 6 || text.Length == 8))
        return;

      Color color;
      try
      {
        color = Function.HexString2Color("#" + text);
      }
      catch
      {
        color = this.ArgbColor;
      }

      this.alpha = color.A;
      this.red = color.R;
      this.green = color.G;
      this.blue = color.B;

      UpdateByHexColor();
    }

    private void transparencyLabel_Click(object sender, EventArgs e)
    {
      this.alpha = this.red = this.green = this.blue = 0;
      this.rgbHexTextBox.Text = "00000000";
      UpdateByHexColor();
    }


  }
}