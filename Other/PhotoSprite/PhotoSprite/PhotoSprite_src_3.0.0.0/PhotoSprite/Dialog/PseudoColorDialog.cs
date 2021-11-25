using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class PseudoColorDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    Color[] colorTable = new Color[16];

    /// <summary>
    /// 获取色彩映射表
    /// </summary>
    public Color[] ColorTable
    {
      get
      {
        return colorTable;
      }
    }

    public PseudoColorDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void PseudoColorDialog_Load(object sender, EventArgs e)
    {
      // 初始化色彩映射表
      colorTable[0] = Color.FromArgb(0x00, 0x00, 0x00);
      colorTable[1] = Color.FromArgb(0x00, 0x00, 0x55);
      colorTable[2] = Color.FromArgb(0x00, 0x55, 0x00);
      colorTable[3] = Color.FromArgb(0x55, 0x00, 0x00);
      colorTable[4] = Color.FromArgb(0x3F, 0x3F, 0x3F);
      colorTable[5] = Color.FromArgb(0x55, 0x00, 0x55);
      colorTable[6] = Color.FromArgb(0x00, 0x00, 0xFF);
      colorTable[7] = Color.FromArgb(0x55, 0x55, 0x00);
      colorTable[8] = Color.FromArgb(0x00, 0xFF, 0x00);
      colorTable[9] = Color.FromArgb(0xFF, 0x00, 0x00);
      colorTable[10] = Color.FromArgb(0x80, 0x80, 0x80);
      colorTable[11] = Color.FromArgb(0x00, 0xFF, 0xFF);
      colorTable[12] = Color.FromArgb(0xFF, 0xFF, 0x00);
      colorTable[13] = Color.FromArgb(0xFF, 0xFF, 0xFF);
      colorTable[14] = Color.FromArgb(0x00, 0x55, 0x55);
      colorTable[15] = Color.FromArgb(0xFF, 0x00, 0xFF);

      // 初始化面板背景色
      this.label11.BackColor = colorTable[0];
      this.label12.BackColor = colorTable[1];
      this.label13.BackColor = colorTable[2];
      this.label14.BackColor = colorTable[3];
      this.label21.BackColor = colorTable[4];
      this.label22.BackColor = colorTable[5];
      this.label23.BackColor = colorTable[6];
      this.label24.BackColor = colorTable[7];
      this.label31.BackColor = colorTable[8];
      this.label32.BackColor = colorTable[9];
      this.label33.BackColor = colorTable[10];
      this.label34.BackColor = colorTable[11];
      this.label41.BackColor = colorTable[12];
      this.label42.BackColor = colorTable[13];
      this.label43.BackColor = colorTable[14];
      this.label44.BackColor = colorTable[15];

      UpdateCanvas();
    }

    private void colorTableRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      this.groupBox.Enabled = this.colorTableRadioButton.Checked;
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void ChangeBackColor( System.Windows.Forms.Label sender)
    {
      if (colorDialog.ShowDialog() == DialogResult.OK)
      {
        // 将面板背景色设为指定色
        sender.BackColor = colorDialog.Color;

        // 将色彩表设为指定色
        string RowCol = sender.Name.Substring(sender.Name.Length - 2);
        int iRowCol = Convert.ToInt32(RowCol);
        int idx = (iRowCol / 10 - 1) * 4 + (iRowCol % 10 - 1);
        colorTable[idx] = colorDialog.Color;

        UpdateCanvas();
      }
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Adjustment a = new Adjustment();

      if (this.curveRadioButton.Checked)
      {
        dstImage = a.PseudoColor((Bitmap)srcImage.Clone());
      }
      else
      {
        dstImage = a.PseudoColor((Bitmap)srcImage.Clone(), this.ColorTable);
      }

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    private void label11_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label12_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label13_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label14_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label21_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label22_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label23_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label24_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label31_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label32_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label33_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label34_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label41_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label42_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label43_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label44_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }


  }
}