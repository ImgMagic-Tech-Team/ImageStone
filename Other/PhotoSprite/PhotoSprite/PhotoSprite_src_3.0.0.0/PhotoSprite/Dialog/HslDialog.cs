using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class HslDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public HslDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void hTrackBar_Scroll(object sender, EventArgs e)
    {
      this.hUpDown.Value = this.hTrackBar.Value;
    }

    private void sTrackBar_Scroll(object sender, EventArgs e)
    {
      this.sUpDown.Value = this.sTrackBar.Value;
    }

    private void lTrackBar_Scroll(object sender, EventArgs e)
    {
      this.lUpDown.Value = this.lTrackBar.Value;
    }

    private void hUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.hTrackBar.Value = (int)this.hUpDown.Value;
      UpdateCanvas();
    }

    private void sUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.sTrackBar.Value = (int)this.sUpDown.Value;
      UpdateCanvas();
    }

    private void lUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.lTrackBar.Value = (int)this.lUpDown.Value;
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Adjustment a = new Adjustment();

      dstImage = a.AdjustHsl((Bitmap)srcImage.Clone(), this.Hue, this.Saturation, this.Luminance);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取色调分量值
    /// </summary>
    public float Hue
    {
      get
      {
        return (float)this.hTrackBar.Value;
      }
    }

    /// <summary>
    /// 获取饱和度分量值
    /// </summary>
    public float Saturation
    {
      get
      {
        return (float)this.sTrackBar.Value / 100.0f;
      }
    }

    /// <summary>
    /// 获取亮度分量值
    /// </summary>
    public float Luminance
    {
      get
      {
        return (float)this.lTrackBar.Value / 100.0f;
      }
    }

  }
}