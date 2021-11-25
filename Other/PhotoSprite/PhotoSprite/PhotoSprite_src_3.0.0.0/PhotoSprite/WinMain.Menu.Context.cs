using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PhotoSprite.Dialog;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite
{
  public partial class WinMain
  {
    /******************************
     * 
     * 右键缩放菜单
     * 
     ******************************/

    /// <summary>
    /// 根据不同的缩放率对画布进行缩放
    /// </summary>
    /// <param name="iZoom">缩放因子</param>
    private void ZoomCanvas(int iZoom)
    {
      // 清除掉右键菜单中所有勾选项
      this.zoom6ToolStripMenuItem.Checked = false;
      this.zoom12ToolStripMenuItem.Checked = false;
      this.zoom25ToolStripMenuItem.Checked = false;
      this.zoom50ToolStripMenuItem.Checked = false;
      this.zoom66ToolStripMenuItem.Checked = false;
      this.zoom100ToolStripMenuItem.Checked = false;
      this.zoom150ToolStripMenuItem.Checked = false;
      this.zoom200ToolStripMenuItem.Checked = false;
      this.zoom300ToolStripMenuItem.Checked = false;
      this.zoom400ToolStripMenuItem.Checked = false;
      this.zoom800ToolStripMenuItem.Checked = false;
      this.zoom1600ToolStripMenuItem.Checked = false;
      this.zoom3200ToolStripMenuItem.Checked = false;
      this.zoom6400ToolStripMenuItem.Checked = false;

      // 勾选用户指定的缩放比率
      // 并设置画布缩放率
      switch (iZoom)
      {
        case 6:
          this.zoom6ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 0.06;
          this.zoomToolStripDropDownButton.Text = "6%";
          break;

        case 12:
          this.zoom12ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 0.12;
          this.zoomToolStripDropDownButton.Text = "12%";
          break;

        case 25:
          this.zoom25ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 0.25;
          this.zoomToolStripDropDownButton.Text = "25%";
          break;

        case 50:
          this.zoom50ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 0.5;
          this.zoomToolStripDropDownButton.Text = "50%";
          break;

        case 66:
          this.zoom66ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 0.66;
          this.zoomToolStripDropDownButton.Text = "66%";
          break;

        case 100:
          this.zoom100ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 1.0;
          this.zoomToolStripDropDownButton.Text = "100%";
          break;

        case 150:
          this.zoom150ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 1.5;
          this.zoomToolStripDropDownButton.Text = "150%";
          break;

        case 200:
          this.zoom200ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 2.0;
          this.zoomToolStripDropDownButton.Text = "200%";
          break;

        case 300:
          this.zoom300ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 3.0;
          this.zoomToolStripDropDownButton.Text = "300%";
          break;

        case 400:
          this.zoom400ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 4.0;
          this.zoomToolStripDropDownButton.Text = "400%";
          break;

        case 800:
          this.zoom800ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 8.0;
          this.zoomToolStripDropDownButton.Text = "800%";
          break;

        case 1600:
          this.zoom1600ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 16.0;
          this.zoomToolStripDropDownButton.Text = "1600%";
          break;

        case 3200:
          this.zoom3200ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 32.0;
          this.zoomToolStripDropDownButton.Text = "3200%";
          break;

        case 6400:
          this.zoom6400ToolStripMenuItem.Checked = true;
          this.canvasMain.Zoom = 64.0;
          this.zoomToolStripDropDownButton.Text = "6400%";
          break;
      } // switch
    } // end of ZoomCanvas

    private void zoom6ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(6);
    }

    private void zoom12ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(12);
    }

    private void zoom25ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(25);
    }

    private void zoom50ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(50);
    }

    private void zoom66ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(66);
    }

    private void zoom100ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(100);
    }

    private void zoom150ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(150);
    }

    private void zoom200ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(200);
    }

    private void zoom300ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(300);
    }

    private void zoom400ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(400);
    }

    private void zoom800ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(800);
    }

    private void zoom1600ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(1600);
    }

    private void zoom3200ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(3200);
    }

    private void zoom6400ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ZoomCanvas(6400);
    }


    /******************************
     * 
     * 右键层菜单
     * 
     ******************************/

    /// <summary>
    /// 根据指定的对齐模式对图层进行定位
    /// </summary>
    /// <param name="align">对齐模式</param>
    private void AlignLayer(Function.AlignMode align)
    {
      // 清除掉右键菜单中所有勾选项
      this.topLeftToolStripMenuItem.Checked = false;
      this.topToolStripMenuItem.Checked = false;
      this.topRightToolStripMenuItem.Checked = false;
      this.leftToolStripMenuItem.Checked = false;
      this.centerToolStripMenuItem.Checked = false;
      this.rightToolStripMenuItem.Checked = false;
      this.bottomLeftToolStripMenuItem.Checked = false;
      this.bottomToolStripMenuItem.Checked = false;
      this.bottomRightToolStripMenuItem.Checked = false;

      // 勾选用户指定的对齐模式
      switch (align)
      {
        case Function.AlignMode.TopLeft:
          this.topLeftToolStripMenuItem.Checked = true;
          break;

        case Function.AlignMode.MidLeft:
          this.leftToolStripMenuItem.Checked = true;
          break;

        case Function.AlignMode.BottomLeft:
          this.bottomLeftToolStripMenuItem.Checked = true;
          break;

        case Function.AlignMode.TopMid:
          this.topToolStripMenuItem.Checked = true;
          break;

        case Function.AlignMode.Center:
          this.centerToolStripMenuItem.Checked = true;
          break;

        case Function.AlignMode.BottomMid:
          this.bottomToolStripMenuItem.Checked = true;
          break;

        case Function.AlignMode.TopRight:
          this.topRightToolStripMenuItem.Checked = true;
          break;

        case Function.AlignMode.MidRight:
          this.rightToolStripMenuItem.Checked = true;
          break;

        case Function.AlignMode.BottomRight:
          this.bottomRightToolStripMenuItem.Checked = true;
          break;
      } // align

      // 定位
      int W = this.canvasMain.Width;
      int H = this.canvasMain.Height;
      int w = this.layer.Width;
      int h = this.layer.Height;
      Function f = new Function();
      this.layer.Location = f.Locate(W, H, w, h, align);
    } // end of AlignLayer

    private void topLeftToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlignLayer(Function.AlignMode.TopLeft);
    }

    private void topToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlignLayer(Function.AlignMode.TopMid);
    }

    private void topRightoolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlignLayer(Function.AlignMode.TopRight);
    }

    private void leftToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlignLayer(Function.AlignMode.MidLeft);
    }

    private void centerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlignLayer(Function.AlignMode.Center);
    }

    private void rightToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlignLayer(Function.AlignMode.MidRight);
    }

    private void bottomLeftoolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlignLayer(Function.AlignMode.BottomLeft);
    }

    private void bottomToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlignLayer(Function.AlignMode.BottomMid);
    }

    private void bottomRightToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AlignLayer(Function.AlignMode.BottomRight);
    }

    private void hideLayerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.layer.Visible = false;
    }

    private void mergeLayerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(this.canvasMain.Image);
      g.DrawImage(this.layer.Image, this.layer.Location);

      SaveCanvas();
      this.layer.Visible = false;
    }


    /******************************
     * 
     * 右键画布菜单
     * 
     ******************************/

    private void showLayerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.showLayerToolStripMenuItem.Checked ^= true;
      this.layer.Visible = this.showLayerToolStripMenuItem.Checked;
    }


  }
}
