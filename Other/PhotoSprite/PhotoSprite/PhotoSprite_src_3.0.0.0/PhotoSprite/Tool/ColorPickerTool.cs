using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Tool
{
  /// <summary>
  /// 颜色拾取器工具类
  /// </summary>
  public class ColorPickerTool
  {
    private WinMain parentWindow = null;
    private Canvas canvas = null;
    private Bitmap srcImage = null;
    private bool mouseDown = false;
    private Rectangle rect = new Rectangle(0, 0, 1, 1);

    /// <summary>
    /// 使用颜色拾取器工具
    /// </summary>
    /// <param name="parent">主窗口</param>
    public ColorPickerTool(WinMain parent)
    {
      this.parentWindow = parent;
      this.canvas = parent.canvasMain;
      this.srcImage = (Bitmap)parent.canvasMain.Image.Clone();
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

        rect = new Rectangle(0, 0, this.parentWindow.canvasMain.Width, this.parentWindow.canvasMain.Height);
      }
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        Point mouseXY = new Point(e.X, e.Y);

        if (!Function.IsPointInRectangle(mouseXY, rect))
          return;

        Color color = srcImage.GetPixel(mouseXY.X, mouseXY.Y);

        // 左键修改前景颜色，右键修改背景颜色
        if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        {
          this.parentWindow.fgColorToolStripButton.Image = ColorPickerDialog.DrawColorButton(color);
          this.parentWindow.foreColor = color;
        }
        else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
        {
          this.parentWindow.bgColorToolStripButton.Image = ColorPickerDialog.DrawColorButton(color);
          this.parentWindow.backColor = color;
        }
      }
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        mouseDown = false;
      }
    }

  }
}
