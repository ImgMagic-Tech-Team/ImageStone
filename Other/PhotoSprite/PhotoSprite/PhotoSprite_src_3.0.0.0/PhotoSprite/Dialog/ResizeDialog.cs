using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhotoSprite.Dialog
{
  public partial class ResizeDialog : Form
  {
    double aspectRatio = 1.3333;
    bool locked = false;

    public ResizeDialog(Bitmap b)
    {
      InitializeComponent();

      aspectRatio = (double)b.Width / (double)b.Height;

      this.widthUpDown.Value = b.Width;
      this.heightUpDown.Value = b.Height;
    }

    private void widthUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (locked) return;

      if (this.constrainCheckBox.Checked)
      {
        locked = true;
        this.heightUpDown.Value = (int)((double)this.widthUpDown.Value / aspectRatio + 0.5);
        locked = false;
      }
    }

    private void heightUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (locked) return;
      
      if (this.constrainCheckBox.Checked)
      {
        locked = true;
        this.widthUpDown.Value = (int)((double)this.heightUpDown.Value * aspectRatio + 0.5);
        locked = false;
      }
    }


    /// <summary>
    /// ��ȡ������ͼ���������
    /// </summary>
    public int NewWidth
    {
      get
      {
        return (int)widthUpDown.Value;
      }

      set
      {
        widthUpDown.Value = value;
      }
    }

    /// <summary>
    /// ��ȡ������ͼ�������߶�
    /// </summary>
    public int NewHeight
    {
      get
      {
        return (int)heightUpDown.Value;
      }
      set
      {
        heightUpDown.Value = value;
      }
    }

  }
}