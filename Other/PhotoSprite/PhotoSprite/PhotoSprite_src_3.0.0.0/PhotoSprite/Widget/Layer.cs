using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite
{
  public partial class Layer : UserControl
  {
    private Bitmap image = null;
    private string imageFile = "";
    private double zoom = 1.0;

    private Point selfLocation = new Point();
    private Point mouseLocation = new Point();

    /// <summary>
    /// 获取或设置图像
    /// </summary>
    public Bitmap Image
    {
      get
      {
        return image;
      }
      set
      {
        image = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// 获取或设置图像文件
    /// </summary>
    public string ImageFile
    {
      get
      {
        return imageFile;
      }
      set
      {
        imageFile = value;

        if (imageFile != "")
        {
          Graphic myGraphic = new Graphic();
          this.Image = myGraphic.Open(imageFile);
        }
      }
    }

    /// <summary>
    /// 层
    /// </summary>
    public Layer()
    {
      this.ResizeRedraw = true;
      this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

      InitializeComponent();
    }


    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      // 绘制图像
      if (image != null)
      {
        this.Size = new Size((int)(this.image.Width * zoom), (int)(this.image.Height * zoom));
        e.Graphics.DrawImage(image,
          new Rectangle(0, 0, this.Width, this.Height),
          new Rectangle(0, 0, image.Width, image.Height),
          GraphicsUnit.Pixel
          );
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);

      selfLocation.X = this.Location.X;
      selfLocation.Y = this.Location.Y;
      mouseLocation.X = Cursor.Position.X;
      mouseLocation.Y = Cursor.Position.Y;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      if (e.Button == MouseButtons.Left)
      {
        this.Location = new Point(
          Cursor.Position.X - (mouseLocation.X - selfLocation.X),
          Cursor.Position.Y - (mouseLocation.Y - selfLocation.Y));

        //Graphics g = this.canvasMain.CreateGraphics();
        //PaintEventArgs pe = new PaintEventArgs(g, new Rectangle(0, 0, this.canvasMain.Width, this.canvasMain.Height));
        //this.pictureBox_Main_Paint(sender, pe);
        //g.Dispose();
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
    }


  }
}
