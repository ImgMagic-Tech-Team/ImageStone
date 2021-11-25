using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite
{
  public partial class Canvas : UserControl
  {
    private Bitmap image = null;
    private string imageFile = "";
    private double zoom = 1.0;

    private GraphicsPath selectedPath = new GraphicsPath();
    private int dancingAnts = 0;
    private static Pen outlinePen = null;
    private static Pen antPen = null;


    /// <summary>
    /// ��ȡ������ͼ��
    /// </summary>
    public Bitmap Image
    {
      get
      {
        return image;
      }
      set
      {
        image = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// ��ȡ������ͼ���ļ�
    /// </summary>
    public string ImageFile
    {
      get
      {
        return imageFile;
      }
      set
      {
        imageFile = value;

        if (imageFile != "")
        {
          Graphic myGraphic = new Graphic();
          this.Image = myGraphic.Open(imageFile);
        }
      }
    }

    /// <summary>
    /// ��ȡ�����û�������ϵ��
    /// </summary>
    public double Zoom
    {
      get
      {
        return zoom;
      }
      set
      {
        zoom = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// ��ȡ������ѡ��
    /// </summary>
    public GraphicsPath SelectedPath
    {
      get
      {
        return selectedPath;
      }
      set
      {
        if (selectedPath != null)
          selectedPath.Reset();
        selectedPath = value;
      }
    }

    /// <summary>
    /// ��ȡ bool ֵ����ָʾ�Ƿ�ѡ��Ϊ��
    /// </summary>
    public bool IsSelectionEmpty
    {
      get
      {
        return selectedPath.PointCount == 0;
      }
    }

    /// <summary>
    /// ��ȡѡ��
    /// </summary>
    public Region SelectedRegion
    {
      get
      {
        if (IsSelectionEmpty)
          return new Region(new Rectangle(0, 0, this.Size.Width, this.Size.Height));
        else
          return new Region(selectedPath);
      }
    }


    /// <summary>
    /// ����
    /// </summary>
    public Canvas()
    {
      InitializeComponent();

      this.ResizeRedraw = true;
      this.SetStyle( ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer, true);

      this.SetStyle(ControlStyles.UserMouse, true);  //�����������¼�
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      // ���Ʊ��������ڰ׷��齻����ֵĻ�����ʽ����͸������
      Color black = Color.FromArgb(0xFF, 0xDF, 0xDF, 0xDF);
      Color white = Color.White;

      Bitmap buffer = new Bitmap(16, 16);
      Graphics g = System.Drawing.Graphics.FromImage(buffer);
      g.FillRectangle(new SolidBrush(black), 0, 0, 8, 8);
      g.FillRectangle(new SolidBrush(white), 8, 0, 8, 8);
      g.FillRectangle(new SolidBrush(white), 0, 8, 8, 8);
      g.FillRectangle(new SolidBrush(black), 8, 8, 8, 8);

      this.BackgroundImage = buffer;
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      //base.OnPaint(e);

      // ����ͼ��
      if (image != null)
      {
        this.Size = new Size((int)(this.image.Width * zoom), (int)(this.image.Height * zoom));

        e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        e.Graphics.DrawImage(image,
          new Rectangle(0, 0, this.Width, this.Height),
          new Rectangle(0, 0, image.Width, image.Height),
          GraphicsUnit.Pixel
          );
      }

      if (!IsSelectionEmpty)
      {
        DrawSelectionOutline(e.Graphics, selectedPath);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
    }

    private void DrawSelectionOutline(Graphics g, GraphicsPath outline)
    {
      if (outline == null)
      {
        return;
      }

      g.SmoothingMode = SmoothingMode.AntiAlias;

      // ����������
      if (outlinePen == null)
      {
        outlinePen = new Pen(Color.FromArgb(200, Color.Black), 1.0f);
        outlinePen.Alignment = PenAlignment.Outset;
        outlinePen.LineJoin = LineJoin.Round;
      }

      // ����������
      if (antPen == null)
      {
        antPen = new Pen(Color.White, 1.0f);
        antPen.Alignment = PenAlignment.Outset;
        antPen.LineJoin = LineJoin.Round;
        antPen.MiterLimit = 2;
        antPen.Width = 1.0f;
        antPen.DashStyle = DashStyle.Dash;
        antPen.DashPattern = new float[] { 4, 4 };
        antPen.DashOffset = 4.0f;
      }
      g.PixelOffsetMode = PixelOffsetMode.Default;
      g.DrawPath(outlinePen, outline);
      antPen.DashOffset = dancingAnts;
      g.DrawPath(antPen, outline);

      // ���һ����ɫ����
      g.FillPath(new SolidBrush(Color.FromArgb(60, Color.Cyan)), outline);
    }

    private void selectionTimer_Tick(object sender, EventArgs e)
    {
      if (!IsSelectionEmpty)
      {
        dancingAnts = unchecked(dancingAnts + 1);
        this.Invalidate();
      }
    }

  }
}
