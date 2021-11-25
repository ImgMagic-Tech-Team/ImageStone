namespace PhotoSprite.Dialog
{
  partial class MappingDialog
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
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.drawPictureBox = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.yLabel = new System.Windows.Forms.Label();
      this.xLabel = new System.Windows.Forms.Label();
      this.channelModeComboBox = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.drawPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(175, 330);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(107, 330);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // drawPictureBox
      // 
      this.drawPictureBox.BackColor = System.Drawing.Color.White;
      this.drawPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.drawPictureBox.Location = new System.Drawing.Point(39, 38);
      this.drawPictureBox.Name = "drawPictureBox";
      this.drawPictureBox.Size = new System.Drawing.Size(258, 258);
      this.drawPictureBox.TabIndex = 5;
      this.drawPictureBox.TabStop = false;
      this.drawPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPictureBox_MouseDown);
      this.drawPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPictureBox_MouseMove);
      this.drawPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPictureBox_Paint);
      this.drawPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.drawPictureBox_MouseDoubleClick);
      this.drawPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawPictureBox_MouseUp);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(22, 299);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(11, 12);
      this.label1.TabIndex = 6;
      this.label1.Text = "0";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(274, 299);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(23, 12);
      this.label2.TabIndex = 6;
      this.label2.Text = "255";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 38);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(23, 12);
      this.label3.TabIndex = 6;
      this.label3.Text = "255";
      // 
      // yLabel
      // 
      this.yLabel.AutoSize = true;
      this.yLabel.Location = new System.Drawing.Point(12, 166);
      this.yLabel.Name = "yLabel";
      this.yLabel.Size = new System.Drawing.Size(11, 12);
      this.yLabel.TabIndex = 6;
      this.yLabel.Text = "y";
      // 
      // xLabel
      // 
      this.xLabel.AutoSize = true;
      this.xLabel.Location = new System.Drawing.Point(156, 299);
      this.xLabel.Name = "xLabel";
      this.xLabel.Size = new System.Drawing.Size(11, 12);
      this.xLabel.TabIndex = 6;
      this.xLabel.Text = "x";
      // 
      // channelModeComboBox
      // 
      this.channelModeComboBox.FormattingEnabled = true;
      this.channelModeComboBox.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue",
            "RGB"});
      this.channelModeComboBox.Location = new System.Drawing.Point(53, 12);
      this.channelModeComboBox.Name = "channelModeComboBox";
      this.channelModeComboBox.Size = new System.Drawing.Size(105, 20);
      this.channelModeComboBox.TabIndex = 8;
      this.channelModeComboBox.Text = "RGB";
      this.channelModeComboBox.SelectedIndexChanged += new System.EventHandler(this.channelModeComboBox_SelectedIndexChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(10, 15);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(35, 12);
      this.label4.TabIndex = 7;
      this.label4.Text = "通道:";
      // 
      // MappingDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(318, 367);
      this.Controls.Add(this.channelModeComboBox);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.yLabel);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.xLabel);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.drawPictureBox);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(326, 394);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(326, 394);
      this.Name = "MappingDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "色彩映射";
      this.Load += new System.EventHandler(this.MappingDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.drawPictureBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.PictureBox drawPictureBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label yLabel;
    private System.Windows.Forms.Label xLabel;
    private System.Windows.Forms.ComboBox channelModeComboBox;
    private System.Windows.Forms.Label label4;
  }
}