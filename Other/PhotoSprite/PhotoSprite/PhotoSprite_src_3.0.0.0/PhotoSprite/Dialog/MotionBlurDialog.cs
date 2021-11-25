using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class MotionBlurDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public MotionBlurDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void MotionBlurDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void distanceTrackBar_Scroll(object sender, EventArgs e)
    {
      this.distanceUpDown.Value = this.distanceTrackBar.Value;
      UpdateCanvas();
    }

    private void distanceUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.distanceTrackBar.Value = (int)this.distanceUpDown.Value;
      UpdateCanvas();
    }

    private void angleChooser_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        this.angleNumericUpDown.Value = this.angleChooser.Angle;
        UpdateCanvas();
      }
    }

    private void angleNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.angleChooser.Angle = (int)this.angleNumericUpDown.Value;
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Effect e = new Effect();

      dstImage = e.MotionBlur((Bitmap)srcImage.Clone(), this.Angle, this.Distance);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取方向角度
    /// </summary>
    public int Angle
    {
      get
      {
        return this.angleChooser.Angle;
      }

    }

    /// <summary>
    /// 获取距离
    /// </summary>
    public int Distance
    {
      get
      {
        return this.distanceTrackBar.Value;
      }

    }

  }
}