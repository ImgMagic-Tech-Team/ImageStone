using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class RedEyeRemovalDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public RedEyeRemovalDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void RedEyeRemovalDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void tolerenceTrackBar_Scroll(object sender, EventArgs e)
    {
      this.tolerenceUpDown.Value = this.tolerenceTrackBar.Value;
    }

    private void tolerenceUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.tolerenceTrackBar.Value = (int)this.tolerenceUpDown.Value;
      UpdateCanvas();
    }

    private void colorLabel_Click(object sender, EventArgs e)
    {
      if (colorDialog.ShowDialog() == DialogResult.OK)
      {
        this.colorLabel.BackColor = colorDialog.Color;
        UpdateCanvas();
      }
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Effect e = new Effect();

      dstImage = e.RedEyeRemoval((Bitmap)srcImage.Clone(), this.Tolerence, this.EyeColor.R, this.EyeColor.G, this.EyeColor.B);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取容差
    /// </summary>
    public int Tolerence
    {
      get
      {
        return this.tolerenceTrackBar.Value;
      }
    }

    /// <summary>
    /// 获取眼睛颜色
    /// </summary>
    public Color EyeColor
    {
      get
      {
        return this.colorLabel.BackColor;
      }
    }

  }
}