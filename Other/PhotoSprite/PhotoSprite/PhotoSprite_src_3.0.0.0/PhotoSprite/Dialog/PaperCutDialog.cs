using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class PaperCutDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public PaperCutDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void PaperCutDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
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

    private void bgLabel_Click(object sender, EventArgs e)
    {
      if (colorDialog.ShowDialog() == DialogResult.OK)
      {
        this.bgLabel.BackColor = colorDialog.Color;
        UpdateCanvas();
      }
    }

    private void fgLabel_Click(object sender, EventArgs e)
    {
      if (colorDialog.ShowDialog() == DialogResult.OK)
      {
        this.fgLabel.BackColor = colorDialog.Color;
        UpdateCanvas();
      }
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Effect e = new Effect();

      dstImage = e.PaperCut((Bitmap)srcImage.Clone(), this.Threshold, this.BgColor, this.FgColor);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }


    /// <summary>
    /// 获取阈值
    /// </summary>
    public byte Threshold
    {
      get
      {
        return (byte)this.thresholdTrackBar.Value;
      }
    }

    /// <summary>
    /// 获取背景色
    /// </summary>
    public Color BgColor
    {
      get
      {
        return this.bgLabel.BackColor;
      }
    }

    /// <summary>
    /// 获取前景色
    /// </summary>
    public Color FgColor
    {
      get
      {
        return this.fgLabel.BackColor;
      }
    }

  }
}