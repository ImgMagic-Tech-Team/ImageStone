using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class AngleDialog : Form
  {
    /// <summary>
    /// ֧�ַ���
    /// </summary>
    public enum SupportMethod
    {
      /// <summary>
      /// ��ת
      /// </summary>
      Rotate,

      /// <summary>
      /// ����ģ��
      /// </summary>
      RadialBlur,

      /// <summary>
      /// �Ҷȸ���
      /// </summary>
      Emboss,

      /// <summary>
      /// ��ɫ����
      /// </summary>
      Relief,

      /// <summary>
      /// ���ұ�Ե
      /// </summary>
      FindEdge,

      /// <summary>
      /// ��
      /// </summary>
      None
    }

    private Canvas canvas = null;
    Bitmap srcImage = null;
    private SupportMethod support = SupportMethod.None;

    public AngleDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void AngleDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void angleChooser_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        this.angleNumericUpDown.Value = this.angleChooser.Angle;
        UpdateCanvas();
      }
    }

    private void angleNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.angleChooser.Angle = (int)this.angleNumericUpDown.Value;
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      ImageTransform it = new ImageTransform();
      Effect e = new Effect();

      if (support == SupportMethod.Rotate)
      {
        dstImage = it.Rotate((Bitmap)srcImage.Clone(), this.Angle);
        this.canvas.Image = dstImage;
        return;
      }

      switch (support)
      {
        case SupportMethod.RadialBlur:
          dstImage = e.RadialBlur((Bitmap)srcImage.Clone(), this.Angle);
          break;

        case SupportMethod.Emboss:
          dstImage = e.Emboss((Bitmap)srcImage.Clone(), this.Angle);
          break;

        case SupportMethod.Relief:
          dstImage = e.Relief((Bitmap)srcImage.Clone(), this.Angle);
          break;

        case SupportMethod.FindEdge:
          dstImage = e.FindEdge((Bitmap)srcImage.Clone(), this.Angle);
          break;

        default:
          break;
      }

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// ��ȡ�������û�ָ���ĽǶ�ֵ
    /// </summary>
    public int Angle
    {
      get
      {
        return this.angleChooser.Angle;
      }
      set
      {
        this.angleChooser.Angle = value;
        this.angleNumericUpDown.Value = value; 
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
    public SupportMethod Support
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

  }
}