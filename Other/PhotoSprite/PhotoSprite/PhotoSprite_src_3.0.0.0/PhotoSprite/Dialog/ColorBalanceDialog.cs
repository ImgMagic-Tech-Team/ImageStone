using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class ColorBalanceDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public ColorBalanceDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void redTrackBar_Scroll(object sender, EventArgs e)
    {
      this.redUpDown.Value = this.redTrackBar.Value;
    }

    private void greenTrackBar_Scroll(object sender, EventArgs e)
    {
      this.greenUpDown.Value = this.greenTrackBar.Value;
    }

    private void blueTrackBar_Scroll(object sender, EventArgs e)
    {
      this.blueUpDown.Value = this.blueTrackBar.Value;
    }

    private void redUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.redTrackBar.Value = (int)this.redUpDown.Value;
      UpdateCanvas();
    }

    private void greenUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.greenTrackBar.Value = (int)this.greenUpDown.Value;
      UpdateCanvas();
    }

    private void blueUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.blueTrackBar.Value = (int)this.blueUpDown.Value;
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Adjustment a = new Adjustment();

      dstImage = a.ColorBalance((Bitmap)srcImage.Clone(), this.Red, this.Green, this.Blue);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取红色分量值
    /// </summary>
    public int Red
    {
      get
      {
        return this.redTrackBar.Value;
      }
    }

    /// <summary>
    /// 获取绿色分量值
    /// </summary>
    public int Green
    {
      get
      {
        return this.greenTrackBar.Value;
      }
    }

    /// <summary>
    /// 获取蓝色分量值
    /// </summary>
    public int Blue
    {
      get
      {
        return this.blueTrackBar.Value;
      }
    }


  }
}