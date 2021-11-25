/***************************************************************************
 * 
 * 
 * PhotoSprite 图像精灵
 * 
 * 本软件集平面设计和科研应用于一体
 * 包含水印处理、图像位处理、图像滤镜等众多图像处理功能
 * 
 * 
 * 
 * 作者：联骏 
 * 2006-4-1 收集整理完成
 * 
 * 本程序在如下环境调试通过：
 *    Window 2000 Server SP4, Windows XP SP2
 *    Microsoft Visual Studio 2005 C#
 * 
 * 版权所有 Copy Right 2005 PhotoSprite.com
 * 如要转载，请注明出处
 * 
 * 如有任何问题，请访问我们的技术支持网站： www.PhotoSprite.com
 * QQ: 120314684   E-mail: zjzmzy@163.com
 * 
 ***************************************************************************/

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using PhotoSprite.Tool;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite
{
  public partial class WinMain : Form
  {
    Graphic myGraphic = new Graphic();

    string applicationPath = Application.StartupPath;
    string startFileName = "";
    string openFolder = "";
    string saveFolder = "";
    string tmpFolder = "";
    int undoTimes = 100;

    HistoryImage history;

    ToolType toolType = ToolType.Move;
    RectangleSelectTool rectangleSelectTool;
    EllipseSelectTool ellipseSelectTool;
    LassoSelectTool lassoSelectTool;
    PencilTool pencilTool;
    BrushTool brushTool;
    EraserTool eraserTool;
    LineTool lineTool;
    RectangleTool rectangleTool;
    TextTool textTool;
    ColorPickerTool colorPickerTool;
    PaintBucketTool paintBucketTool;

    int brushWidth = 2;
    HatchStyle hatchStyle;
    bool hasBrushStyle = false;

    public Color foreColor = Color.Red;
    public Color backColor = Color.Transparent;


    /******************************
     * 
     * 主程序
     * 
     ******************************/

    public WinMain(string fileName)
    {
      InitializeComponent();

      this.startFileName = fileName;
    }

    private void WinMain_Load(object sender, EventArgs e)
    {
      // 初始化用户参数
      try
      {
        System.IO.StreamReader sr = new StreamReader(
          Application.StartupPath + @"\ps.cfg", System.Text.Encoding.Default);
        openFolder = sr.ReadLine().Replace("OpenFolder: ", "");
        saveFolder = sr.ReadLine().Replace("SaveFolder: ", "");
        tmpFolder = sr.ReadLine().Replace("TmpFolder: ", "");
        undoTimes = Convert.ToInt32(sr.ReadLine().Replace("UndoTimes: ", ""));
        sr.Close();

        history = new HistoryImage(tmpFolder, undoTimes);
      }
      catch
      {
        // 如果获取用户参数失败，则初始化一份默认配置
        openFolder = applicationPath + @"\";
        saveFolder = applicationPath + @"\";
        tmpFolder = applicationPath + @"\tmp\";
        history = new HistoryImage(tmpFolder, 100);

        System.IO.StreamWriter sw = new StreamWriter(
          Application.StartupPath + @"\ps.cfg", false, System.Text.Encoding.Default);
        sw.WriteLine("OpenFolder: " + openFolder);
        sw.WriteLine("SaveFolder: " + saveFolder);
        sw.WriteLine("TmpFolder: " + tmpFolder);
        sw.WriteLine("UndoTimes: 100");
        sw.Close();
      }


      // 指定历史记录事件
      history.HistoryChanged += new EventHandler(this.HistoryStatus);


      // 建立一个临时目录，存放一些临时处理的图像
      if (!System.IO.Directory.Exists(tmpFolder))
      {
        System.IO.Directory.CreateDirectory(tmpFolder);
      }

      // 初始化画布
      if (startFileName != "")
      {
        // 打开关联图像文件
        OpenFile(startFileName);
      }
      else
      {
        // 欢迎画面
      }

      // 初始化历史状态
      RefreshHistory();

      // 初始化工具条
      InitStyleToolbar();
    }

    private void WinMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      bool cancel = false;

      if (history.IsDirty)
      {
        switch (MessageBox.Show("当前编辑的图像已修改，您需要保存图像吗？",
          "友情提醒", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
        {
          case DialogResult.Yes:
            saveAsToolStripMenuItem_Click(sender, e);
            break;

          case DialogResult.No:
            break;

          case DialogResult.Cancel:
            cancel = true;
            break;
        } // switch
      }

      if (cancel)
      {
        e.Cancel = cancel;
      }
      else
      {
        // 删除临时目录
        if (System.IO.Directory.Exists(tmpFolder))
        {
          System.IO.Directory.Delete(tmpFolder, true);
        }
      }
    }

    private void WinMain_Resize(object sender, EventArgs e)
    {
      canvasMain_Resize(sender, e);
    }

    private void WinMain_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.Copy;
      string[] drag = (string[])e.Data.GetData(DataFormats.FileDrop, true);
      string fileName = drag[0];
      OpenFile(fileName);

      RefreshHistory();
    }


    /******************************
     * 
     * 主绘图区
     * 
     ******************************/

    private void mainToolStripContainer_ContentPanel_DoubleClick(object sender, EventArgs e)
    {
      if (!this.canvasMain.Visible)
        openToolStripMenuItem_Click(sender, e);
    }


    /******************************
     * 
     * 画布
     * 
     ******************************/

    /// <summary>
    /// 对图像的指定选区进行处理后并保存
    /// </summary>
    /// <param name="bgImage">背景</param>
    /// <param name="fgImage">前景</param>
    private void SaveCanvas(Bitmap bgImage, Bitmap fgImage)
    {
      RegionClip rc = new RegionClip(this.canvasMain.SelectedRegion);
      this.canvasMain.Image = rc.Replace(bgImage, fgImage);

      SaveCanvas();
    }

    /// <summary>
    /// 将画布内容保存到图像文件中
    /// </summary>
    private void SaveCanvas()
    { 
      myGraphic.DestFile = history.NextImage;
      myGraphic.Save(this.canvasMain.Image);

      history.Current++;
    }

    private void SaveCanvas(object sender, System.EventArgs e)
    {
      SaveCanvas();
    }

    private void canvasMain_Resize(object sender, EventArgs e)
    {
      if (!this.canvasMain.Visible)
        return;

      // 获取画布宽高
      int width = this.canvasMain.Width;
      int height = this.canvasMain.Height;

      // 获取容器宽高
      int W = this.mainToolStripContainer.ContentPanel.Width;
      int H = this.mainToolStripContainer.ContentPanel.Height;

      // 判断画布宽度是否超出容器宽度
      if (width > W)
      {
        this.hScrollBar.Visible = true;

        this.hScrollBar.Minimum = 0;
        this.hScrollBar.Maximum = width;
        this.hScrollBar.Value = (this.hScrollBar.Maximum + this.hScrollBar.Minimum) / 2;
      }
      else
      {
        this.hScrollBar.Visible = false;
      }

      // 判断画布高度是否超出容器高度
      if (height > H)
      {
        this.vScrollBar.Visible = true;

        this.vScrollBar.Minimum = 0;
        this.vScrollBar.Maximum = height;
        this.vScrollBar.Value = (this.vScrollBar.Maximum + this.vScrollBar.Minimum) / 2;
      }
      else
      {
        this.vScrollBar.Visible = false;
      }

      // 在容器中定位画布
      this.canvasMain.Left = (W - width) / 2;
      this.canvasMain.Top = (H - height) / 2;

      // 在状态栏显示图像尺寸
      this.sizeToolStripStatusLabel.Text = this.canvasMain.Image.Width.ToString() + "×" + this.canvasMain.Image.Height.ToString();
    }

    private void canvasMain_MouseMove(object sender, MouseEventArgs e)
    {
      this.mouseToolStripStatusLabel.Text = "X:" + e.X.ToString() + ", Y:" + e.Y.ToString();
    }

    private void canvasMain_MouseDown(object sender, MouseEventArgs e)
    {
      UpdateToolbox(this.toolType);
    }


    /******************************
     * 
     * 层
     * 
     ******************************/

    private void layer_VisibleChanged(object sender, EventArgs e)
    {
      this.showLayerToolStripMenuItem.Enabled = true;

      this.showLayerToolStripMenuItem.Checked = this.layer.Visible;
    }


    /******************************
     * 
     * 滚动条
     * 
     ******************************/

    /// <summary>
    /// 垂直滚动
    /// </summary>
    private void VertScroll()
    {
      int H = this.mainToolStripContainer.ContentPanel.Height;
      int h = this.canvasMain.Height;
      int y = this.vScrollBar.Value;
      this.canvasMain.Top = (H * 4 / 5 - h) * y / h + H / 10;
    } // end of VertScroll

    /// <summary>
    /// 当鼠标滚动时，查看图像其它部分
    /// </summary>
    private void Canvas_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      // 当鼠标滑轮每滚动一次时，滚动条滚动的距离
      int minScrollSize = (this.vScrollBar.Maximum - this.vScrollBar.Minimum) / 10;

      if (this.vScrollBar.Visible)
      {
        if (e.Delta < 0)
        {
          // 向下滚动
          if (this.vScrollBar.Value + minScrollSize > this.vScrollBar.Maximum)
          {
            this.vScrollBar.Value = this.vScrollBar.Maximum;
          }
          else
          {
            this.vScrollBar.Value += minScrollSize;
          }
        }
        else
        {
          // 向上滚动
          if (this.vScrollBar.Value - minScrollSize < this.vScrollBar.Minimum)
          {
            this.vScrollBar.Value = this.vScrollBar.Minimum;
          }
          else
          {
            this.vScrollBar.Value -= minScrollSize;
          }
        }

        VertScroll();
      }
    }

    /// <summary>
    /// 水平滚动
    /// </summary>
    private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
    {
      int W = this.mainToolStripContainer.ContentPanel.Width;
      int w = this.canvasMain.Width;
      int x = this.hScrollBar.Value;
      this.canvasMain.Left = (W * 4 / 5 - w) * x / w + W / 10;
    }

    /// <summary>
    /// 垂直滚动
    /// </summary>
    private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
    {
      VertScroll();
    }


  }
}