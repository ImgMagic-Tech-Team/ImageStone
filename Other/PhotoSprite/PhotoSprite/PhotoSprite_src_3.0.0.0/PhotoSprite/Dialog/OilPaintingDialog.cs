using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class OilPaintingDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public OilPaintingDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void OilPaintingDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void brushSizeTrackBar_Scroll(object sender, EventArgs e)
    {
      this.brushSizeUpDown.Value = this.brushSizeTrackBar.Value;
    }

    private void coarsenessTrackBar_Scroll(object sender, EventArgs e)
    {
      this.coarsenessUpDown.Value = this.coarsenessTrackBar.Value;
    }

    private void brushSizeUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.brushSizeTrackBar.Value = (int)this.brushSizeUpDown.Value;
      UpdateCanvas();
    }

    private void coarsenessUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.coarsenessTrackBar.Value = (int)this.coarsenessUpDown.Value;
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Effect e = new Effect();

      dstImage = e.OilPainting((Bitmap)srcImage.Clone(), this.BrushSize, this.Coarseness);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }


    /// <summary>
    /// 获取画刷尺寸
    /// </summary>
    public int BrushSize
    {
      get
      {
        return this.brushSizeTrackBar.Value;
      }
    }

    /// <summary>
    /// 获取粗糙度
    /// </summary>
    public byte Coarseness
    {
      get
      {
        return (byte)this.coarsenessTrackBar.Value;
      }
    }

  }
}