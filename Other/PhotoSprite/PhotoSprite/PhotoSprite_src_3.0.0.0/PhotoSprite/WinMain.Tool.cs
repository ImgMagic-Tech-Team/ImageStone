using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using PhotoSprite.Tool;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite
{
  public partial class WinMain
  {
    /******************************
     * 
     * 主工具条
     * 
     ******************************/

    private void newToolStripButton_Click(object sender, EventArgs e)
    {
      this.newToolStripMenuItem_Click(sender, e);
    }

    private void openToolStripButton_Click(object sender, EventArgs e)
    {
      this.openToolStripMenuItem_Click(sender, e);
    }

    private void saveToolStripButton_Click(object sender, EventArgs e)
    {
      this.saveAsToolStripMenuItem_Click(sender, e);
    }

    private void printPreviewToolStripButton_Click(object sender, EventArgs e)
    {
      this.printPreviewToolStripMenuItem_Click(sender, e);
    }

    private void printToolStripButton_Click(object sender, EventArgs e)
    {
      this.printToolStripMenuItem_Click(sender, e);
    }

    private void cutToolStripButton_Click(object sender, EventArgs e)
    {
      this.cutToolStripMenuItem_Click(sender, e);
    }

    private void copyToolStripButton_Click(object sender, EventArgs e)
    {
      this.copyToolStripMenuItem_Click(sender, e);
    }

    private void pasteToolStripButton_Click(object sender, EventArgs e)
    {
      this.pasteToolStripMenuItem_Click(sender, e);
    }

    private void undoToolStripButton_Click(object sender, EventArgs e)
    {
      this.undoToolStripMenuItem_Click(sender, e);
    }

    private void redoToolStripButton_Click(object sender, EventArgs e)
    {
      this.redoToolStripMenuItem_Click(sender, e);
    }

    private void ieToolStripButton_Click(object sender, EventArgs e)
    {
      this.ieToolStripMenuItem_Click(sender, e);
    }

    private void fireworksToolStripButton_Click(object sender, EventArgs e)
    {
      this.fireworksToolStripMenuItem_Click(sender, e);
    }

    private void photoshopToolStripButton_Click(object sender, EventArgs e)
    {
      this.photoshopToolStripMenuItem_Click(sender, e);
    }

    private void helpToolStripButton_Click(object sender, EventArgs e)
    {
      this.contentToolStripMenuItem_Click(sender, e);
    }


    /******************************
     * 
     * 风格工具条
     * 
     ******************************/

    private void InitStyleToolbar()
    {
      // 获取所有已经安装的字体系列
      InstalledFontCollection ifc = new InstalledFontCollection();
      foreach (FontFamily ff in ifc.Families)
      {
        this.fontFamilyToolStripComboBox.Items.Add(ff.Name);
      }

      // 初始化画笔宽度
      for (int i = 1; i < 100; i++)
      {
        this.brushWidthToolStripComboBox.Items.Add(i.ToString());
      }

      // 初始化图案
      this.hatchStyleToolStripComboBox.Items.Add("Solid Brush");
      foreach (string styleName in Enum.GetNames(typeof(HatchStyle)))
      {
        this.hatchStyleToolStripComboBox.Items.Add(styleName);
      }
      this.hatchStyleToolStripComboBox.SelectedIndex = 0;
      this.hatchStyleToolStripComboBox.ComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
      this.hatchStyleToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.hatchStyleToolStripComboBox.DropDownWidth = 190;
      this.hatchStyleToolStripComboBox.ComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.hatchStyleToolStripComboBox_DrawItem);

      // 初始化前背景按扭
      this.fgColorToolStripButton.Image = ColorPickerDialog.DrawColorButton(this.foreColor);
      this.bgColorToolStripButton.Image = ColorPickerDialog.DrawColorButton(this.backColor);
    }

    private void hatchStyleToolStripComboBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
    {
      e.DrawBackground();
      Rectangle rect = e.Bounds;
      
      if (e.Index != -1)
      {
        if (e.Index > 0)
        {
          Rectangle rd = rect;
          rd.Width = rd.Left + 25;

          Rectangle rt = rect;
          rect.X = rd.Right;

          string displayText = this.hatchStyleToolStripComboBox.Items[e.Index].ToString();
          HatchStyle hs = (HatchStyle)Enum.Parse(typeof(HatchStyle), displayText, true);

          using (HatchBrush hb = new HatchBrush(hs, e.ForeColor, e.BackColor))
          {
            e.Graphics.FillRectangle(hb, rd);
          }

          StringFormat sf = new StringFormat();
          sf.Alignment = StringAlignment.Near;

          using (SolidBrush sb = new SolidBrush(Color.White))
          {
            if ((e.State & DrawItemState.Focus) == 0)
            {
              sb.Color = SystemColors.Window;
              e.Graphics.FillRectangle(sb, rect);
              sb.Color = SystemColors.WindowText;
              e.Graphics.DrawString(displayText, this.Font, sb, rect, sf);
            }
            else
            {
              sb.Color = SystemColors.Highlight;
              e.Graphics.FillRectangle(sb, rect);
              sb.Color = SystemColors.HighlightText;
              e.Graphics.DrawString(displayText, this.Font, sb, rect, sf);
            }
          }
        }
        else
        {
          // Solid brush
          using (SolidBrush sb = new SolidBrush(Color.White))
          {
            if ((e.State & DrawItemState.Focus) == 0)
            {
              sb.Color = SystemColors.Window;
              e.Graphics.FillRectangle(sb, e.Bounds);
              string displayText = this.hatchStyleToolStripComboBox.Items[e.Index].ToString();
              sb.Color = SystemColors.WindowText;
              e.Graphics.DrawString(displayText, this.Font, sb, e.Bounds);
            }
            else
            {
              sb.Color = SystemColors.Highlight;
              e.Graphics.FillRectangle(sb, e.Bounds);
              string displayText = this.hatchStyleToolStripComboBox.Items[e.Index].ToString();
              sb.Color = SystemColors.HighlightText;
              e.Graphics.DrawString(displayText, this.Font, sb, e.Bounds);
            }
          }
        }

        e.DrawFocusRectangle();
      }
    }

    private void brushWidthToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.brushWidth = Convert.ToInt32(this.brushWidthToolStripComboBox.SelectedItem);
    }

    private void hatchStyleToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      int index = this.hatchStyleToolStripComboBox.SelectedIndex;
      if (index == 0)
      {
        this.hasBrushStyle = false;
      }
      else
      {
        this.hasBrushStyle = true;
        string displayText = this.hatchStyleToolStripComboBox.Items[index].ToString();
        this.hatchStyle = (HatchStyle)Enum.Parse(typeof(HatchStyle), displayText, true);
      }
    }

    private void fgColorToolStripButton_Click(object sender, EventArgs e)
    {
      ColorPickerDialog dlg = new ColorPickerDialog();
      dlg.ArgbColor = this.foreColor;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.foreColor = dlg.ArgbColor;
        this.fgColorToolStripButton.Image = ColorPickerDialog.DrawColorButton(this.foreColor);
      }
    }

    private void bgColorToolStripButton_Click(object sender, EventArgs e)
    {
      ColorPickerDialog dlg = new ColorPickerDialog();
      dlg.ArgbColor = this.backColor;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.backColor = dlg.ArgbColor;
        this.bgColorToolStripButton.Image = ColorPickerDialog.DrawColorButton(this.backColor);
      }
    }


    /******************************
     * 
     * 字体工具条
     * 详见 TextTool 文字工具类
     * 
     ******************************/



    /******************************
     * 
     * 工具箱
     * 
     ******************************/

    /// <summary>
    /// 选择工具
    /// </summary>
    /// <param name="toolType">工具类型</param>
    private void ToolboxClick(ToolType toolType)
    {
      if (toolType == this.toolType)
        return;
      
      CreateTools(toolType);
      DeactivateToolbox(this.toolType);
      UpdateToolbox(toolType);
      ActivateToolbox(toolType);

      this.toolType = toolType;
    }

    /// <summary>
    /// 建立指定的工具实例
    /// </summary>
    /// <param name="toolType">工具类型</param>
    private void CreateTools(ToolType toolType)
    {
      switch (toolType)
      {
        case ToolType.Move:
          break;

        case ToolType.RectangleSelect:
          rectangleSelectTool = new RectangleSelectTool(this.canvasMain);
          break;

        case ToolType.EllipseSelect:
          ellipseSelectTool = new EllipseSelectTool(this.canvasMain);
          break;

        case ToolType.LassoSelect:
          lassoSelectTool = new LassoSelectTool(this.canvasMain);
          break;

        case ToolType.Pencil:
          pencilTool = new PencilTool(this.canvasMain);
          pencilTool.DrawingFinished += new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Brush:
          brushTool = new BrushTool(this.canvasMain);
          brushTool.DrawingFinished += new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Eraser:
          eraserTool = new EraserTool(this.canvasMain);
          eraserTool.DrawingFinished += new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Line:
          lineTool = new LineTool(this.canvasMain);
          lineTool.DrawingFinished += new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Rectangle:
          rectangleTool = new RectangleTool(this.canvasMain);
          rectangleTool.DrawingFinished += new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Text:
          textTool = new TextTool(this);
          textTool.DrawingFinished += new EventHandler(this.SaveCanvas);
          break;

        case ToolType.ColorPicker:
          colorPickerTool = new ColorPickerTool(this);
          break;

        case ToolType.PaintBucket:
          paintBucketTool = new PaintBucketTool(this.canvasMain);
          paintBucketTool.DrawingFinished += new EventHandler(this.SaveCanvas);
          break;
      }
    }

    /// <summary>
    /// 激活工具
    /// </summary>
    /// <param name="toolType">工具类型</param>
    private void ActivateToolbox(ToolType toolType)
    {
      switch (toolType)
      {
        case ToolType.Move:
          this.moveToolStripButton.Checked = true;
          break;

        case ToolType.RectangleSelect:
          this.rectangleSelectToolStripButton.Checked = true;
          rectangleSelectTool.OnActivate();
          break;

        case ToolType.EllipseSelect:
          this.ellipseSelectToolStripButton.Checked = true;
          ellipseSelectTool.OnActivate();
          break;

        case ToolType.LassoSelect:
          this.lassoSelectToolStripButton.Checked = true;
          lassoSelectTool.OnActivate();
          break;

        case ToolType.Pencil:
          this.pencilToolStripButton.Checked = true;
          pencilTool.OnActivate();
          break;

        case ToolType.Brush:
          this.brushToolStripButton.Checked = true;
          brushTool.OnActivate();
          break;

        case ToolType.Eraser:
          this.eraserToolStripButton.Checked = true;
          eraserTool.OnActivate();
          break;

        case ToolType.Line:
          this.lineToolStripButton.Checked = true;
          lineTool.OnActivate();
          break;

        case ToolType.Rectangle:
          this.rectangleToolStripButton.Checked = true;
          rectangleTool.OnActivate();
          break;

        case ToolType.Text:
          this.textToolStripButton.Checked = true;
          textTool.OnActivate();
          break;

        case ToolType.ColorPicker:
          this.colorPickerToolStripButton.Checked = true;
          colorPickerTool.OnActivate();
          break;

        case ToolType.PaintBucket:
          this.paintBucketToolStripButton.Checked = true;
          paintBucketTool.OnActivate();
          break;
      }
    }

    /// <summary>
    /// 释放工具
    /// </summary>
    /// <param name="toolType">工具类型</param>
    private void DeactivateToolbox(ToolType toolType)
    {
      switch (toolType)
      {
        case ToolType.Move:
          this.moveToolStripButton.Checked = false;
          break;

        case ToolType.RectangleSelect:
          this.rectangleSelectToolStripButton.Checked = false;
          rectangleSelectTool.OnDeactivate();
          break;

        case ToolType.EllipseSelect:
          this.ellipseSelectToolStripButton.Checked = false;
          ellipseSelectTool.OnDeactivate();
          break;

        case ToolType.LassoSelect:
          this.lassoSelectToolStripButton.Checked = false;
          lassoSelectTool.OnDeactivate();
          break;

        case ToolType.Pencil:
          this.pencilToolStripButton.Checked = false;
          pencilTool.OnDeactivate();
          pencilTool.DrawingFinished -= new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Brush:
          this.brushToolStripButton.Checked = false;
          brushTool.OnDeactivate();
          brushTool.DrawingFinished -= new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Eraser:
          this.eraserToolStripButton.Checked = false;
          eraserTool.OnDeactivate();
          eraserTool.DrawingFinished -= new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Line:
          this.lineToolStripButton.Checked = false;
          lineTool.OnDeactivate();
          lineTool.DrawingFinished -= new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Rectangle:
          this.rectangleToolStripButton.Checked = false;
          rectangleTool.OnDeactivate();
          rectangleTool.DrawingFinished -= new EventHandler(this.SaveCanvas);
          break;

        case ToolType.Text:
          this.textToolStripButton.Checked = false;
          textTool.OnDeactivate();
          textTool.DrawingFinished -= new EventHandler(this.SaveCanvas);
          break;

        case ToolType.ColorPicker:
          this.colorPickerToolStripButton.Checked = false;
          colorPickerTool.OnDeactivate();
          break;

        case ToolType.PaintBucket:
          this.paintBucketToolStripButton.Checked = false;
          paintBucketTool.OnDeactivate();
          paintBucketTool.DrawingFinished -= new EventHandler(this.SaveCanvas);
          break;
      }
    }

    /// <summary>
    /// 更新工具
    /// </summary>
    /// <param name="toolType">工具类型</param>
    private void UpdateToolbox(ToolType toolType)
    {
      switch (toolType)
      {
        case ToolType.Move:
          break;

        case ToolType.RectangleSelect:
          break;

        case ToolType.EllipseSelect:
          break;

        case ToolType.LassoSelect:
          break;

        case ToolType.Pencil:
          pencilTool.BackColor = this.backColor;
          pencilTool.ForeColor = this.foreColor;
          pencilTool.BrushWidth = this.brushWidth;
          break;

        case ToolType.Brush:
          brushTool.BackColor = this.backColor;
          brushTool.ForeColor = this.foreColor;
          brushTool.BrushWidth = this.brushWidth;
          brushTool.FillStyle = this.hatchStyle;
          brushTool.HasBrushStyle = this.hasBrushStyle;
          break;

        case ToolType.Eraser:
          eraserTool.BrushWidth = this.brushWidth;
          break;

        case ToolType.Line:
          lineTool.BackColor = this.backColor;
          lineTool.ForeColor = this.foreColor;
          lineTool.BrushWidth = this.brushWidth;
          break;

        case ToolType.Rectangle:
          rectangleTool.BackColor = this.backColor;
          rectangleTool.ForeColor = this.foreColor;
          rectangleTool.BrushWidth = this.brushWidth;
          break;

        case ToolType.Text:
          break;

        case ToolType.ColorPicker:
          break;

        case ToolType.PaintBucket:
          paintBucketTool.BackColor = this.backColor;
          paintBucketTool.ForeColor = this.foreColor;
          break;
      }
    }

    private void moveToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.Move);
    }

    private void rectangleSelectToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.RectangleSelect);
    }

    private void ellipseSelectToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.EllipseSelect);
    }

    private void lassoSelectToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.LassoSelect);
    }

    private void pencilToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.Pencil);
    }

    private void brushToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.Brush);
    }

    private void eraserToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.Eraser);
    }

    private void lineToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.Line);
    }

    private void rectangleToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.Rectangle);
    }

    private void textToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.Text);
    }

    private void colorPickerToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.ColorPicker);
    }

    private void paintBucketToolStripButton_Click(object sender, EventArgs e)
    {
      ToolboxClick(ToolType.PaintBucket);
    }


  }
}
