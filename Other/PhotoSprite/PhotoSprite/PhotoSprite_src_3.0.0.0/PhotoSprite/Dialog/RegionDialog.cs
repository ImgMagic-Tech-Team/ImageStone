using System;
using System.Drawing;
using System.Windows.Forms;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class RegionDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;
    bool isShowContour = false;

    public RegionDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void RegionDialog_Load(object sender, EventArgs e)
    {
      Segmentation s = new Segmentation();
      if (!isShowContour)
      {
        int[] Area = s.ImageArea((Bitmap)srcImage.Clone());
        int len = Area.Length;

        for (int i = 0; i < len; i++)
        {
          string no = "    " + i.ToString();
          no = no.Substring(no.Length - 5);
          string area = Area[i].ToString();
          this.regionListBox.Items.Add("����:" + no + "      ���:" + area);
        } // i
      }
      else
      {
        int[] Perimeter = s.ImagePerimeter((Bitmap)srcImage.Clone());
        int len = Perimeter.Length;

        for (int i = 0; i < len; i++)
        {
          string no = "    " + i.ToString();
          no = no.Substring(no.Length - 5);
          string perimeter = Perimeter[i].ToString();
          this.regionListBox.Items.Add("����:" + no + "      �ܳ�:" + perimeter);
        } // i
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void regionListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Segmentation s = new Segmentation();
      dstImage = s.ImageRegion((Bitmap)srcImage.Clone(), this.RegionShowing, this.IsShowContour);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// ��ȡҪ��ʾ������
    /// </summary>
    public ushort[] RegionShowing
    {
      get
      {
        int len = this.regionListBox.SelectedIndices.Count;
        int[] Region = new int[len];
        this.regionListBox.SelectedIndices.CopyTo(Region, 0);

        ushort[] ValidRegion = new ushort[len];
        for (int i = 0; i < len; i++)
        {
          ValidRegion[i] = (ushort)Region[i];
        } // i

        return ValidRegion;
      }
    }

    /// <summary>
    /// ��ȡ������ bool ֵ����ָʾ�Ƿ���ʾ����
    /// true ��ʾ������ false ��ʾ����
    /// </summary>
    public bool IsShowContour
    {
      get
      {
        return isShowContour;
      }
      set
      {
        isShowContour = value;
      }
    }

  }
}