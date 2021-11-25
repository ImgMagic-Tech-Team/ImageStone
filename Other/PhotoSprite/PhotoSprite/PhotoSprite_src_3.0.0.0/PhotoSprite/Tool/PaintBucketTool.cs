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
  /// 油漆桶工具类
  /// </summary>
  public class PaintBucketTool : Tool
  {
    private Canvas canvas = null;
    private Bitmap image = null;
    private Color currentColor;
    private Color colorToDraw;
    private int minX, maxX, minY, maxY;
    private int width, height;

    /// <summary>
    /// 使用油漆桶工具
    /// </summary>
    /// <param name="parent">画布</param>
    public PaintBucketTool(Canvas parent)
    {
      this.canvas = parent;
    }

    public void OnActivate()
    {
      this.canvas.MouseDown += this.OnMouseDown;
      this.canvas.MouseUp += this.OnMouseUp;
    }

    public void OnDeactivate()
    {
      this.canvas.MouseDown -= this.OnMouseDown;
      this.canvas.MouseUp -= this.OnMouseUp;
    }

    public void OnMouseDown(object sender, MouseEventArgs e)
    {
      // 锁定画布图像
      this.image = this.canvas.Image;

      Point mouseXY = new Point(e.X, e.Y);
      width = this.canvas.Width;
      height = this.canvas.Height;

      Rectangle rect = new Rectangle(0, 0, this.canvas.Width, this.canvas.Height);
      if (Function.IsPointInRectangle(mouseXY, rect))
      {
        bool[,] pixelsChecked = new bool[width, height];

        // 获取当前像素
        currentColor = this.image.GetPixel(mouseXY.X, mouseXY.Y);

        // 根据左右键指定前背景颜色进行填充
        switch (e.Button)
        {
          case MouseButtons.Left:
            colorToDraw = this.ForeColor;
            break;

          case MouseButtons.Right:
            colorToDraw = this.BackColor;
            break;

          default:
            return;
        }

        // 以下四变量用于建立区域边界
        minX = mouseXY.X;
        maxX = mouseXY.X;
        minY = mouseXY.Y;
        maxY = mouseXY.Y;

        // 扫描出最大区域边界
        FillScanLines(new FillScanLinesInfo(mouseXY.X, mouseXY.Y, pixelsChecked, false));

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            pixelsChecked[x, y] = false;
          } // x
        } // y

        // 填充区域
        FillScanLines(new FillScanLinesInfo(mouseXY.X, mouseXY.Y, pixelsChecked, true));

        // 释放资源
        pixelsChecked = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
      }
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
      this.canvas.Invalidate();

      // 触发绘图结束事件
      OnDrawingFinished();
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


    #region 扫描填充核心算法
    /// <summary>
    /// 填充（或扫描）当前像素的左边区域
    /// </summary>
    /// <param name="x">当前像素 X 坐标</param>
    /// <param name="y">当前像素 Y 坐标</param>
    /// <param name="pixelsChecked">图像像素扫描结果集</param>
    /// <param name="isDraw">是否需要修改目标像素</param>
    /// <returns></returns>
    private int FillScanLeft(int x, int y, bool[,] pixelsChecked, bool isDraw)
    {
      int lowX = x;

      while (lowX >= 0 && !pixelsChecked[lowX, y] &&
        Function.CheckColor(image.GetPixel(lowX, y), currentColor, tolerance))
      {
        if (isDraw)
        {
          image.SetPixel(lowX, y, colorToDraw);
        }

        pixelsChecked[lowX, y] = true;
        lowX--;
      }

      if (lowX < minX)
      {
        minX = lowX;
      }

      return lowX + 1;
    }

    /// <summary>
    /// 填充（或扫描）当前像素的右边区域
    /// </summary>
    /// <param name="x">当前像素 X 坐标</param>
    /// <param name="y">当前像素 Y 坐标</param>
    /// <param name="pixelsChecked">图像像素扫描结果集</param>
    /// <param name="isDraw">是否需要修改目标像素</param>
    /// <returns></returns>
    private int FillScanRight(int x, int y, bool[,] pixelsChecked, bool isDraw)
    {
      int highX = x;

      while ((highX < width) && !pixelsChecked[highX, y] &&
        Function.CheckColor(image.GetPixel(highX, y), currentColor, tolerance)       )
      {
        if (isDraw)
        {
          image.SetPixel(highX, y, colorToDraw);
        }

        pixelsChecked[highX, y] = true;
        highX++;
      }

      if (highX >= maxX)
      {
        maxX = highX;
      }

      return highX - 1;
    }

    /// <summary>
    /// 填充扫描行信息类
    /// </summary>
    private class FillScanLinesInfo
    {
      public int X;
      public int Y;
      public bool[,] PixelsChecked;
      public bool IsDraw;

      public FillScanLinesInfo(int x, int y, bool[,] pixelsChecked, bool isDraw)
      {
        this.X = x;
        this.Y = y;
        this.PixelsChecked = pixelsChecked;
        this.IsDraw = isDraw;
      }
    }

    private void FillScanLines(FillScanLinesInfo info, Queue infoQueue, int maxRecursionDepth)
    {
      if (maxRecursionDepth <= 0)
      {
        infoQueue.Enqueue(info);
        return;
      }

      int lowX = FillScanLeft(info.X, info.Y, info.PixelsChecked, info.IsDraw);
      int highX = FillScanRight(info.X + 1, info.Y, info.PixelsChecked, info.IsDraw);
      int i;

      // 建立填充区域的上下边界
      if (info.Y > maxY)
      {
        maxY = info.Y;
      }
      else if (info.Y < minY)
      {
        minY = info.Y;
      }

      // 垂直扫描
      for (i = lowX; i <= highX; i++)
      {
        if (info.Y > 0 && !info.PixelsChecked[i, info.Y - 1] &&
          Function.CheckColor(image.GetPixel(i, info.Y - 1), currentColor, tolerance))
        {
          FillScanLines(new FillScanLinesInfo(i, info.Y - 1, info.PixelsChecked, info.IsDraw), infoQueue, maxRecursionDepth - 1);
        }

        if (info.Y < (height - 1) && !info.PixelsChecked[i, info.Y + 1] &&
          Function.CheckColor(image.GetPixel(i, info.Y + 1), currentColor, tolerance))
        {
          FillScanLines(new FillScanLinesInfo(i, info.Y + 1, info.PixelsChecked, info.IsDraw), infoQueue, maxRecursionDepth - 1);
        }
      }
    }

    private void FillScanLines(FillScanLinesInfo info)
    {
      Queue infoQueue = new Queue();

      FillScanLines(info, infoQueue, 4);

      while (infoQueue.Count > 0)
      {
        FillScanLinesInfo fsli = (FillScanLinesInfo)infoQueue.Dequeue();
        FillScanLines(fsli, infoQueue, 4);
      }
    }
    #endregion


  }
}
