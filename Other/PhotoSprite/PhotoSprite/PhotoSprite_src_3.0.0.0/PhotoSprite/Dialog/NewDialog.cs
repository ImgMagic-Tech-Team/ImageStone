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
      // 查看内存中是否有图像
      // 如果有，则将内存中图像的宽高作为新建图像的宽高
      if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap))
      {
        Bitmap newImage = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
        this.widthUpDown.Value = newImage.Width;
        this.heightUpDown.Value = newImage.Height;
      }
    }

    private void widthUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.groupBox.Text = "文件尺寸: " + EstimateFileSize() + " MB";
    }

    private void heightUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.groupBox.Text = "文件尺寸: " + EstimateFileSize() + " MB";
    }

    /// <summary>
    /// 估计文件尺寸
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
    /// 获取或设置新建图像宽度
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
    /// 获取或设置新建图像高度
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