using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Tool
{
  /// <summary>
  /// ֱ�߹�����
  /// </summary>
  public class LineTool : Tool
  {
    private Canvas canvas = null;
    private Bitmap image = null;
    private Bitmap srcImage = null;
    private Graphics gImage;
    private Graphics gSurface;
    private bool mouseDown = false;
    private Point startPoint, lastPoint;
    private Pen pen = null;
    
    /// <summary>
    /// ʹ��ֱ�߹���
    /// </summary>
    /// <param name="parent">����</param>
    public LineTool(Canvas parent)
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
        lastPoint = startPoint = new Point(e.X, e.Y);

        // ���Ϊǰ����ɫ���Ҽ�Ϊ������ɫ
        if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        {
          pen = new Pen(new SolidBrush(this.ForeColor), this.BrushWidth);
        }
        else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
        {
          pen = new Pen(new SolidBrush(this.BackColor), this.BrushWidth);
        }

        // ��������ͼ��
        this.image = this.canvas.Image;
        this.srcImage = (Bitmap)this.image.Clone();
        gImage = System.Drawing.Graphics.FromImage(image);
      }
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        Point mouseXY = new Point(e.X, e.Y);
        mouseXY = Function.PointInRectangle(mouseXY, this.canvas.Bounds);
        if (mouseXY.X == this.canvas.Width - 1) mouseXY.X = this.canvas.Width - 2;
        if (mouseXY.Y == this.canvas.Height - 1) mouseXY.Y = this.canvas.Height - 2;

        // �����ϴκۼ�
        Rectangle lastRect = Function.PointsToRectangle(startPoint, lastPoint);
        lastRect.Inflate(2, 2);
        Region invalidRegion = new Region(lastRect);
        this.canvas.Invalidate(invalidRegion);
        this.gImage.DrawImage((Bitmap)this.srcImage.Clone(), 
          lastRect, lastRect, GraphicsUnit.Pixel);

        // ���Ʊ���
        this.gSurface.DrawLine(pen, mouseXY, startPoint);

        // ���Ƶ�ͼ��
        this.gImage.DrawLine(pen, mouseXY, startPoint);

        lastPoint = mouseXY;
      }
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        Point mouseXY = new Point(e.X, e.Y);

        // ����ֱ��
        Graphics g = Graphics.FromImage(this.srcImage);
        g.DrawLine(pen, mouseXY, startPoint);
        this.gImage.DrawImage((Bitmap)this.srcImage.Clone(), 0,0);

        startPoint = Point.Empty;
        lastPoint = Point.Empty;
        mouseDown = false;

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
