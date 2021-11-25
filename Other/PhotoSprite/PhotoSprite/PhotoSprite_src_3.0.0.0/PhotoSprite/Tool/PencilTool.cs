using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PhotoSprite.Tool
{
  /// <summary>
  /// 铅笔工具类
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
    /// 使用铅笔工具
    /// </summary>
    /// <param name="parent">画布</param>
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

        // 添加起始点
        tracePoints.Add(new Point(e.X, e.Y));

        // 左键为前景颜色，右键为背景颜色
        if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        {
          pen = new Pen(new SolidBrush(this.ForeColor), 1);
        }
        else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
        {
          pen = new Pen(new SolidBrush(this.BackColor), 1);
        }

        // 锁定画布图像
        this.gImage = System.Drawing.Graphics.FromImage(this.canvas.Image);
      }
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        Point mouseXY = new Point(e.X, e.Y);

        // 如果鼠标未移动，则取消绘制
        // 否则记录下当前点
        if ((tracePoints.Count > 0) && 
          ((Point)tracePoints[tracePoints.Count - 1] == mouseXY))
        {
          return;
        }
        else
        {
          tracePoints.Add(mouseXY);
        }

        // 沿跟踪点进行画线
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

        // 触发绘图结束事件
        OnDrawingFinished();
      }
    }


    /// <summary>
    /// 在绘图结束时发生
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
