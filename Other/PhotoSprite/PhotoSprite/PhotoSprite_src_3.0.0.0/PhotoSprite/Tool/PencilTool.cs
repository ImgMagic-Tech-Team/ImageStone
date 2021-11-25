using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PhotoSprite.Tool
{
  /// <summary>
  /// Ǧ�ʹ�����
  /// </summary>
  public class PencilTool : Tool
  {
    private Canvas canvas = null;
    private Graphics gImage;
    private Graphics gSurface;
    private bool mouseDown = false;
    private ArrayList tracePoints;
    private Pen pen = null;

    /// <summary>
    /// ʹ��Ǧ�ʹ���
    /// </summary>
    /// <param name="parent">����</param>
    public PencilTool(Canvas parent)
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

        // ���Ϊǰ����ɫ���Ҽ�Ϊ������ɫ
        if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        {
          pen = new Pen(new SolidBrush(this.ForeColor), 1);
        }
        else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
        {
          pen = new Pen(new SolidBrush(this.BackColor), 1);
        }

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

        // �ظ��ٵ���л���
        this.gSurface.DrawLine(pen, 
          (Point)tracePoints[tracePoints.Count - 1], 
          (Point)tracePoints[tracePoints.Count - 2]);
        this.gImage.DrawLine(pen, 
          (Point)tracePoints[tracePoints.Count - 1], 
          (Point)tracePoints[tracePoints.Count - 2]);
      }
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        mouseDown = false;
        tracePoints = null;

        pen.Dispose();
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
