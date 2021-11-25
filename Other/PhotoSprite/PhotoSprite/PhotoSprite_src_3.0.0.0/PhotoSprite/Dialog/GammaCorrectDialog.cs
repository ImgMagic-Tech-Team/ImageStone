using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class GammaCorrectDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public GammaCorrectDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void degreeTrackBar_Scroll(object sender, EventArgs e)
    {
      int pixel = this.degreeTrackBar.Value;
      double gamma = 0;
      if (pixel <= 50)
      {
        gamma = pixel / 50.0;
      }
      else
      {
        gamma = (0.08 * pixel - 3);
      }

      this.degreeUpDown.Value = (decimal)gamma;
      UpdateCanvas();
    }

    private void degreeUpDown_ValueChanged(object sender, EventArgs e)
    {
      double gamma = (double)this.degreeUpDown.Value;
      int pixel = 0;
      if (gamma <= 1)
      {
        pixel = (int)(gamma * 50.0);
      }
      else
      {
        pixel = (int)(12.5 * gamma + 37.5);
      }

      this.degreeTrackBar.Value = pixel;
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Adjustment a = new Adjustment();

      dstImage = a.GammaCorrect((Bitmap)srcImage.Clone(), this.Degree);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取 Gamma 矫正值
    /// </summary>
    public double Degree
    {
      get
      {
        return (double)(this.degreeUpDown.Value);
      }
    }

  }
}