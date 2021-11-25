using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhotoSprite.Dialog
{
  public partial class NewDialog : Form
  {
    public NewDialog()
    {
      InitializeComponent();
    }

    private void NewDialog_Load(object sender, EventArgs e)
    {
      // �鿴�ڴ����Ƿ���ͼ��
      // ����У����ڴ���ͼ��Ŀ����Ϊ�½�ͼ��Ŀ��
      if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap))
      {
        Bitmap newImage = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
        this.widthUpDown.Value = newImage.Width;
        this.heightUpDown.Value = newImage.Height;
      }
    }

    private void widthUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.groupBox.Text = "�ļ��ߴ�: " + EstimateFileSize() + " MB";
    }

    private void heightUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.groupBox.Text = "�ļ��ߴ�: " + EstimateFileSize() + " MB";
    }

    /// <summary>
    /// �����ļ��ߴ�
    /// </summary>
    /// <returns></returns>
    private double EstimateFileSize()
    { 
      int width = (int)widthUpDown.Value;
      int height = (int)heightUpDown.Value;
      double fileSize = width*height*4/1024.0/1024.0;
      return ((int)(fileSize * 100) / 100.0);
    }

    /// <summary>
    /// ��ȡ�������½�ͼ����
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
    /// ��ȡ�������½�ͼ��߶�
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