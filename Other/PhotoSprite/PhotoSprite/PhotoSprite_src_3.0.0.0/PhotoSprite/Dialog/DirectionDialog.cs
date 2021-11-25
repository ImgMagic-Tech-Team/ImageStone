using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class DirectionDialog : Form
  {
    private Canvas canvas = null;
    Bitmap srcImage = null;

    public DirectionDialog(Canvas parent)
    {
      InitializeComponent();

      this.canvas = parent;
      this.srcImage = this.canvas.Image;
    }

    private void DirectionDialog_Load(object sender, EventArgs e)
    {
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.canvas.Image = (Bitmap)srcImage.Clone();
    }

    private void angleChooser_MouseMove(object sender, MouseEventArgs e)
    {
      string[] ADirection = { "E", "NE", "N", "NW", "W", "SW", "S", "SE" };
      this.angleTextBox.Text = ADirection[((angleChooser.Angle + 22) / 45) % 8];
      UpdateCanvas();
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Effect e = new Effect();

      dstImage = e.Emboss((Bitmap)srcImage.Clone(), this.Direction);

      RegionClip rc = new RegionClip(this.canvas.SelectedRegion);
      dstImage = rc.Replace((Bitmap)srcImage.Clone(), dstImage);

      this.canvas.Image = dstImage;
    }

    /// <summary>
    /// 获取用户指定的方向
    /// </summary>
    public Effect.Direction Direction
    {
      get
      {
        Effect.Direction direction = Effect.Direction.E;
        switch ((angleChooser.Angle - 22) / 45)
        {
          case 0:
            direction = Effect.Direction.E;
            break;

          case 1:
            direction = Effect.Direction.NE;
            break;

          case 2:
            direction = Effect.Direction.N;
            break;

          case 3:
            direction = Effect.Direction.NW;
            break;

          case 4:
            direction = Effect.Direction.W;
            break;

          case 5:
            direction = Effect.Direction.SW;
            break;

          case 6:
            direction = Effect.Direction.S;
            break;

          case 7:
            direction = Effect.Direction.SE;
            break;
        }

        return direction;
      }
    }

  }
}