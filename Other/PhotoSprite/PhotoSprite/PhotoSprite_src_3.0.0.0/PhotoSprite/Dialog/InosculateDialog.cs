using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class InosculateDialog : Form
  {
    private WinMain parent = null;
    private Bitmap srcImage = null;
    private Bitmap bgImage = null;
    private Bitmap fgImage = null;
    private Region validRegion = new Region();

    public InosculateDialog(WinMain parent)
    {
      InitializeComponent();

      this.parent = parent;
      this.srcImage = (Bitmap)this.parent.canvasMain.Image.Clone();
    }

    private void InosculateDialog_Load(object sender, EventArgs e)
    {
      this.parent.layer.Visible = false;

      validRegion = new Region(new Rectangle(0, 0, this.srcImage.Width, this.srcImage.Height));
      validRegion.Intersect(this.parent.layer.Bounds);

      RegionClip rc = new RegionClip(validRegion);
      this.bgImage = rc.Hold((Bitmap)this.parent.canvasMain.Image.Clone());
      rc.SelectedRegion.Translate(-this.parent.layer.Bounds.X, -this.parent.layer.Bounds.Y);
      this.fgImage = rc.Hold((Bitmap)this.parent.layer.Image.Clone());
      rc.SelectedRegion.Translate(this.parent.layer.Bounds.X, this.parent.layer.Bounds.Y);

      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.parent.canvasMain.Image = (Bitmap)this.srcImage.Clone();
      this.parent.layer.Visible = true;
    }

    private void transparencyTrackBar_Scroll(object sender, EventArgs e)
    {
      this.transparencyUpDown.Value = this.transparencyTrackBar.Value;
    }

    private void transparencyUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.transparencyTrackBar.Value = (int)this.transparencyUpDown.Value;

      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Effect f = new Effect();
      Bitmap alphaImage = f.Inosculate((Bitmap)bgImage.Clone(), (Bitmap)fgImage.Clone(), this.Transparency);

      RectangleF validRect = new RectangleF();

      Bitmap dstImage = (Bitmap)this.srcImage.Clone();
      Graphics g = System.Drawing.Graphics.FromImage(dstImage);
      validRect = validRegion.GetBounds(g);

      g.DrawImage(alphaImage, validRect,
        new Rectangle(0, 0, (int)validRect.Width, (int)validRect.Height), GraphicsUnit.Pixel);

      this.parent.canvasMain.Image = dstImage;

      alphaImage.Dispose();
    }


    /// <summary>
    /// 获取透明度
    /// </summary>
    public byte Transparency
    {
      get
      {
        return (byte)this.transparencyTrackBar.Value;
      }
    }

  }
}