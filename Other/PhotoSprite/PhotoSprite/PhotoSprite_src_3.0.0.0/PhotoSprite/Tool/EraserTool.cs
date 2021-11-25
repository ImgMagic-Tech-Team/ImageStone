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
  /// 橡皮擦工具类
  /// </summary>
  public class EraserTool : Tool
  {
    private Canvas canvas = null;
    private bool mouseDown = false;
    private ArrayList tracePoints;

    /// <summary>
    /// 使用橡皮擦工具
    /// </summary>
    /// <param name="parent">画布</param>
    public EraserTool(Canvas parent)
    {
      this.canvas = parent;
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

        tracePoints.Add(new Point(e.X, e.Y));
      }
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        Point mouseXY = new Point(e.X, e.Y);

        if ((tracePoints.Count > 0) && ((Point)tracePoints[tracePoints.Count - 1] == mouseXY))
        {
          return;
        }

        if (!(tracePoints.Count > 0 && mouseXY == (Point)tracePoints[tracePoints.Count - 1]))
        {
          tracePoints.Add(mouseXY);
        }

        int penWidth = this.BrushWidth;
        Point[] points = Function.GetLinePoints((Point)tracePoints[tracePoints.Count - 1], (Point)tracePoints[tracePoints.Count - 2]);
        for (int i = 0; i < points.Length; i++)
        {
          Rectangle rect = new Rectangle(points[i].X - penWidth / 2, points[i].Y - penWidth / 2, penWidth + 1, penWidth + 1);

          ColorChange.EraseColor(canvas.Image, rect);
          canvas.Invalidate(rect);
        } // i
     }
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        mouseDown = false;
        tracePoints = null;

        canvas.Invalidate();

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
