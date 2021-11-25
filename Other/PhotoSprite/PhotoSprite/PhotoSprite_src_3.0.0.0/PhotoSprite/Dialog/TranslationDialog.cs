using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class TranslationDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public TranslationDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void horzUpDown_ValueChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void vertUpDown_ValueChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
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
      ImageTransform it = new ImageTransform();

      dstImage = it.Translate((Bitmap)srcImage.Clone(), this.Horizontal, this.Vertical, this.AreasMode);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取水平偏移量
    /// </summary>
    public int Horizontal
    {
      get
      {
        return (int)this.horzUpDown.Value;
      }
    }

    /// <summary>
    /// 获取垂直偏移量
    /// </summary>
    public int Vertical
    {
      get
      {
        return (int)this.vertUpDown.Value;
      }
    }

    /// <summary>
    /// 获取平移后图像留下的未知区域设置方式
    /// </summary>
    public ImageTransform.AreasMode AreasMode
    {
      get
      {
        ImageTransform.AreasMode areaMode = ImageTransform.AreasMode.WrapAround;
        if (this.rad1.Checked)
          areaMode = ImageTransform.AreasMode.Transparent;
        if (this.rad2.Checked)
          areaMode = ImageTransform.AreasMode.RepeatEdgePixels;

        return areaMode;
      }
    }

  }
}