using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class FilterDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;
    private Filter.FilteringMethod support = Filter.FilteringMethod.Mean;
    private bool isWindowFiltering = true;

    public FilterDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void FilterDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void nTrackBar_Scroll(object sender, EventArgs e)
    {
      if (this.nTrackBar.Value % 2 == 0)
        this.nTrackBar.Value++;
      this.nUpDown.Value = this.nTrackBar.Value;
    }

    private void nUpDown_ValueChanged(object sender, EventArgs e)
    {
      if ((int)this.nUpDown.Value % 2 == 0)
        this.nUpDown.Value++;

      this.nTrackBar.Value = (int)this.nUpDown.Value;

      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Filter f = new Filter();

      if (this.IsWindowFiltering)
        dstImage = f.FilterNxN((Bitmap)srcImage.Clone(), this.N, this.Support);
      else
        dstImage = f.FilterCross((Bitmap)srcImage.Clone(), this.N, this.Support);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }


    /// <summary>
    /// ��ȡ�˲����ڴ�С��NΪ����
    /// </summary>
    public int N
    {
      get
      {
        return this.nTrackBar.Value;
      }
      set
      {
        this.nTrackBar.Value = value;
        this.nUpDown.Value = value;
      }
    }

    /// <summary>
    /// ��ȡ�����ô������ֵ
    /// </summary>
    public int Maximum
    {
      get
      {
        return this.nTrackBar.Maximum;
      }
      set
      {
        this.nTrackBar.Maximum = value;
        this.nUpDown.Maximum = value;
      }
    }

    /// <summary>
    /// ��ȡ�����ô�����Сֵ
    /// </summary>
    public int Minimum
    {
      get
      {
        return this.nTrackBar.Minimum;
      }
      set
      {
        this.nTrackBar.Minimum = value;
        this.nUpDown.Minimum = value;
      }
    }

    /// <summary>
    /// ��ȡ����������
    /// </summary>
    public string Description
    {
      get
      {
        return this.groupBox.Text;
      }
      set
      {
        this.groupBox.Text = value;
      }
    }

    /// <summary>
    /// ��ȡ������֧�ַ���
    /// </summary>
    public Filter.FilteringMethod Support
    {
      get
      {
        return support;
      }
      set
      {
        support = value;
      }
    }

    /// <summary>
    /// ��ȡ������ bool ֵ����ָʾ�Ƿ񰴴�����ʽ�����˲�
    /// true ��������ʽ�����˲��� false ��ʮ�ּ���ʽ�����˲�
    /// </summary>
    public bool IsWindowFiltering
    {
      get
      {
        return this.isWindowFiltering;
      }
      set
      {
        isWindowFiltering = value;
      }
    }

  }
}