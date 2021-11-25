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
  /// 索套工具类
  /// </summary>
  public class LassoSelectTool
  {
    private Canvas canvas = null;
    private bool mouseDown = false;
    private ArrayList tracePoints;
    private bool hasClosed = false;

    /// <summary>
    /// 使用索套工具
    /// </summary>
    /// <param name="parent">画布</param>
    public LassoSelectTool(Canvas parent)
    {
      this.canvas = parent;
    }

    public void OnActivate()
    {
      this.canvas.MouseDown += this.OnMouseDown;
      this.canvas.MouseMove += this.OnMouseMove;
      this.canvas.MouseUp += this.OnMouseUp;
      this.canvas.Click += this.OnClick;
    }

    public void OnDeactivate()
    {
      this.canvas.SelectedPath.Reset();
      this.canvas.Invalidate();

      this.canvas.MouseDown -= this.OnMouseDown;
      this.canvas.MouseMove -= this.OnMouseMove;
      this.canvas.MouseUp -= this.OnMouseUp;
      this.canvas.Click -= this.OnClick;
    }

    public void OnMouseDown(object sender, MouseEventArgs e)
    {
      if (((e.Button & MouseButtons.Left) == MouseButtons.Left))
      {
        mouseDown = true;
        tracePoints = new ArrayList();
        tracePoints.Add(new Point(e.X, e.Y));
        hasClosed = false;
      }
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        Point mouseXY = new Point(e.X, e.Y);
        mouseXY = Function.PointInRectangle(mouseXY, this.canvas.Bounds);
        tracePoints.Add(mouseXY);

        if (tracePoints.Count > 2)
        {
          Point[] polygon = (Point[])tracePoints.ToArray(typeof(Point));

          this.canvas.SelectedPath.Reset();
          this.canvas.SelectedPath.AddPolygon(polygon);
        }

        if (mouseXY != (Point)tracePoints[0])
        {
          hasClosed = true;
        }
      }
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        if (hasClosed)
        {
          if (tracePoints.Count > 2)
          {
            this.canvas.SelectedPath.CloseFigure();
          }
        }

        mouseDown = false;
        hasClosed = false;
      }
    }

    public void OnClick(object sender, EventArgs e)
    {
      if (tracePoints == null || tracePoints.Count == 0)
      {
        return;
      }

      if (!hasClosed || tracePoints.Count <= 2)
      {
        this.canvas.SelectedPath.Reset();
      }

      this.canvas.Invalidate();

      mouseDown = false;
      tracePoints = null;
      hasClosed = false;
    }

  }
}
