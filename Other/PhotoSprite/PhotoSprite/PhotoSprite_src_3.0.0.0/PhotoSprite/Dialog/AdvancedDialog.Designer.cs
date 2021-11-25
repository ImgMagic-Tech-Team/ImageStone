namespace PhotoSprite.Dialog
{
  partial class AdvancedDialog
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnExport = new System.Windows.Forms.Button();
      this.btnImport = new System.Windows.Forms.Button();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.templateRichTextBox = new System.Windows.Forms.RichTextBox();
      this.groupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnExport
      // 
      this.btnExport.Location = new System.Drawing.Point(223, 114);
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(51, 23);
      this.btnExport.TabIndex = 4;
      this.btnExport.Text = "导出";
      this.btnExport.UseVisualStyleBackColor = true;
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // btnImport
      // 
      this.btnImport.Location = new System.Drawing.Point(223, 85);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(51, 23);
      this.btnImport.TabIndex = 3;
      this.btnImport.Text = "导入";
      this.btnImport.UseVisualStyleBackColor = true;
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // saveFileDialog
      // 
      this.saveFileDialog.DefaultExt = "acf";
      this.saveFileDialog.Filter = "自定义滤镜 (*.ACF)|*.ACF";
      this.saveFileDialog.Title = "导出";
      // 
      // openFileDialog
      // 
      this.openFileDialog.DefaultExt = "acf";
      this.openFileDialog.Filter = "自定义滤镜 (*.ACF)|*.ACF";
      this.openFileDialog.Title = "导入";
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(223, 12);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(223, 41);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.templateRichTextBox);
      this.groupBox.Location = new System.Drawing.Point(12, 2);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(205, 193);
      this.groupBox.TabIndex = 15;
      this.groupBox.TabStop = false;
      // 
      // templateRichTextBox
      // 
      this.templateRichTextBox.Location = new System.Drawing.Point(6, 12);
      this.templateRichTextBox.Name = "templateRichTextBox";
      this.templateRichTextBox.Size = new System.Drawing.Size(193, 175);
      this.templateRichTextBox.TabIndex = 0;
      this.templateRichTextBox.Text = "";
      // 
      // AdvancedDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 206);
      this.Controls.Add(this.btnExport);
      this.Controls.Add(this.btnImport);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.groupBox);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(292, 240);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(292, 240);
      this.Name = "AdvancedDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "高级模板设置";
      this.groupBox.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.RichTextBox templateRichTextBox;
  }
}