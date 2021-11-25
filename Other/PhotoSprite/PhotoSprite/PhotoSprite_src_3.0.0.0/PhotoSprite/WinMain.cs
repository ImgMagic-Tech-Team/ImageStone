/***************************************************************************
 * 
 * 
 * PhotoSprite ͼ����
 * 
 * �������ƽ����ƺͿ���Ӧ����һ��
 * ����ˮӡ����ͼ��λ����ͼ���˾����ڶ�ͼ������
 * 
 * 
 * 
 * ���ߣ����� 
 * 2006-4-1 �ռ��������
 * 
 * �����������»�������ͨ����
 *    Window 2000 Server SP4, Windows XP SP2
 *    Microsoft Visual Studio 2005 C#
 * 
 * ��Ȩ���� Copy Right 2005 PhotoSprite.com
 * ��Ҫת�أ���ע������
 * 
 * �����κ����⣬��������ǵļ���֧����վ�� www.PhotoSprite.com
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
     * ������
     * 
     ******************************/

    public WinMain(string fileName)
    {
      InitializeComponent();

      this.startFileName = fileName;
    }

    private void WinMain_Load(object sender, EventArgs e)
    {
      // ��ʼ���û�����
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
        // �����ȡ�û�����ʧ�ܣ����ʼ��һ��Ĭ������
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


      // ָ����ʷ��¼�¼�
      history.HistoryChanged += new EventHandler(this.HistoryStatus);


      // ����һ����ʱĿ¼�����һЩ��ʱ�����ͼ��
      if (!System.IO.Directory.Exists(tmpFolder))
      {
        System.IO.Directory.CreateDirectory(tmpFolder);
      }

      // ��ʼ������
      if (startFileName != "")
      {
        // �򿪹���ͼ���ļ�
        OpenFile(startFileName);
      }
      else
      {
        // ��ӭ����
      }

      // ��ʼ����ʷ״̬
      RefreshHistory();

      // ��ʼ��������
      InitStyleToolbar();
    }

    private void WinMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      bool cancel = false;

      if (history.IsDirty)
      {
        switch (MessageBox.Show("��ǰ�༭��ͼ�����޸ģ�����Ҫ����ͼ����",
          "��������", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
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
        // ɾ����ʱĿ¼
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
     * ����ͼ��
     * 
     ******************************/

    private void mainToolStripContainer_ContentPanel_DoubleClick(object sender, EventArgs e)
    {
      if (!this.canvasMain.Visible)
        openToolStripMenuItem_Click(sender, e);
    }


    /******************************
     * 
     * ����
     * 
     ******************************/

    /// <summary>
    /// ��ͼ���ָ��ѡ�����д���󲢱���
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    private void SaveCanvas(Bitmap bgImage, Bitmap fgImage)
    {
      RegionClip rc = new RegionClip(this.canvasMain.SelectedRegion);
      this.canvasMain.Image = rc.Replace(bgImage, fgImage);

      SaveCanvas();
    }

    /// <summary>
    /// ���������ݱ��浽ͼ���ļ���
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

      // ��ȡ�������
      int width = this.canvasMain.Width;
      int height = this.canvasMain.Height;

      // ��ȡ�������
      int W = this.mainToolStripContainer.ContentPanel.Width;
      int H = this.mainToolStripContainer.ContentPanel.Height;

      // �жϻ�������Ƿ񳬳��������
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

      // �жϻ����߶��Ƿ񳬳������߶�
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

      // �������ж�λ����
      this.canvasMain.Left = (W - width) / 2;
      this.canvasMain.Top = (H - height) / 2;

      // ��״̬����ʾͼ��ߴ�
      this.sizeToolStripStatusLabel.Text = this.canvasMain.Image.Width.ToString() + "��" + this.canvasMain.Image.Height.ToString();
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
     * ��
     * 
     ******************************/

    private void layer_VisibleChanged(object sender, EventArgs e)
    {
      this.showLayerToolStripMenuItem.Enabled = true;

      this.showLayerToolStripMenuItem.Checked = this.layer.Visible;
    }


    /******************************
     * 
     * ������
     * 
     ******************************/

    /// <summary>
    /// ��ֱ����
    /// </summary>
    private void VertScroll()
    {
      int H = this.mainToolStripContainer.ContentPanel.Height;
      int h = this.canvasMain.Height;
      int y = this.vScrollBar.Value;
      this.canvasMain.Top = (H * 4 / 5 - h) * y / h + H / 10;
    } // end of VertScroll

    /// <summary>
    /// ��������ʱ���鿴ͼ����������
    /// </summary>
    private void Canvas_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      // ����껬��ÿ����һ��ʱ�������������ľ���
      int minScrollSize = (this.vScrollBar.Maximum - this.vScrollBar.Minimum) / 10;

      if (this.vScrollBar.Visible)
      {
        if (e.Delta < 0)
        {
          // ���¹���
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
          // ���Ϲ���
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
    /// ˮƽ����
    /// </summary>
    private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
    {
      int W = this.mainToolStripContainer.ContentPanel.Width;
      int w = this.canvasMain.Width;
      int x = this.hScrollBar.Value;
      this.canvasMain.Left = (W * 4 / 5 - w) * x / w + W / 10;
    }

    /// <summary>
    /// ��ֱ����
    /// </summary>
    private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
    {
      VertScroll();
    }


  }
}