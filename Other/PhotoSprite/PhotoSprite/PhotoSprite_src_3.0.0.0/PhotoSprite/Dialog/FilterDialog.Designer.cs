namespace PhotoSprite.Dialog
{
  partial class FilterDialog
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
      this.nTrackBar = new System.Windows.Forms.TrackBar();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.nUpDown = new System.Windows.Forms.NumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.nTrackBar)).BeginInit();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // nTrackBar
      // 
      this.nTrackBar.AutoSize = false;
      this.nTrackBar.Location = new System.Drawing.Point(6, 20);
      this.nTrackBar.Maximum = 25;
      this.nTrackBar.Minimum = 3;
      this.nTrackBar.Name = "nTrackBar";
      this.nTrackBar.Size = new System.Drawing.Size(200, 26);
      this.nTrackBar.TabIndex = 3;
      this.nTrackBar.TickFrequency = 0;
      this.nTrackBar.Value = 5;
      this.nTrackBar.Scroll += new System.EventHandler(this.nTrackBar_Scroll);
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
      // groupBox
      // 
      this.groupBox.Controls.Add(this.nUpDown);
      this.groupBox.Controls.Add(this.nTrackBar);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(268, 53);
      this.groupBox.TabIndex = 14;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "窗口大小(奇数)";
      // 
      // nUpDown
      // 
      this.nUpDown.Location = new System.Drawing.Point(210, 22);
      this.nUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
      this.nUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
      this.nUpDown.Name = "nUpDown";
      this.nUpDown.Size = new System.Drawing.Size(50, 21);
      this.nUpDown.TabIndex = 4;
      this.nUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.nUpDown.ValueChanged += new System.EventHandler(this.nUpDown_ValueChanged);
      // 
      // FilterDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 111);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(300, 138);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(300, 138);
      this.Name = "FilterDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "滤波器";
      this.Load += new System.EventHandler(this.FilterDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nTrackBar)).EndInit();
      this.groupBox.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nUpDown)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TrackBar nTrackBar;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.NumericUpDown nUpDown;
  }
}