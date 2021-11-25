using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class TrimDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public TrimDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void UpdateCanvas()
    {
      ImageTransform it = new ImageTransform();

      Bitmap dstImage = it.Trim((Bitmap)srcImage.Clone(), this.TrimAway);

      this.canvas.Image = dstImage;
    }

    private void topCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void bottomCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void leftCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void rightCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    /// <summary>
    /// »ñÈ¡ÐÞÕû·¶Î§
    /// </summary>
    public ImageTransform.TrimMode TrimAway
    {
      get
      {
        ImageTransform.TrimMode trimAway = ImageTransform.TrimMode.None;
        if (topCheckBox.Checked) trimAway |= ImageTransform.TrimMode.Top;
        if (bottomCheckBox.Checked) trimAway |= ImageTransform.TrimMode.Bottom;
        if (leftCheckBox.Checked) trimAway |= ImageTransform.TrimMode.Left;
        if (rightCheckBox.Checked) trimAway |= ImageTransform.TrimMode.Right;

        return trimAway;
      }
    }

  }
}