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
  /// 矩形选择工具类
  /// </summary>
  public class RectangleSelectTool
  {
    private Canvas canvas = null;
    private bool mouseDown = false;
    private Point startPoint;
    private bool hasClosed = false;

    /// <summary>
    /// 使用矩形选择工具
    /// </summary>
    /// <param name="parent">画布</param>
    public RectangleSelectTool(Canvas parent)
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
        startPoint = new Point(e.X, e.Y);
        hasClosed = false;
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
        Rectangle rect = Function.PointsToRectangle(startPoint, mouseXY);

        this.canvas.SelectedPath.Reset();
        this.canvas.SelectedPath.AddRectangle(rect);

        hasClosed = true;
      }
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        startPoint = Point.Empty;
        mouseDown = false;
        hasClosed = false;
      }
    }

    public void OnClick(object sender, EventArgs e)
    {
      if (!hasClosed)
      {
        this.canvas.SelectedPath.Reset();
      }

      this.canvas.Invalidate();

      mouseDown = false;
      hasClosed = false;
    }

  }
}
