using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PhotoSprite
{
  public partial class AngleChooser : UserControl
  {
    public AngleChooser()
    {
      InitializeComponent();

      this.ResizeRedraw = true;
    }

    private Point lastMouseXY;

    // 用于双缓存
    private Bitmap buffer = null;

    private int angle = 0;

    /// <summary>
    /// 获取或设置角度值
    /// </summary> 
    public int Angle
    {
      get
      {
        if (angle < 0) angle = 360+angle;
        return angle;
      }

      set
      {
        int v = value % 360;
        if (angle != v)
        {
          angle = v;
          Invalidate();
        }
      }
    }


    private void DrawToGraphics(Graphics g)
    {
      g.Clear(this.BackColor);
      g.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle ourRect = Rectangle.Inflate(ClientRectangle, -2, -2);
      int diameter = Math.Min(ourRect.Width, ourRect.Height);


      Point center = new Point(ourRect.X + (diameter / 2), ourRect.Y + (diameter / 2));
      double radius = ((double)diameter / 2);
      double theta = ((double)angle * 2 * Math.PI) / 360.0;

      Point endPoint = new Point(center.X + (int)(radius * Math.Cos(theta)),
          center.Y - (int)(radius * Math.Sin(theta)));

      g.DrawLine(SystemPens.ControlDark, center, new Point(center.X + (diameter / 2), center.Y));
      g.DrawEllipse(SystemPens.ControlDarkDark, new Rectangle(new Point(ourRect.X - 1, ourRect.Y - 1), new Size(diameter, diameter)));
      g.DrawEllipse(SystemPens.ControlLightLight, new Rectangle(ourRect.Location, new Size(diameter, diameter)));
      g.DrawLine(SystemPens.ControlText, center, endPoint);
    }

    private void CheckRenderSurface()
    {
      if (buffer == null || buffer.Size != Size)
      {
        buffer = new Bitmap(Width, Height);

        using (Graphics g = Graphics.FromImage(buffer))
        {
          DrawToGraphics(g);
        }
      }
    }

    private void DoPaint(Graphics g)
    {
      CheckRenderSurface();
      g.DrawImage(buffer, ClientRectangle, ClientRectangle, GraphicsUnit.Pixel);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      buffer = null;
      DoPaint(e.Graphics);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      lastMouseXY = new Point(e.X, e.Y);

      if (e.Button==MouseButtons.Left)
      {
        Rectangle ourRect = Rectangle.Inflate(ClientRectangle, -2, -2);
        int diameter = Math.Min(ourRect.Width, ourRect.Height);
        Point center = new Point(ourRect.X + (diameter / 2), ourRect.Y + (diameter / 2));

        int dx = e.X - center.X;
        int dy = e.Y - center.Y;
        double theta = Math.Atan2(-dy, dx);
        Angle = (int)((theta * 360) / (2 * Math.PI));
      }
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      OnMouseMove(new MouseEventArgs(MouseButtons.Left, 1, lastMouseXY.X, lastMouseXY.Y, 0));
    }

  }
}
