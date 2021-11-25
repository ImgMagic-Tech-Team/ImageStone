using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PhotoSprite.Tool
{
  /// <summary>
  /// ��ˢ������
  /// </summary>
  public class BrushTool : Tool
  {
    private Canvas canvas = null;
    private Graphics gImage;
    private Graphics gSurface;
    private bool mouseDown = false;
    private ArrayList tracePoints;
    private Brush brush = null;
    private Size brushSize = new Size(2, 2);

    /// <summary>
    /// ʹ�û�ˢ����
    /// </summary>
    /// <param name="parent">����</param>
    public BrushTool(Canvas parent)
    {
      this.canvas = parent;
      gSurface = canvas.CreateGraphics();
    }

    public void OnActivate()
    {
      this.canvas.MouseDown += this.OnMouseDown;
      this.canvas.MouseMove += this.OnMouseMove;
      this.canvas.MouseUp += this.OnMouseUp;
    }

    public void OnDeactivate()
    {
      this.canvas.MouseDown -= this.OnMouseDown;
      this.canvas.MouseMove -= this.OnMouseMove;
      this.canvas.MouseUp -= this.OnMouseUp;
    }

    public void OnMouseDown(object sender, MouseEventArgs e)
    {
      if (((e.Button & MouseButtons.Left) == MouseButtons.Left) ||
        ((e.Button & MouseButtons.Right) == MouseButtons.Right))
      {
        mouseDown = true;
        tracePoints = new ArrayList();

        // �����ʼ��
        tracePoints.Add(new Point(e.X, e.Y));

        // ���Ϊǰ��������ɫ
        // �Ҽ�Ϊ����ǰ����ɫ
        if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        {
          if (this.HasBrushStyle)
            brush = new HatchBrush(this.FillStyle, this.ForeColor, this.BackColor);
          else
            brush = new SolidBrush(this.ForeColor);
        }
        else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
        {
          if (this.HasBrushStyle)
            brush = new HatchBrush(this.FillStyle, this.BackColor, this.ForeColor);
          else
            brush = new SolidBrush(this.BackColor);
        }
        brushSize = new Size(this.BrushWidth, this.BrushWidth);

        // ��������ͼ��
        this.gImage = System.Drawing.Graphics.FromImage(this.canvas.Image);
      }
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        Point mouseXY = new Point(e.X, e.Y);

        // ������δ�ƶ�����ȡ������
        // �����¼�µ�ǰ��
        if ((tracePoints.Count > 0) && 
          ((Point)tracePoints[tracePoints.Count - 1] == mouseXY))
        {
          return;
        }
        else
        {
          tracePoints.Add(mouseXY);
        }

        Point center = mouseXY;
        center.X -= brushSize.Width / 2;
        center.Y -= brushSize.Height / 2;
        Rectangle rect = new Rectangle(center, brushSize);

        // �ظ��ٵ���л���
        this.gSurface.FillEllipse(brush, rect);
        this.gImage.FillEllipse(brush, rect);
      }
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        mouseDown = false;
        tracePoints = null;

        brush.Dispose();
        this.canvas.Invalidate();

        // ������ͼ�����¼�
        OnDrawingFinished();
      }
    }


    /// <summary>
    /// �ڻ�ͼ����ʱ����
    /// </summary>
    public event EventHandler DrawingFinished;
    protected void OnDrawingFinished()
    {
      if (DrawingFinished != null)
      {
        DrawingFinished(this, EventArgs.Empty);
      }
    }

  }
}
