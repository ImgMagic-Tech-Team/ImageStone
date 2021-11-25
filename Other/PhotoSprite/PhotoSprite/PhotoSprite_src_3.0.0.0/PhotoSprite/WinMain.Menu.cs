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
     * 文件菜单
     * 
     ******************************/

    private void newToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NewDialog dlg = new NewDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.canvasMain.Visible = true;

        // 获取用户指定宽高值
        int newWidth = dlg.NewWidth;
        int newHeight = dlg.NewHeight;

        // 初始化一张空白图像
        Bitmap newImage = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        Graphics g = System.Drawing.Graphics.FromImage(newImage);
        g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, newWidth, newHeight));

        // 保存到文件夹
        this.canvasMain.Image = newImage;
        SaveCanvas();
      }
    }

    /// <summary>
    /// 打开指定的文件
    /// </summary>
    /// <param name="fileName">图像文件名</param>
    private void OpenFile(string fileName)
    {
      if (!this.canvasMain.Visible)
        this.canvasMain.Visible = true;

      string fileNameExt = fileName.Substring(fileName.LastIndexOf(".")).ToLower();
      if (fileNameExt == ".jpg" || fileNameExt == ".gif" ||
        fileNameExt == ".bmp" || fileNameExt == ".png" ||
        fileNameExt == ".tif" || fileNameExt == ".psf")
      {
        if (history.Count > 0)
        {
          switch (MessageBox.Show("画布里已经有一幅图像，您要替换掉画布里的图像吗？\n如果选择“是”将替换掉现有图像！\n选择“否”将在新建图层里打开！",
            "友情提醒", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
          {
            case DialogResult.Yes:
              myGraphic.SourceFile = fileName;
              myGraphic.DestFile = history.NextImage;
              myGraphic.ConvertFormat();

              history.Current++;
              this.pathToolStripStatusLabel.Text = fileName;
              break;

            case DialogResult.No:
              myGraphic.SourceFile = fileName;
              myGraphic.DestFile = history.InitDirectory + "layer.psf";
              myGraphic.ConvertFormat();

              this.layer.Visible = true;
              this.layer.ImageFile = myGraphic.DestFile;
              break;

            case DialogResult.Cancel:
              break;
          } // switch
        }
        else
        {
          myGraphic.SourceFile = fileName;
          myGraphic.DestFile = history.NextImage;
          myGraphic.ConvertFormat();

          history.Current++;
          history.Save();

          this.pathToolStripStatusLabel.Text = fileName;
        }
      }
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // 初始化打开目录
      openFileDialog.InitialDirectory = openFolder;

      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        string srcFileName = openFileDialog.FileName;
        OpenFile(srcFileName);

        RefreshHistory();
      }
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string saveFileName = this.pathToolStripStatusLabel.Text;
      if (saveFileName == "")
      {
        saveAsToolStripMenuItem_Click(sender, e);
        return;
      }

      myGraphic.SourceFile = history.CurrentImage;
      myGraphic.DestFile = saveFileName;
      myGraphic.ConvertFormat();

      history.Save();
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // 初始化打开目录为软件运行目录
      saveFileDialog.InitialDirectory = saveFolder;
      if (saveFileDialog.ShowDialog() == DialogResult.OK)
      {
        string saveFileName = saveFileDialog.FileName;

        myGraphic.SourceFile = history.CurrentImage;
        myGraphic.DestFile = saveFileName;
        myGraphic.ConvertFormat();

        history.Save();

        this.pathToolStripStatusLabel.Text = saveFileName;
      }
    }

    private void ieToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myGraphic.SourceFile = history.CurrentImage;
      myGraphic.DestFile = history.InitDirectory + "ie.bmp";
      myGraphic.ConvertFormat();

      try
      {
        System.Diagnostics.Process.Start("IEXPLORE.EXE", myGraphic.DestFile);
      }
      catch
      {
        MessageBox.Show("打开 IE 失败！", "友情提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void fireworksToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myGraphic.SourceFile = history.CurrentImage;
      myGraphic.DestFile = history.InitDirectory + "fireworks.bmp";
      myGraphic.ConvertFormat();

      try
      {
        System.Diagnostics.Process.Start("Fireworks.EXE", myGraphic.DestFile);
      }
      catch
      {
        MessageBox.Show("打开 Fireworks 软件失败！", "友情提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void photoshopToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myGraphic.SourceFile = history.CurrentImage;
      myGraphic.DestFile = history.InitDirectory + "photoshop.bmp";
      myGraphic.ConvertFormat();

      try
      {
        System.Diagnostics.Process.Start("Photoshop.EXE", myGraphic.DestFile);
      }
      catch
      {
        MessageBox.Show("打开 Photoshop 软件失败！", "友情提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      e.Graphics.DrawImage(this.canvasMain.Image, 0, 0);
    }

    private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      printPreviewDialog.ShowDialog();
    }

    private void printToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (printDialog.ShowDialog() == DialogResult.OK)
      {
        this.printDocument.Print();
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }


    /******************************
     * 
     * 编辑菜单
     * 
     ******************************/

    /// <summary>
    /// 刷新历史记录状态
    /// </summary>
    private void RefreshHistory()
    {
      if (history.CanUndo)
      {
        this.undoToolStripMenuItem.Enabled = true;
        this.undoToolStripButton.Enabled = true;
      }
      else
      {
        this.undoToolStripMenuItem.Enabled = false;
        this.undoToolStripButton.Enabled = false;
      }

      if (history.CanRedo)
      {
        this.redoToolStripMenuItem.Enabled = true;
        this.redoToolStripButton.Enabled = true;
      }
      else
      {
        this.redoToolStripMenuItem.Enabled = false;
        this.redoToolStripButton.Enabled = false;
      }

      if (this.canvasMain.SelectedPath.PointCount > 0)
        this.canvasMain.SelectedPath.Reset();
      this.canvasMain.ImageFile = history.CurrentImage;
    } // end of RefreshHistory

    private void HistoryStatus(object sender, System.EventArgs e)
    {
      RefreshHistory();
    }

    private void undoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      history.Current--;
      RefreshHistory();
    }

    private void redoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      history.Current++;
      RefreshHistory();
    }

    private void cutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      RegionClip gc = new RegionClip(this.canvasMain.SelectedRegion);

      Bitmap srcImage = (Bitmap)this.canvasMain.Image.Clone();
      Clipboard.SetDataObject(gc.Hold(srcImage), true);

      this.canvasMain.Image = gc.Remove((Bitmap)this.canvasMain.Image.Clone());
      SaveCanvas();
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      RegionClip gc = new RegionClip(this.canvasMain.SelectedRegion);
      Bitmap srcImage = (Bitmap)this.canvasMain.Image.Clone();

      Clipboard.SetDataObject(gc.Hold(srcImage), true);
    }

    private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap))
      {
        // 保存到文件夹
        myGraphic.DestFile = history.InitDirectory + "layer.psf";
        myGraphic.Save((Bitmap)(Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap));

        this.layer.Visible = true;
        this.layer.ImageFile = myGraphic.DestFile;
      }
    }

    private void optionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OptionDialog dlg = new OptionDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        openFolder = dlg.OpenFolder;
        saveFolder = dlg.SaveFolder;
        tmpFolder = dlg.TmpFolder;
        undoTimes = dlg.UndoTimes;

        //history = new HistoryImage(tmpFolder, undoTimes);
      }
    }


    /******************************
     * 
     * 图像.调整菜单
     * 
     ******************************/

    private void colorBalanceToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ColorBalanceDialog dlg = new ColorBalanceDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "亮度";
      dlg.Description = "亮度调节";
      dlg.Minimum = -255;
      dlg.Maximum = 255;
      dlg.Degree = 0;
      dlg.Support = DegreeDialog.SupportMethod.Brightness;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "对比度";
      dlg.Description = "对比度调节";
      dlg.Minimum = -100;
      dlg.Maximum = 100;
      dlg.Degree = 0;
      dlg.Support = DegreeDialog.SupportMethod.Contrast;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void hslToolStripMenuItem_Click(object sender, EventArgs e)
    {
      HslDialog dlg = new HslDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void gammaCorrectToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GammaCorrectDialog dlg = new GammaCorrectDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void thresholdToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "阈值";
      dlg.Description = "阈值调节";
      dlg.Support = DegreeDialog.SupportMethod.Thresholding;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void grayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GrayDialog dlg = new GrayDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void invertToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.Invert((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void pseudoColorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PseudoColorDialog dlg = new PseudoColorDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void rotateChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.RotateChannel((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void redExtractChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.ExtractChannel((Bitmap)srcImage.Clone(), Adjustment.ChannelMode.Red);

      SaveCanvas(srcImage, dstImage);
    }

    private void greenExtractChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.ExtractChannel((Bitmap)srcImage.Clone(), Adjustment.ChannelMode.Green);

      SaveCanvas(srcImage, dstImage);
    }

    private void blueExtractChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.ExtractChannel((Bitmap)srcImage.Clone(), Adjustment.ChannelMode.Blue);

      SaveCanvas(srcImage, dstImage);
    }

    private void redFilterChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.FilterChannel((Bitmap)srcImage.Clone(), Adjustment.ChannelMode.Red);

      SaveCanvas(srcImage, dstImage);
    }

    private void greenFilterChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.FilterChannel((Bitmap)srcImage.Clone(), Adjustment.ChannelMode.Green);

      SaveCanvas(srcImage, dstImage);
    }

    private void blueFilterChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.FilterChannel((Bitmap)srcImage.Clone(), Adjustment.ChannelMode.Blue);

      SaveCanvas(srcImage, dstImage);
    }

    private void cyanFilterChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.FilterChannel((Bitmap)srcImage.Clone(), Adjustment.ChannelMode.Cyan);

      SaveCanvas(srcImage, dstImage);
    }

    private void megentaFilterChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.FilterChannel((Bitmap)srcImage.Clone(), Adjustment.ChannelMode.Megenta);

      SaveCanvas(srcImage, dstImage);
    }

    private void yellowFilterChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Adjustment a = new Adjustment();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = a.FilterChannel((Bitmap)srcImage.Clone(), Adjustment.ChannelMode.Yellow);

      SaveCanvas(srcImage, dstImage);
    }

    private void mappingToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MappingDialog dlg = new MappingDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void equalizerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Bitmap srcImage = this.canvasMain.Image;
      Histogram h = new Histogram((Bitmap)srcImage.Clone());
      Bitmap dstImage = h.Equalizer();

      SaveCanvas(srcImage, dstImage);
    }


    /******************************
     * 
     * 图像菜单
     * 
     ******************************/

    private void translationToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TranslationDialog dlg = new TranslationDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ResizeDialog dlg = new ResizeDialog((Bitmap)this.canvasMain.Image);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        ImageTransform it = new ImageTransform();
        this.canvasMain.Image = it.Resize(this.canvasMain.Image, dlg.NewWidth, dlg.NewHeight);

        SaveCanvas();
      }
    }

    private void cropToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ImageTransform it = new ImageTransform();
      this.canvasMain.Image = it.Crop(this.canvasMain.Image, this.canvasMain.SelectedRegion);

      SaveCanvas();
    }

    private void rotateCW90ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ImageTransform it = new ImageTransform();
      this.canvasMain.Image = it.Rotate(this.canvasMain.Image, -90);

      SaveCanvas();
    }

    private void rotateCCW90ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ImageTransform it = new ImageTransform();
      this.canvasMain.Image = it.Rotate(this.canvasMain.Image, 90);

      SaveCanvas();
    }

    private void rotateArbitraryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AngleDialog dlg = new AngleDialog(this.canvasMain);
      dlg.Text = "旋转";
      dlg.Support = AngleDialog.SupportMethod.Rotate;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void flipHorzToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ImageTransform it = new ImageTransform();
      this.canvasMain.Image = it.Flip(this.canvasMain.Image, true);

      SaveCanvas();
    }

    private void flipVertToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ImageTransform it = new ImageTransform();
      this.canvasMain.Image = it.Flip(this.canvasMain.Image, false);

      SaveCanvas();
    }

    private void transposeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ImageTransform it = new ImageTransform();
      this.canvasMain.Image = it.Transpose(this.canvasMain.Image);

      SaveCanvas();
    }

    private void slantToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SlantDialog dlg = new SlantDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void trimToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TrimDialog dlg = new TrimDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
    {
      HistogramDialog dlg = new HistogramDialog(this.canvasMain.Image);
      dlg.ShowDialog();
    }


    /******************************
     * 
     * 特效滤镜菜单
     * 
     ******************************/

    private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Smooth((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void gaussBlurToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.GaussBlur((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MotionBlurDialog dlg = new MotionBlurDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void radialBlurToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AngleDialog dlg = new AngleDialog(this.canvasMain);
      dlg.Text = "径向模糊";
      dlg.Support = AngleDialog.SupportMethod.RadialBlur;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Sharpen((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void sharpenMoreToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.SharpenMore((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void sharpenFreeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "锐化";
      dlg.Description = "锐化度调节";
      dlg.Minimum = 1;
      dlg.Maximum = 100;
      dlg.Degree = 10;
      dlg.Support = DegreeDialog.SupportMethod.Sharpen;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void unsharpMaskToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "钝化蒙板";
      dlg.Description = "钝化度调节";
      dlg.Minimum = 1;
      dlg.Maximum = 100;
      dlg.Degree = 10;
      dlg.Support = DegreeDialog.SupportMethod.UnsharpMask;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void laplacianEmbossToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Emboss((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void directionEmbossToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DirectionDialog dlg = new DirectionDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void grayEmbossToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AngleDialog dlg = new AngleDialog(this.canvasMain);
      dlg.Text = "灰度浮雕";
      dlg.Support = AngleDialog.SupportMethod.Emboss;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void reliefToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AngleDialog dlg = new AngleDialog(this.canvasMain);
      dlg.Text = "彩色浮雕";
      dlg.Support = AngleDialog.SupportMethod.Relief;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void addNoiseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "新增杂点";
      dlg.Maximum = 255;
      dlg.Degree = 20;
      dlg.Support = DegreeDialog.SupportMethod.AddNoise;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void sprinkleToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "雪花杂点";
      dlg.Description = "雪花抛撒概率";
      dlg.Maximum = 100;
      dlg.Degree = 5;
      dlg.Support = DegreeDialog.SupportMethod.Sprinkle;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void paperCutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PaperCutDialog dlg = new PaperCutDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void sketchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SketchDialog dlg = new SketchDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void comicToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Comic((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void aquaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Aqua((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Sepia((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void colorizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (colorDialog.ShowDialog() == DialogResult.OK)
      {
        Color color = colorDialog.Color;

        Effect f = new Effect();
        Bitmap srcImage = this.canvasMain.Image;
        Bitmap dstImage = f.Colorize((Bitmap)srcImage.Clone(), color.R, color.G, color.B);

        SaveCanvas(srcImage, dstImage);
      }
    }

    private void iceToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Ice((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void moltenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Molten((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void darknessToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Darkness((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void subtenseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Subtense((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void whimToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Whim((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void pinchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "挤压";
      dlg.Maximum = 32;
      dlg.Minimum = 1;
      dlg.Degree = 15;
      dlg.Support = DegreeDialog.SupportMethod.Pinch;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void spherizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Spherize((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void swirlToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "漩涡";
      dlg.Maximum = 100;
      dlg.Minimum = 1;
      dlg.Degree = 50;
      dlg.Support = DegreeDialog.SupportMethod.Swirl;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void waveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "波浪";
      dlg.Maximum = 32;
      dlg.Minimum = 1;
      dlg.Degree = 15;
      dlg.Support = DegreeDialog.SupportMethod.Wave;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void moireFringeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "摩尔纹";
      dlg.Maximum = 16;
      dlg.Minimum = 1;
      dlg.Degree = 3;
      dlg.Support = DegreeDialog.SupportMethod.MoireFringe;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void diffuseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "扩散";
      dlg.Maximum = 32;
      dlg.Minimum = 1;
      dlg.Degree = 3;
      dlg.Support = DegreeDialog.SupportMethod.Diffuse;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void findEdgeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AngleDialog dlg = new AngleDialog(this.canvasMain);
      dlg.Text = "查找边缘";
      dlg.Support = AngleDialog.SupportMethod.FindEdge;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void glowingEdgesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.GlowingEdge((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void lightingToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "灯光";
      dlg.Description = "光照强度";
      dlg.Degree = 220;
      dlg.Support = DegreeDialog.SupportMethod.Lighting;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void mosaicToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "马赛克";
      dlg.Maximum = 32;
      dlg.Minimum = 1;
      dlg.Degree = 5;
      dlg.Support = DegreeDialog.SupportMethod.Mosaic;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void oilPaintingToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OilPaintingDialog dlg = new OilPaintingDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void solarizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Effect f = new Effect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.Solarize((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void customTemplateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CustomDialog dlg = new CustomDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        Effect f = new Effect();
        Bitmap srcImage = this.canvasMain.Image;
        Bitmap dstImage = f.Custom((Bitmap)srcImage.Clone(), dlg.Sequence);

        SaveCanvas(srcImage, dstImage);
      }
    }

    private void inosculateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      InosculateDialog dlg = new InosculateDialog(this);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void magicToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      MagicDialog dlg = new MagicDialog(this);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();

        // 在 IE 中打开
        ieToolStripMenuItem_Click(sender, e);
      }
    }

    private void redEyeRemovalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      RedEyeRemovalDialog dlg = new RedEyeRemovalDialog(this.canvasMain);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void artStringToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ArtStringDialog dlg = new ArtStringDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        string destFile = saveFolder + history.Current.ToString() + ".txt";

        Effect f = new Effect();
        string text = f.Image2Text((Bitmap)this.canvasMain.Image.Clone(), dlg.ArtString, dlg.BlockWidth, dlg.BlockHeight);

        StreamWriter myStreamWriter = new StreamWriter(destFile, false, System.Text.Encoding.Default);
        myStreamWriter.Write(text);
        myStreamWriter.Close();

        MessageBox.Show("图像文件已被成功转换为文本文件！", "友情提醒",
          MessageBoxButtons.OK, MessageBoxIcon.Information);
        System.Diagnostics.Process.Start("NOTEPAD.EXE", destFile);
      }
    }


    /******************************
     * 
     * 科研应用菜单
     * 
     ******************************/

    /// <summary>
    /// 代数运算
    /// </summary>
    /// <param name="algebraMethod">代数运算方法</param>
    private void AlgebraOperate(Algebra.AlgebraMethod algebraMethod)
    {
      Algebra a = new Algebra();
      a.BackgroundRegion = new Region(new Rectangle(0, 0, this.canvasMain.Width, this.canvasMain.Height));
      a.ForegroundRegion = new Region(this.layer.Bounds);

      Bitmap bgImage = (Bitmap)this.canvasMain.Image.Clone();
      Bitmap fgImage = (Bitmap)this.layer.Image.Clone();
      Bitmap dstImage = a.AlgebraOperate(bgImage, fgImage, algebraMethod);

      this.canvasMain.Image = dstImage;
      SaveCanvas();

      this.layer.Visible = false;
    }

    /// <summary>
    /// 逻辑运算
    /// </summary>
    /// <param name="logicMethod">逻辑运算方法</param>
    private void LogicOperate(Logic.LogicMethod logicMethod)
    {
      Logic l = new Logic();
      l.BackgroundRegion = new Region(new Rectangle(0, 0, this.canvasMain.Width, this.canvasMain.Height));
      l.ForegroundRegion = new Region(this.layer.Bounds);

      Bitmap bgImage = (Bitmap)this.canvasMain.Image.Clone();
      Bitmap fgImage = (Bitmap)this.layer.Image.Clone();
      Bitmap dstImage = l.LogicOperate(bgImage, fgImage, logicMethod);

      this.canvasMain.Image = dstImage;
      SaveCanvas();

      this.layer.Visible = false;
    }

    private void algebraAddToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      AlgebraOperate(Algebra.AlgebraMethod.Add);
    }

    private void algebraSubtractToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      AlgebraOperate(Algebra.AlgebraMethod.Subtract);
    }

    private void algebraMultiplyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      AlgebraOperate(Algebra.AlgebraMethod.Multiply);
    }

    private void algebraDivideToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      AlgebraOperate(Algebra.AlgebraMethod.Divide);
    }

    private void algebraAverageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      AlgebraOperate(Algebra.AlgebraMethod.Average);
    }

    private void algebraDifferToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      AlgebraOperate(Algebra.AlgebraMethod.Differ);
    }

    private void algebraMaximizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      AlgebraOperate(Algebra.AlgebraMethod.Maximize);
    }

    private void algebraMinimumToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      AlgebraOperate(Algebra.AlgebraMethod.Minimum);
    }

    private void logicAndToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      LogicOperate(Logic.LogicMethod.And);
    }

    private void logicOrToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      LogicOperate(Logic.LogicMethod.Or);
    }

    private void logicNotToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Logic l = new Logic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = l.LogicNot((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void logicXorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.layer.Visible)
        return;

      LogicOperate(Logic.LogicMethod.Xor);
    }

    private void autoFitThresholdToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GrayProcessing gp = new GrayProcessing();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = gp.AutoFitThreshold((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void areaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      RegionDialog dlg = new RegionDialog(this.canvasMain);
      dlg.Text = "区域面积信息";
      dlg.IsShowContour = false;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void perimeterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      RegionDialog dlg = new RegionDialog(this.canvasMain);
      dlg.Text = "区域周长信息";
      dlg.IsShowContour = true;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void clearSmallAreaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "消除小区域";
      dlg.Description = "阈值调节";
      dlg.Maximum = 100;
      dlg.Degree = 33;
      dlg.Support = DegreeDialog.SupportMethod.ClearSmallArea;   
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void contourPickToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Segmentation s = new Segmentation();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = s.ContourPick((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void contourTraceToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Segmentation s = new Segmentation();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = s.ContourTrace((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void projectHorzToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Segmentation s = new Segmentation();
      this.canvasMain.Image = s.Project(this.canvasMain.Image, true);

      SaveCanvas();
    }

    private void projectVertToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Segmentation s = new Segmentation();
      this.canvasMain.Image = s.Project(this.canvasMain.Image, false);

      SaveCanvas();
    }

    private void erosionHorzToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.ErosionHorz((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void erosionVertToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.ErosionVert((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void erosionCrossToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.ErosionCross((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void dilationHorzToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.DilationHorz((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void dilationVertToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.DilationVert((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void dilationCrossToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.DilationCross((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void openingToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.Opening((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void closingToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.Closing((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void thinningToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.Thinning((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void thickeningToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Morphologic m = new Morphologic();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = m.Thickening((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void meanNNToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FilterDialog dlg = new FilterDialog(this.canvasMain);
      dlg.Support = Filter.FilteringMethod.Mean;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void autoFitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Filter f = new Filter();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = f.FilterAutoFit((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void medianNNToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FilterDialog dlg = new FilterDialog(this.canvasMain);
      dlg.Support = Filter.FilteringMethod.Median;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void medianCrossToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FilterDialog dlg = new FilterDialog(this.canvasMain);
      dlg.Support = Filter.FilteringMethod.Median;
      dlg.IsWindowFiltering = false;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void maxNNToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FilterDialog dlg = new FilterDialog(this.canvasMain);
      dlg.Support = Filter.FilteringMethod.Maximum;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void minNNToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FilterDialog dlg = new FilterDialog(this.canvasMain);
      dlg.Support = Filter.FilteringMethod.Minimum;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void robertsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EdgeDetect ed = new EdgeDetect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = ed.Roberts((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EdgeDetect ed = new EdgeDetect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = ed.Sobel((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void prewittToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EdgeDetect ed = new EdgeDetect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = ed.Prewitt((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void kirschToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EdgeDetect ed = new EdgeDetect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = ed.Kirsch((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void gaussLaplacianToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EdgeDetect ed = new EdgeDetect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = ed.GaussLaplacian((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void edgeDetectHorzToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EdgeDetect ed = new EdgeDetect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = ed.EdgeDetectHorizontal((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void edgeDetectVertToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EdgeDetect ed = new EdgeDetect();
      Bitmap srcImage = this.canvasMain.Image;
      Bitmap dstImage = ed.EdgeDetectVertical((Bitmap)srcImage.Clone());

      SaveCanvas(srcImage, dstImage);
    }

    private void edgeEnhanceToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "边缘增强";
      dlg.Description = "阈值调节";
      dlg.Maximum = 64;
      dlg.Degree = 16;
      dlg.Support = DegreeDialog.SupportMethod.EdgeEnhance;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void edgeHomogenizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DegreeDialog dlg = new DegreeDialog(this.canvasMain);
      dlg.Text = "边缘均衡化";
      dlg.Description = "阈值调节";
      dlg.Maximum = 64;
      dlg.Degree = 16;
      dlg.Support = DegreeDialog.SupportMethod.EdgeHomogenize;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveCanvas();
      }
    }

    private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AdvancedDialog dlg = new AdvancedDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        Effect f = new Effect();
        Bitmap srcImage = this.canvasMain.Image;
        Bitmap dstImage = f.Custom((Bitmap)srcImage.Clone(), dlg.Sequence);

        SaveCanvas(srcImage, dstImage);
      }
    }


    /******************************
     * 
     * 帮助菜单
     * 
     ******************************/

    private void contentToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (DialogResult.OK == MessageBox.Show(
        "请到本软件官方发布网站查看软件最新动向！\nhttp://www.PhotoSprite.com",
        "友情提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
        System.Diagnostics.Process.Start("http://www.PhotoSprite.com");
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AboutBox dlg = new AboutBox();
      dlg.ShowDialog();
    }

  }
}
