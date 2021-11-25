namespace PhotoSprite.Dialog
{
  partial class PaperCutDialog
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
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.thresholdUpDown = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.fgLabel = new System.Windows.Forms.Label();
      this.bgLabel = new System.Windows.Forms.Label();
      this.thresholdTrackBar = new System.Windows.Forms.TrackBar();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.colorDialog = new System.Windows.Forms.ColorDialog();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.thresholdUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.thresholdUpDown);
      this.groupBox.Controls.Add(this.label3);
      this.groupBox.Controls.Add(this.label2);
      this.groupBox.Controls.Add(this.label1);
      this.groupBox.Controls.Add(this.fgLabel);
      this.groupBox.Controls.Add(this.bgLabel);
      this.groupBox.Controls.Add(this.thresholdTrackBar);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(268, 105);
      this.groupBox.TabIndex = 8;
      this.groupBox.TabStop = false;
      // 
      // thresholdUpDown
      // 
      this.thresholdUpDown.Location = new System.Drawing.Point(210, 32);
      this.thresholdUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.thresholdUpDown.Name = "thresholdUpDown";
      this.thresholdUpDown.Size = new System.Drawing.Size(50, 21);
      this.thresholdUpDown.TabIndex = 4;
      this.thresholdUpDown.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
      this.thresholdUpDown.ValueChanged += new System.EventHandler(this.thresholdUpDown_ValueChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(111, 81);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(47, 12);
      this.label3.TabIndex = 6;
      this.label3.Text = "前景色:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 81);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(47, 12);
      this.label2.TabIndex = 6;
      this.label2.Text = "背景色:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(59, 12);
      this.label1.TabIndex = 6;
      this.label1.Text = "阈值调节:";
      // 
      // fgLabel
      // 
      this.fgLabel.BackColor = System.Drawing.Color.Red;
      this.fgLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.fgLabel.Location = new System.Drawing.Point(164, 71);
      this.fgLabel.Name = "fgLabel";
      this.fgLabel.Size = new System.Drawing.Size(24, 24);
      this.fgLabel.TabIndex = 5;
      this.fgLabel.Click += new System.EventHandler(this.fgLabel_Click);
      // 
      // bgLabel
      // 
      this.bgLabel.BackColor = System.Drawing.Color.White;
      this.bgLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.bgLabel.Location = new System.Drawing.Point(59, 71);
      this.bgLabel.Name = "bgLabel";
      this.bgLabel.Size = new System.Drawing.Size(24, 24);
      this.bgLabel.TabIndex = 5;
      this.bgLabel.Click += new System.EventHandler(this.bgLabel_Click);
      // 
      // thresholdTrackBar
      // 
      this.thresholdTrackBar.AutoSize = false;
      this.thresholdTrackBar.Location = new System.Drawing.Point(6, 32);
      this.thresholdTrackBar.Maximum = 255;
      this.thresholdTrackBar.Name = "thresholdTrackBar";
      this.thresholdTrackBar.Size = new System.Drawing.Size(200, 26);
      this.thresholdTrackBar.TabIndex = 3;
      this.thresholdTrackBar.TickFrequency = 0;
      this.thresholdTrackBar.Value = 128;
      this.thresholdTrackBar.Scroll += new System.EventHandler(this.thresholdTrackBar_Scroll);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(217, 123);
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
      this.btnOk.Location = new System.Drawing.Point(151, 123);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // PaperCutDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 157);
      this.Controls.Add(this.groupBox);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(300, 184);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(300, 184);
      this.Name = "PaperCutDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "剪纸";
      this.Load += new System.EventHandler(this.PaperCutDialog_Load);
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.thresholdUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.TrackBar thresholdTrackBar;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Label bgLabel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label fgLabel;
    private System.Windows.Forms.ColorDialog colorDialog;
    private System.Windows.Forms.NumericUpDown thresholdUpDown;
  }
}