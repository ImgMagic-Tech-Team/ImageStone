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
  /// ����Ͱ������
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
    /// ʹ������Ͱ����
    /// </summary>
    /// <param name="parent">����</param>
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
      // ��������ͼ��
      this.image = this.canvas.Image;

      Point mouseXY = new Point(e.X, e.Y);
      width = this.canvas.Width;
      height = this.canvas.Height;

      Rectangle rect = new Rectangle(0, 0, this.canvas.Width, this.canvas.Height);
      if (Function.IsPointInRectangle(mouseXY, rect))
      {
        bool[,] pixelsChecked = new bool[width, height];

        // ��ȡ��ǰ����
        currentColor = this.image.GetPixel(mouseXY.X, mouseXY.Y);

        // �������Ҽ�ָ��ǰ������ɫ�������
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

        // �����ı������ڽ�������߽�
        minX = mouseXY.X;
        maxX = mouseXY.X;
        minY = mouseXY.Y;
        maxY = mouseXY.Y;

        // ɨ����������߽�
        FillScanLines(new FillScanLinesInfo(mouseXY.X, mouseXY.Y, pixelsChecked, false));

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            pixelsChecked[x, y] = false;
          } // x
        } // y

        // �������
        FillScanLines(new FillScanLinesInfo(mouseXY.X, mouseXY.Y, pixelsChecked, true));

        // �ͷ���Դ
        pixelsChecked = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
      }
    }

    public void OnMouseUp(object sender, MouseEventArgs e)
    {
      this.canvas.Invalidate();

      // ������ͼ�����¼�
      OnDrawingFinished();
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


    #region ɨ���������㷨
    /// <summary>
    /// ��䣨��ɨ�裩��ǰ���ص��������
    /// </summary>
    /// <param name="x">��ǰ���� X ����</param>
    /// <param name="y">��ǰ���� Y ����</param>
    /// <param name="pixelsChecked">ͼ������ɨ������</param>
    /// <param name="isDraw">�Ƿ���Ҫ�޸�Ŀ������</param>
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
    /// ��䣨��ɨ�裩��ǰ���ص��ұ�����
    /// </summary>
    /// <param name="x">��ǰ���� X ����</param>
    /// <param name="y">��ǰ���� Y ����</param>
    /// <param name="pixelsChecked">ͼ������ɨ������</param>
    /// <param name="isDraw">�Ƿ���Ҫ�޸�Ŀ������</param>
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
    /// ���ɨ������Ϣ��
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

      // ���������������±߽�
      if (info.Y > maxY)
      {
        maxY = info.Y;
      }
      else if (info.Y < minY)
      {
        minY = info.Y;
      }

      // ��ֱɨ��
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
