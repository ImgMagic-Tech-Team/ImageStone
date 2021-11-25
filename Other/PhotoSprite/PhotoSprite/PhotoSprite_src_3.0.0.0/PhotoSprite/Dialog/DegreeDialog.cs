using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class DegreeDialog : Form
  {
    /// <summary>
    /// 支持方法
    /// </summary>
    public enum SupportMethod
    {
      /// <summary>
      /// 亮度
      /// </summary>
      Brightness,

      /// <summary>
      /// 对比度
      /// </summary>
      Contrast,

      /// <summary>
      /// 阈值化
      /// </summary>
      Thresholding,

      /// <summary>
      /// 锐化
      /// </summary>
      Sharpen,

      /// <summary>
      /// 钝化蒙板
      /// </summary>
      UnsharpMask,

      /// <summary>
      /// 新增杂点
      /// </summary>
      AddNoise,

      /// <summary>
      /// 雪花杂点
      /// </summary>
      Sprinkle,

      /// <summary>
      /// 挤压
      /// </summary>
      Pinch,

      /// <summary>
      /// 漩涡
      /// </summary>
      Swirl,

      /// <summary>
      /// 波浪
      /// </summary>
      Wave,

      /// <summary>
      /// 摩尔纹
      /// </summary>
      MoireFringe,

      /// <summary>
      /// 扩散
      /// </summary>
      Diffuse,

      /// <summary>
      /// 灯光
      /// </summary>
      Lighting,

      /// <summary>
      /// 马赛克
      /// </summary>
      Mosaic,

      /// <summary>
      /// 消除小区域
      /// </summary>
      ClearSmallArea,

      /// <summary>
      /// 边缘增强
      /// </summary>
      EdgeEnhance,

      /// <summary>
      /// 边缘均衡化
      /// </summary>
      EdgeHomogenize,

      /// <summary>
      /// 无
      /// </summary>
      None
    }

    private Canvas canvas = null;
    Bitmap srcImage = null;
    private SupportMethod support = SupportMethod.None;

    public DegreeDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void DegreeDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void degreeTrackBar_Scroll(object sender, EventArgs e)
    {
      this.degreeUpDown.Value = this.degreeTrackBar.Value;
    }

    private void degreeUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.degreeTrackBar.Value = (int)this.degreeUpDown.Value;
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      GrayProcessing gp = new GrayProcessing();
      Adjustment a = new Adjustment();
      Effect e = new Effect();
      Segmentation s = new Segmentation();
      EdgeDetect ed = new EdgeDetect();

      switch (support)
      {
        case SupportMethod.Brightness: 
          dstImage = a.Brightness((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.Contrast:
          dstImage = a.Contrast((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.Thresholding:
          dstImage = gp.Thresholding((Bitmap)srcImage.Clone(), (byte)this.Degree);
          break;

        case SupportMethod.Sharpen:
          dstImage = e.Sharpen((Bitmap)srcImage.Clone(), (byte)this.Degree);
          break;

        case SupportMethod.UnsharpMask:
          dstImage = e.UnsharpMask((Bitmap)srcImage.Clone(), (byte)this.Degree);
          break;

        case SupportMethod.AddNoise:
          dstImage = e.AddNoise((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.Sprinkle:
          dstImage = e.Sprinkle((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.Pinch:
          dstImage = e.Pinch((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.Swirl:
          dstImage = e.Swirl((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.Wave:
          dstImage = e.Wave((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.MoireFringe:
          dstImage = e.MoireFringe((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.Diffuse:
          dstImage = e.Diffuse((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.Lighting:
          dstImage = e.Lighting((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.Mosaic:
          dstImage = e.Mosaic((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.ClearSmallArea:
          dstImage = s.ClearSmallArea((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.EdgeEnhance:
          dstImage = ed.EdgeEnhance((Bitmap)srcImage.Clone(), this.Degree);
          break;

        case SupportMethod.EdgeHomogenize:
          dstImage = ed.EdgeHomogenize((Bitmap)srcImage.Clone(), this.Degree);
          break;

        default:
          break;
      }

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }


    /// <summary>
    /// 获取或设置幅度
    /// </summary>
    public int Degree
    {
      get
      {
        return this.degreeTrackBar.Value;
      }
      set
      {
        this.degreeTrackBar.Value = value;
        this.degreeUpDown.Value = value;
      }
    }

    /// <summary>
    /// 获取或设置幅度最大值
    /// </summary>
    public int Maximum
    {
      get
      {
        return this.degreeTrackBar.Maximum;
      }
      set
      {
        this.degreeTrackBar.Maximum = value;
        this.degreeUpDown.Maximum = value;
      }
    }

    /// <summary>
    /// 获取或设置幅度最小值
    /// </summary>
    public int Minimum
    {
      get
      {
        return this.degreeTrackBar.Minimum;
      }
      set
      {
        this.degreeTrackBar.Minimum = value;
        this.degreeUpDown.Minimum = value;
      }
    }

    /// <summary>
    /// 获取或设置描述
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
    /// 获取或设置支持方法
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