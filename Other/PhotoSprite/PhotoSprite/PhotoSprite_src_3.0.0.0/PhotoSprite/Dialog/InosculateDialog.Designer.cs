namespace PhotoSprite.Dialog
{
  partial class InosculateDialog
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
      this.transparencyUpDown = new System.Windows.Forms.NumericUpDown();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.transparencyTrackBar = new System.Windows.Forms.TrackBar();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.transparencyUpDown)).BeginInit();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.transparencyTrackBar)).BeginInit();
      this.SuspendLayout();
      // 
      // transparencyUpDown
      // 
      this.transparencyUpDown.Location = new System.Drawing.Point(212, 22);
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
      // groupBox
      // 
      this.groupBox.Controls.Add(this.transparencyUpDown);
      this.groupBox.Controls.Add(this.transparencyTrackBar);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(268, 53);
      this.groupBox.TabIndex = 8;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "透明度调节";
      // 
      // transparencyTrackBar
      // 
      this.transparencyTrackBar.AutoSize = false;
      this.transparencyTrackBar.Location = new System.Drawing.Point(6, 20);
      this.transparencyTrackBar.Maximum = 255;
      this.transparencyTrackBar.Name = "transparencyTrackBar";
      this.transparencyTrackBar.Size = new System.Drawing.Size(200, 26);
      this.transparencyTrackBar.TabIndex = 3;
      this.transparencyTrackBar.TickFrequency = 0;
      this.transparencyTrackBar.Value = 128;
      this.transparencyTrackBar.Scroll += new System.EventHandler(this.transparencyTrackBar_Scroll);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(215, 74);
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
      this.btnOk.Location = new System.Drawing.Point(149, 74);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // InosculateDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 109);
      this.Controls.Add(this.groupBox);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(300, 136);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(300, 136);
      this.Name = "InosculateDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "图像融合";
      this.Load += new System.EventHandler(this.InosculateDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.transparencyUpDown)).EndInit();
      this.groupBox.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.transparencyTrackBar)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.NumericUpDown transparencyUpDown;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.TrackBar transparencyTrackBar;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
  }
}