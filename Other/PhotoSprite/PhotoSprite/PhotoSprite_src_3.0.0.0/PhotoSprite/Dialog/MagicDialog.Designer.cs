namespace PhotoSprite.Dialog
{
  partial class MagicDialog
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
      this.contrastUpDown = new System.Windows.Forms.NumericUpDown();
      this.btnCancel = new System.Windows.Forms.Button();
      this.transparencyUpDown = new System.Windows.Forms.NumericUpDown();
      this.transparencyTrackBar = new System.Windows.Forms.TrackBar();
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.contrastTrackBar = new System.Windows.Forms.TrackBar();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.contrastUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.transparencyUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.transparencyTrackBar)).BeginInit();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
      this.SuspendLayout();
      // 
      // contrastUpDown
      // 
      this.contrastUpDown.Location = new System.Drawing.Point(266, 59);
      this.contrastUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
      this.contrastUpDown.Name = "contrastUpDown";
      this.contrastUpDown.Size = new System.Drawing.Size(50, 21);
      this.contrastUpDown.TabIndex = 6;
      this.contrastUpDown.ValueChanged += new System.EventHandler(this.contrastUpDown_ValueChanged);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(253, 114);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // transparencyUpDown
      // 
      this.transparencyUpDown.Location = new System.Drawing.Point(266, 26);
      this.transparencyUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.transparencyUpDown.Name = "transparencyUpDown";
      this.transparencyUpDown.Size = new System.Drawing.Size(50, 21);
      this.transparencyUpDown.TabIndex = 4;
      this.transparencyUpDown.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
      this.transparencyUpDown.ValueChanged += new System.EventHandler(this.transparencyUpDown_ValueChanged);
      // 
      // transparencyTrackBar
      // 
      this.transparencyTrackBar.AutoSize = false;
      this.transparencyTrackBar.Location = new System.Drawing.Point(60, 24);
      this.transparencyTrackBar.Maximum = 255;
      this.transparencyTrackBar.Name = "transparencyTrackBar";
      this.transparencyTrackBar.Size = new System.Drawing.Size(200, 26);
      this.transparencyTrackBar.TabIndex = 3;
      this.transparencyTrackBar.TickFrequency = 0;
      this.transparencyTrackBar.Value = 128;
      this.transparencyTrackBar.Scroll += new System.EventHandler(this.transparencyTrackBar_Scroll);
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(187, 114);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.contrastUpDown);
      this.groupBox.Controls.Add(this.transparencyUpDown);
      this.groupBox.Controls.Add(this.contrastTrackBar);
      this.groupBox.Controls.Add(this.transparencyTrackBar);
      this.groupBox.Controls.Add(this.label2);
      this.groupBox.Controls.Add(this.label1);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(334, 92);
      this.groupBox.TabIndex = 13;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "魔术效果调节";
      // 
      // contrastTrackBar
      // 
      this.contrastTrackBar.AutoSize = false;
      this.contrastTrackBar.Location = new System.Drawing.Point(60, 56);
      this.contrastTrackBar.Maximum = 100;
      this.contrastTrackBar.Minimum = -100;
      this.contrastTrackBar.Name = "contrastTrackBar";
      this.contrastTrackBar.Size = new System.Drawing.Size(200, 26);
      this.contrastTrackBar.TabIndex = 5;
      this.contrastTrackBar.TickFrequency = 0;
      this.contrastTrackBar.Scroll += new System.EventHandler(this.contrastTrackBar_Scroll);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 61);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(47, 12);
      this.label2.TabIndex = 5;
      this.label2.Text = "对比度:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(47, 12);
      this.label1.TabIndex = 5;
      this.label1.Text = "透明度:";
      // 
      // MagicDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(358, 151);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(366, 178);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(366, 178);
      this.Name = "MagicDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "魔术图";
      this.Load += new System.EventHandler(this.MagicDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.contrastUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.transparencyUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.transparencyTrackBar)).EndInit();
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.NumericUpDown contrastUpDown;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.NumericUpDown transparencyUpDown;
    private System.Windows.Forms.TrackBar transparencyTrackBar;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.TrackBar contrastTrackBar;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
  }
}