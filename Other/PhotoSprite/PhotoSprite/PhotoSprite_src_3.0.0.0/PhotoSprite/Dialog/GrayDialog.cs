using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class GrayDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public GrayDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void GrayDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void rad1_CheckedChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void rad2_CheckedChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void rad3_CheckedChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      GrayProcessing gp = new GrayProcessing();

      dstImage = gp.Gray((Bitmap)srcImage.Clone(), this.GrayMethod);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取灰度方法
    /// </summary>
    public GrayProcessing.GrayMethod GrayMethod
    {
      get
      {
        GrayProcessing.GrayMethod grayMethod = GrayProcessing.GrayMethod.WeightAveraging;

        if (this.rad2.Checked)
          grayMethod = GrayProcessing.GrayMethod.Maximum;
        if (this.rad3.Checked)
          grayMethod = GrayProcessing.GrayMethod.Average;

        return grayMethod;
      }
    }

  }
}