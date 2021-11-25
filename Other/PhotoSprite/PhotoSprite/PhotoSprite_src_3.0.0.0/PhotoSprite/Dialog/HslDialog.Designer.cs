namespace PhotoSprite.Dialog
{
  partial class HslDialog
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
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.lTrackBar = new System.Windows.Forms.TrackBar();
      this.btnCancel = new System.Windows.Forms.Button();
      this.sTrackBar = new System.Windows.Forms.TrackBar();
      this.btnOk = new System.Windows.Forms.Button();
      this.hTrackBar = new System.Windows.Forms.TrackBar();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.lUpDown = new System.Windows.Forms.NumericUpDown();
      this.sUpDown = new System.Windows.Forms.NumericUpDown();
      this.hUpDown = new System.Windows.Forms.NumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.lTrackBar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.sTrackBar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.hTrackBar)).BeginInit();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.lUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.sUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.hUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 61);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(17, 12);
      this.label2.TabIndex = 5;
      this.label2.Text = "S:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(17, 12);
      this.label1.TabIndex = 5;
      this.label1.Text = "H:";
      // 
      // lTrackBar
      // 
      this.lTrackBar.AutoSize = false;
      this.lTrackBar.Location = new System.Drawing.Point(27, 88);
      this.lTrackBar.Maximum = 100;
      this.lTrackBar.Minimum = -100;
      this.lTrackBar.Name = "lTrackBar";
      this.lTrackBar.Size = new System.Drawing.Size(200, 26);
      this.lTrackBar.TabIndex = 7;
      this.lTrackBar.TickFrequency = 0;
      this.lTrackBar.Scroll += new System.EventHandler(this.lTrackBar_Scroll);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(215, 140);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // sTrackBar
      // 
      this.sTrackBar.AutoSize = false;
      this.sTrackBar.Location = new System.Drawing.Point(27, 56);
      this.sTrackBar.Maximum = 100;
      this.sTrackBar.Minimum = -100;
      this.sTrackBar.Name = "sTrackBar";
      this.sTrackBar.Size = new System.Drawing.Size(200, 26);
      this.sTrackBar.TabIndex = 5;
      this.sTrackBar.TickFrequency = 0;
      this.sTrackBar.Scroll += new System.EventHandler(this.sTrackBar_Scroll);
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(149, 140);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // hTrackBar
      // 
      this.hTrackBar.AutoSize = false;
      this.hTrackBar.Location = new System.Drawing.Point(27, 24);
      this.hTrackBar.Maximum = 180;
      this.hTrackBar.Minimum = -180;
      this.hTrackBar.Name = "hTrackBar";
      this.hTrackBar.Size = new System.Drawing.Size(200, 26);
      this.hTrackBar.TabIndex = 3;
      this.hTrackBar.TickFrequency = 0;
      this.hTrackBar.Scroll += new System.EventHandler(this.hTrackBar_Scroll);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 93);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(17, 12);
      this.label3.TabIndex = 5;
      this.label3.Text = "L:";
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.lUpDown);
      this.groupBox.Controls.Add(this.sUpDown);
      this.groupBox.Controls.Add(this.hUpDown);
      this.groupBox.Controls.Add(this.lTrackBar);
      this.groupBox.Controls.Add(this.sTrackBar);
      this.groupBox.Controls.Add(this.hTrackBar);
      this.groupBox.Controls.Add(this.label3);
      this.groupBox.Controls.Add(this.label2);
      this.groupBox.Controls.Add(this.label1);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(290, 122);
      this.groupBox.TabIndex = 10;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "HSL 分量调节";
      // 
      // lUpDown
      // 
      this.lUpDown.Location = new System.Drawing.Point(230, 88);
      this.lUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
      this.lUpDown.Name = "lUpDown";
      this.lUpDown.Size = new System.Drawing.Size(50, 21);
      this.lUpDown.TabIndex = 8;
      this.lUpDown.ValueChanged += new System.EventHandler(this.lUpDown_ValueChanged);
      // 
      // sUpDown
      // 
      this.sUpDown.Location = new System.Drawing.Point(230, 56);
      this.sUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
      this.sUpDown.Name = "sUpDown";
      this.sUpDown.Size = new System.Drawing.Size(50, 21);
      this.sUpDown.TabIndex = 6;
      this.sUpDown.ValueChanged += new System.EventHandler(this.sUpDown_ValueChanged);
      // 
      // hUpDown
      // 
      this.hUpDown.Location = new System.Drawing.Point(230, 24);
      this.hUpDown.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
      this.hUpDown.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
      this.hUpDown.Name = "hUpDown";
      this.hUpDown.Size = new System.Drawing.Size(50, 21);
      this.hUpDown.TabIndex = 4;
      this.hUpDown.ValueChanged += new System.EventHandler(this.hUpDown_ValueChanged);
      // 
      // HslDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(314, 173);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(322, 200);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(322, 200);
      this.Name = "HslDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "色调/饱和度";
      ((System.ComponentModel.ISupportInitialize)(this.lTrackBar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.sTrackBar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.hTrackBar)).EndInit();
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.lUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.sUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.hUpDown)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TrackBar lTrackBar;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TrackBar sTrackBar;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.TrackBar hTrackBar;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.NumericUpDown hUpDown;
    private System.Windows.Forms.NumericUpDown lUpDown;
    private System.Windows.Forms.NumericUpDown sUpDown;
  }
}