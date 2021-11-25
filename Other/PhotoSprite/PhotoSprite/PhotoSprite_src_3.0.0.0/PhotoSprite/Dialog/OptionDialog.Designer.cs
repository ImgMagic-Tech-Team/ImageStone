namespace PhotoSprite.Dialog
{
  partial class OptionDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionDialog));
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.tmpFolderTextBox = new System.Windows.Forms.TextBox();
      this.saveFolderTextBox = new System.Windows.Forms.TextBox();
      this.openFolderTextBox = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.undoTimesUpDown = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.associationCheckBox = new System.Windows.Forms.CheckBox();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.undoTimesUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(193, 210);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(136, 210);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 2;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.associationCheckBox);
      this.groupBox.Controls.Add(this.tmpFolderTextBox);
      this.groupBox.Controls.Add(this.saveFolderTextBox);
      this.groupBox.Controls.Add(this.openFolderTextBox);
      this.groupBox.Controls.Add(this.label5);
      this.groupBox.Controls.Add(this.undoTimesUpDown);
      this.groupBox.Controls.Add(this.label2);
      this.groupBox.Controls.Add(this.label3);
      this.groupBox.Controls.Add(this.label1);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(260, 192);
      this.groupBox.TabIndex = 10;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "首选参数";
      // 
      // tmpFolderTextBox
      // 
      this.tmpFolderTextBox.Location = new System.Drawing.Point(8, 110);
      this.tmpFolderTextBox.Name = "tmpFolderTextBox";
      this.tmpFolderTextBox.Size = new System.Drawing.Size(246, 21);
      this.tmpFolderTextBox.TabIndex = 5;
      // 
      // saveFolderTextBox
      // 
      this.saveFolderTextBox.Location = new System.Drawing.Point(8, 71);
      this.saveFolderTextBox.Name = "saveFolderTextBox";
      this.saveFolderTextBox.Size = new System.Drawing.Size(246, 21);
      this.saveFolderTextBox.TabIndex = 4;
      // 
      // openFolderTextBox
      // 
      this.openFolderTextBox.Location = new System.Drawing.Point(8, 32);
      this.openFolderTextBox.Name = "openFolderTextBox";
      this.openFolderTextBox.Size = new System.Drawing.Size(246, 21);
      this.openFolderTextBox.TabIndex = 3;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 95);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(107, 12);
      this.label5.TabIndex = 0;
      this.label5.Text = "临时文件存放目录:";
      // 
      // undoTimesUpDown
      // 
      this.undoTimesUpDown.Location = new System.Drawing.Point(95, 140);
      this.undoTimesUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.undoTimesUpDown.Name = "undoTimesUpDown";
      this.undoTimesUpDown.Size = new System.Drawing.Size(58, 21);
      this.undoTimesUpDown.TabIndex = 6;
      this.undoTimesUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 12);
      this.label2.TabIndex = 0;
      this.label2.Text = "默认图像保存目录:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 146);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(83, 12);
      this.label3.TabIndex = 0;
      this.label3.Text = "最大撤消次数:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(107, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "默认图像打开目录:";
      // 
      // associationCheckBox
      // 
      this.associationCheckBox.AutoSize = true;
      this.associationCheckBox.Checked = true;
      this.associationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.associationCheckBox.Location = new System.Drawing.Point(8, 167);
      this.associationCheckBox.Name = "associationCheckBox";
      this.associationCheckBox.Size = new System.Drawing.Size(102, 16);
      this.associationCheckBox.TabIndex = 7;
      this.associationCheckBox.Text = "关联\"PSF\"文件";
      this.associationCheckBox.UseVisualStyleBackColor = true;
      this.associationCheckBox.CheckedChanged += new System.EventHandler(this.associationCheckBox_CheckedChanged);
      // 
      // OptionDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 245);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(292, 272);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(292, 272);
      this.Name = "OptionDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "选项";
      this.Load += new System.EventHandler(this.OptionDialog_Load);
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.undoTimesUpDown)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.NumericUpDown undoTimesUpDown;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox saveFolderTextBox;
    private System.Windows.Forms.TextBox openFolderTextBox;
    private System.Windows.Forms.TextBox tmpFolderTextBox;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.CheckBox associationCheckBox;
  }
}