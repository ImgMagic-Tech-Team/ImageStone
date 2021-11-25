using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class SketchDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public SketchDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void SketchDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void radPencil_CheckedChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void radSketch_CheckedChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void thresholdTrackBar_Scroll(object sender, EventArgs e)
    {
      this.thresholdUpDown.Value = this.thresholdTrackBar.Value;
    }

    private void thresholdUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.thresholdTrackBar.Value = (int)this.thresholdUpDown.Value;
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Effect e = new Effect();

      if (this.radSketch.Checked)
        dstImage = e.Sketch((Bitmap)srcImage.Clone(), this.Threshold);
      else
        dstImage = e.Pencil((Bitmap)srcImage.Clone(), this.Threshold);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取颜色值
    /// </summary>
    public byte Threshold
    {
      get
      {
        return (byte)this.thresholdTrackBar.Value;
      }
    }

  }
}