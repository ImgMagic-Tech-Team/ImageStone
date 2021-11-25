using System;
using System.Drawing;
using System.Windows.Forms;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class MappingDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    private bool mouseDown = false;
    private byte[] map = new byte[256];
    private Point lastPoint = Point.Empty;

    public MappingDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Adjustment a = new Adjustment();

      dstImage = a.Mapping((Bitmap)srcImage.Clone(), this.Map, this.ChannelMode);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    private void Clear()
    {
      for (int i = 0; i < 256; i++)
      {
        map[i] = (byte)i;
      }

      this.drawPictureBox.Invalidate();
    }

    private void MappingDialog_Load(object sender, EventArgs e)
    {
      Clear();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void drawPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      Clear();
    }

    private void drawPictureBox_Paint(object sender, PaintEventArgs e)
    {
      UpdateCanvas();

      Graphics g = e.Graphics;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

      Point[] points = new Point[256];
      for (int i = 0; i < 256; i++)
      {
        points[i].X = i;
        points[i].Y = 255 - map[i];
      }

      Color color = Color.Gray;
      switch (this.channelModeComboBox.SelectedIndex)
      {
        case 0:
          color = Color.Red;
          break;

        case 1:
          color = Color.Green;
          break;

        case 2:
          color = Color.Blue;
          break;

        default: // RGB
          break;
      } // switch

      g.DrawLines(new Pen(new SolidBrush(color), 1), points);
    }

    private void drawPictureBox_MouseDown(object sender, MouseEventArgs e)
    {
      mouseDown = true;
      if (((e.Button & MouseButtons.Left) == MouseButtons.Left))
      {
        map[e.X] = (byte)(255 - e.Y);
      }
      else if (((e.Button & MouseButtons.Right) == MouseButtons.Right))
      {
        map[e.X] = (byte)e.X;
      }

      this.drawPictureBox.Invalidate();
    }

    private void drawPictureBox_MouseMove(object sender, MouseEventArgs e)
    {
      this.xLabel.Text = e.X.ToString();
      this.yLabel.Text = Convert.ToString(255 - e.Y);

      Point mouseXY = new Point(e.X, e.Y);
      Rectangle rect = new Rectangle(0, 0, 256, 256);

      if (!Function.IsPointInRectangle(mouseXY, rect))
        return;

      if (mouseDown)
      {
        if (((e.Button & MouseButtons.Left) == MouseButtons.Left))
        {
          Point[] points = Function.GetLinePoints(lastPoint, mouseXY);

          for (int i = 0; i < points.Length; i++)
          {
            map[points[i].X] = (byte)(255 - points[i].Y);
          }
        }
        else if (((e.Button & MouseButtons.Right) == MouseButtons.Right))
        {
          int left = lastPoint.X;
          int right = mouseXY.X;
          if (left > right)
          {
            int t = left;
            left = right;
            right = t;
          }

          for (int i = left; i <= right; i++)
          {
            map[i] = (byte)i;
          }
        }

      }

      lastPoint = mouseXY;
      this.drawPictureBox.Invalidate();
    }

    private void drawPictureBox_MouseUp(object sender, MouseEventArgs e)
    {
      if (mouseDown)
        mouseDown = false;

      this.drawPictureBox.Invalidate();
    }

    private void channelModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      Clear();
    }


    /// <summary>
    /// 获取色彩亮度映射表
    /// </summary>
    public byte[] Map
    {
      get
      {
        return map;
      }
    }


    /// <summary>
    /// 获取通道模式
    /// </summary>
    public Adjustment.ChannelMode ChannelMode
    {
      get
      {
        Adjustment.ChannelMode channelMode = Adjustment.ChannelMode.White;

        switch (this.channelModeComboBox.SelectedIndex)
        {
          case 0:
            channelMode = Adjustment.ChannelMode.Red;
            break;

          case 1:
            channelMode = Adjustment.ChannelMode.Green;
            break;

          case 2:
            channelMode = Adjustment.ChannelMode.Blue;
            break;

          default: // White
            break;
        } // switch

        return channelMode;
      }
    }


  }
}