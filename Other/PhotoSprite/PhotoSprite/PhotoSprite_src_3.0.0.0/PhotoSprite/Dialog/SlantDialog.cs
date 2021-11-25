using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class SlantDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public SlantDialog(Canvas parent)
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

    private void UpdateCanvas()
    {
      Bitmap dstImage = (Bitmap)srcImage.Clone();
      ImageTransform it = new ImageTransform();

      if (this.Horizontal != 0)
        dstImage = it.SlantHorz(dstImage, this.Horizontal);
      if (this.Vertical != 0)
        dstImage = it.SlantVert(dstImage, this.Vertical);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取水平倾斜量
    /// </summary>
    public int Horizontal
    {
      get
      {
        return (int)this.horzUpDown.Value;
      }
    }

    /// <summary>
    /// 获取垂直倾斜量
    /// </summary>
    public int Vertical
    {
      get
      {
        return (int)this.vertUpDown.Value;
      }
    }

  }
}