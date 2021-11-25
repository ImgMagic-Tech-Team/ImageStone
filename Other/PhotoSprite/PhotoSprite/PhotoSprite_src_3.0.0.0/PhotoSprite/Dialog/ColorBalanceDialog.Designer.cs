namespace PhotoSprite.Dialog
{
  partial class ColorBalanceDialog
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
      this.blueUpDown = new System.Windows.Forms.NumericUpDown();
      this.greenUpDown = new System.Windows.Forms.NumericUpDown();
      this.redUpDown = new System.Windows.Forms.NumericUpDown();
      this.blueTrackBar = new System.Windows.Forms.TrackBar();
      this.greenTrackBar = new System.Windows.Forms.TrackBar();
      this.redTrackBar = new System.Windows.Forms.TrackBar();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.blueUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.greenUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.redUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.blueTrackBar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.greenTrackBar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.redTrackBar)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.blueUpDown);
      this.groupBox.Controls.Add(this.greenUpDown);
      this.groupBox.Controls.Add(this.redUpDown);
      this.groupBox.Controls.Add(this.blueTrackBar);
      this.groupBox.Controls.Add(this.greenTrackBar);
      this.groupBox.Controls.Add(this.redTrackBar);
      this.groupBox.Controls.Add(this.label3);
      this.groupBox.Controls.Add(this.label2);
      this.groupBox.Controls.Add(this.label1);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(294, 122);
      this.groupBox.TabIndex = 11;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "色彩分量调节";
      // 
      // blueUpDown
      // 
      this.blueUpDown.Location = new System.Drawing.Point(233, 91);
      this.blueUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.blueUpDown.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
      this.blueUpDown.Name = "blueUpDown";
      this.blueUpDown.Size = new System.Drawing.Size(50, 21);
      this.blueUpDown.TabIndex = 8;
      this.blueUpDown.ValueChanged += new System.EventHandler(this.blueUpDown_ValueChanged);
      // 
      // greenUpDown
      // 
      this.greenUpDown.Location = new System.Drawing.Point(233, 59);
      this.greenUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.greenUpDown.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
      this.greenUpDown.Name = "greenUpDown";
      this.greenUpDown.Size = new System.Drawing.Size(50, 21);
      this.greenUpDown.TabIndex = 6;
      this.greenUpDown.ValueChanged += new System.EventHandler(this.greenUpDown_ValueChanged);
      // 
      // redUpDown
      // 
      this.redUpDown.Location = new System.Drawing.Point(233, 27);
      this.redUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.redUpDown.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
      this.redUpDown.Name = "redUpDown";
      this.redUpDown.Size = new System.Drawing.Size(50, 21);
      this.redUpDown.TabIndex = 4;
      this.redUpDown.ValueChanged += new System.EventHandler(this.redUpDown_ValueChanged);
      // 
      // blueTrackBar
      // 
      this.blueTrackBar.AutoSize = false;
      this.blueTrackBar.Location = new System.Drawing.Point(27, 88);
      this.blueTrackBar.Maximum = 255;
      this.blueTrackBar.Minimum = -255;
      this.blueTrackBar.Name = "blueTrackBar";
      this.blueTrackBar.Size = new System.Drawing.Size(200, 26);
      this.blueTrackBar.TabIndex = 7;
      this.blueTrackBar.TickFrequency = 0;
      this.blueTrackBar.Scroll += new System.EventHandler(this.blueTrackBar_Scroll);
      // 
      // greenTrackBar
      // 
      this.greenTrackBar.AutoSize = false;
      this.greenTrackBar.Location = new System.Drawing.Point(27, 56);
      this.greenTrackBar.Maximum = 255;
      this.greenTrackBar.Minimum = -255;
      this.greenTrackBar.Name = "greenTrackBar";
      this.greenTrackBar.Size = new System.Drawing.Size(200, 26);
      this.greenTrackBar.TabIndex = 5;
      this.greenTrackBar.TickFrequency = 0;
      this.greenTrackBar.Scroll += new System.EventHandler(this.greenTrackBar_Scroll);
      // 
      // redTrackBar
      // 
      this.redTrackBar.AutoSize = false;
      this.redTrackBar.Location = new System.Drawing.Point(27, 24);
      this.redTrackBar.Maximum = 255;
      this.redTrackBar.Minimum = -255;
      this.redTrackBar.Name = "redTrackBar";
      this.redTrackBar.Size = new System.Drawing.Size(200, 26);
      this.redTrackBar.TabIndex = 3;
      this.redTrackBar.TickFrequency = 0;
      this.redTrackBar.Scroll += new System.EventHandler(this.redTrackBar_Scroll);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 93);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(23, 12);
      this.label3.TabIndex = 5;
      this.label3.Text = "蓝:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 61);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(23, 12);
      this.label2.TabIndex = 5;
      this.label2.Text = "绿:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(23, 12);
      this.label1.TabIndex = 5;
      this.label1.Text = "红:";
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(201, 140);
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
      this.btnOk.Location = new System.Drawing.Point(135, 140);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // ColorBalanceDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(318, 173);
      this.Controls.Add(this.groupBox);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(326, 200);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(326, 200);
      this.Name = "ColorBalanceDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "色彩调整";
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.blueUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.greenUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.redUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.blueTrackBar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.greenTrackBar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.redTrackBar)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.TrackBar redTrackBar;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TrackBar blueTrackBar;
    private System.Windows.Forms.TrackBar greenTrackBar;
    private System.Windows.Forms.NumericUpDown blueUpDown;
    private System.Windows.Forms.NumericUpDown greenUpDown;
    private System.Windows.Forms.NumericUpDown redUpDown;
  }
}