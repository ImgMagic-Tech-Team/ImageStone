using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Tool
{
  /// <summary>
  /// 文本工具类
  /// </summary>
  public class TextTool : Tool
  {
    private WinMain parentWindow = null;
    private TextBox textBox = null;
    private Canvas canvas = null;
    private bool mouseDown = false;
    private Point lastPoint = Point.Empty;
    private Point mouseXY = Point.Empty;

    /// <summary>
    /// 使用文本工具
    /// </summary>
    /// <param name="parent">主窗口</param>
    public TextTool(WinMain parent)
    {
      this.parentWindow = parent;
      this.textBox = parent.textBoxTool;
      this.canvas = parent.canvasMain;
    }

    public void OnActivate()
    {
      this.canvas.MouseDown += this.OnMouseDown;
      this.canvas.MouseMove += this.OnMouseMove;
      this.canvas.MouseUp += this.OnMouseUp;

      this.parentWindow.fontFamilyToolStripComboBox.SelectedIndexChanged += this.fontFamilyToolStripComboBox_SelectedIndexChanged;
      this.parentWindow.fontSizeToolStripComboBox.SelectedIndexChanged += this.fontSizeToolStripComboBox_SelectedIndexChanged;
      this.parentWindow.boldToolStripButton.Click += this.boldToolStripButton_Click;
      this.parentWindow.italicToolStripButton.Click += this.italicToolStripButton_Click;
      this.parentWindow.underlineToolStripButton.Click += this.underlineToolStripButton_Click;

      this.textBox.TextChanged += this.textBox_TextChanged;
    }

    public void OnDeactivate()
    {
      this.textBox.Text = "";
      this.textBox.Visible = false;

      this.canvas.MouseDown -= this.OnMouseDown;
      this.canvas.MouseMove -= this.OnMouseMove;
      this.canvas.MouseUp -= this.OnMouseUp;

      this.parentWindow.fontFamilyToolStripComboBox.SelectedIndexChanged -= this.fontFamilyToolStripComboBox_SelectedIndexChanged;
      this.parentWindow.fontSizeToolStripComboBox.SelectedIndexChanged -= this.fontSizeToolStripComboBox_SelectedIndexChanged;
      this.parentWindow.boldToolStripButton.Click -= this.boldToolStripButton_Click;
      this.parentWindow.italicToolStripButton.Click -= this.italicToolStripButton_Click;
      this.parentWindow.underlineToolStripButton.Click -= this.underlineToolStripButton_Click;

      this.textBox.TextChanged -= this.textBox_TextChanged;
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
      // 左键进行文字效果调节，右键绘制到图像
      if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
      {
        if (lastPoint != mouseXY)
          return;

        mouseDown = true;
        mouseXY = new Point(e.X, e.Y);

        this.textBox.Visible = true;
        this.textBox.Location = new Point(mouseXY.X, mouseXY.Y - this.textBox.Height / 2);
      }
      else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
      {
        if (this.textBox.Text == "") 
          return;

        UpdateFontStyle(true);

        this.textBox.Visible = false;

        // 触发绘图结束事件
        OnDrawingFinished();
      }
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        mouseXY = new Point(e.X, e.Y);

        if (mouseXY != lastPoint)
        {
          this.textBox.Location = new Point(mouseXY.X, mouseXY.Y - this.textBox.Height / 2);

          lastPoint = mouseXY;
        }
      }
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        mouseXY = new Point(e.X, e.Y);
        this.textBox.Location = new Point(mouseXY.X, mouseXY.Y - this.textBox.Height / 2);

        this.textBox.Focus();
        mouseDown = false;
      }
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


    /// <summary>
    /// 更新文字风格
    /// </summary>
    /// <param name="needDraw">需要绘到图像上吗？</param>
    private void UpdateFontStyle(bool needDraw)
    {
      FontFamily fontFamily = new FontFamily("宋体");
      string selectedText = this.parentWindow.fontFamilyToolStripComboBox.SelectedItem.ToString();
      if (selectedText != "-----")
      {
        fontFamily = new FontFamily(selectedText);
      }

      int fontSize = Convert.ToInt32(this.parentWindow.fontSizeToolStripComboBox.SelectedItem);

      FontStyle fontStyle = FontStyle.Regular;
      if (this.parentWindow.boldToolStripButton.Checked) fontStyle |= FontStyle.Bold;
      if (this.parentWindow.italicToolStripButton.Checked) fontStyle |= FontStyle.Italic;
      if (this.parentWindow.underlineToolStripButton.Checked) fontStyle |= FontStyle.Underline;

      string text = this.textBox.Text;


      Color color = this.parentWindow.foreColor;
      Font font;
      try
      {
        font = new Font(fontFamily, fontSize, fontStyle, GraphicsUnit.Pixel);
      }
      catch
      {
        font = new Font(new FontFamily("宋体"), 12, FontStyle.Regular, GraphicsUnit.Pixel);
      }

      if (needDraw)
      {
        if (this.textBox.Text == "")
          return;

        // 水印文字类
        Watermark water = new Watermark();
        water.X = this.textBox.Location.X;
        water.Y = this.textBox.Location.Y;
        water.FontColor = color;
        water.FontFamily = fontFamily.Name;
        water.FontSize = fontSize;
        water.FontWeight = fontStyle;

        this.canvas.Image = water.WaterText(this.canvas.Image, text, true);
        this.canvas.Invalidate();

        this.textBox.Text = "";
        this.textBox.Visible = false;
      }
      else
      {
        Graphics g = this.textBox.CreateGraphics();
        SizeF size = g.MeasureString(text, font);
        this.textBox.Size = new Size((int)size.Width + 2, (int)size.Height);
        this.textBox.Font = font;
        this.textBox.ForeColor = color;
      }
    }

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      UpdateFontStyle(false);
    }


    /******************************
     * 
     * 字体工具条
     * 
     ******************************/

    private void fontFamilyToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      string selectedText = this.parentWindow.fontFamilyToolStripComboBox.SelectedText.ToString();
      if (selectedText != "-----")
      {
        UpdateFontStyle(false);
      }
    }

    private void fontSizeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateFontStyle(false);
    }

    private void boldToolStripButton_Click(object sender, EventArgs e)
    {
      this.parentWindow.boldToolStripButton.Checked ^= true;
      UpdateFontStyle(false);
    }

    private void italicToolStripButton_Click(object sender, EventArgs e)
    {
      this.parentWindow.italicToolStripButton.Checked ^= true;
      UpdateFontStyle(false);
    }

    private void underlineToolStripButton_Click(object sender, EventArgs e)
    {
      this.parentWindow.underlineToolStripButton.Checked ^= true;
      UpdateFontStyle(false);
    }

  }
}
