namespace PhotoSprite
{
  partial class ColorPickerDialog
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.luminancePictureBox = new System.Windows.Forms.PictureBox();
      this.saturationPictureBox = new System.Windows.Forms.PictureBox();
      this.luminanceUpDown = new System.Windows.Forms.NumericUpDown();
      this.saturationUpDown = new System.Windows.Forms.NumericUpDown();
      this.hueUpDown = new System.Windows.Forms.NumericUpDown();
      this.huePictureBox = new System.Windows.Forms.PictureBox();
      this.label3 = new System.Windows.Forms.Label();
      this.luminanceTrackBar = new System.Windows.Forms.TrackBar();
      this.saturationTrackBar = new System.Windows.Forms.TrackBar();
      this.hueTrackBar = new System.Windows.Forms.TrackBar();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.colorPreviewLabel = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.transparencyLabel = new System.Windows.Forms.Label();
      this.blueUpDown = new System.Windows.Forms.NumericUpDown();
      this.greenUpDown = new System.Windows.Forms.NumericUpDown();
      this.redUpDown = new System.Windows.Forms.NumericUpDown();
      this.label12 = new System.Windows.Forms.Label();
      this.rgbHexTextBox = new System.Windows.Forms.TextBox();
      this.label17 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.luminancePictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.saturationPictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.luminanceUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.saturationUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.hueUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.huePictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.luminanceTrackBar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.saturationTrackBar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.hueTrackBar)).BeginInit();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.blueUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.greenUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.redUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.luminancePictureBox);
      this.groupBox1.Controls.Add(this.saturationPictureBox);
      this.groupBox1.Controls.Add(this.luminanceUpDown);
      this.groupBox1.Controls.Add(this.saturationUpDown);
      this.groupBox1.Controls.Add(this.hueUpDown);
      this.groupBox1.Controls.Add(this.huePictureBox);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.luminanceTrackBar);
      this.groupBox1.Controls.Add(this.saturationTrackBar);
      this.groupBox1.Controls.Add(this.hueTrackBar);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(211, 258);
      this.groupBox1.TabIndex = 101;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "HSL";
      // 
      // luminancePictureBox
      // 
      this.luminancePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.luminancePictureBox.Location = new System.Drawing.Point(171, 30);
      this.luminancePictureBox.Name = "luminancePictureBox";
      this.luminancePictureBox.Size = new System.Drawing.Size(24, 180);
      this.luminancePictureBox.TabIndex = 7;
      this.luminancePictureBox.TabStop = false;
      // 
      // saturationPictureBox
      // 
      this.saturationPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.saturationPictureBox.Location = new System.Drawing.Point(105, 30);
      this.saturationPictureBox.Name = "saturationPictureBox";
      this.saturationPictureBox.Size = new System.Drawing.Size(24, 180);
      this.saturationPictureBox.TabIndex = 6;
      this.saturationPictureBox.TabStop = false;
      // 
      // luminanceUpDown
      // 
      this.luminanceUpDown.Location = new System.Drawing.Point(156, 226);
      this.luminanceUpDown.Name = "luminanceUpDown";
      this.luminanceUpDown.Size = new System.Drawing.Size(41, 21);
      this.luminanceUpDown.TabIndex = 8;
      this.luminanceUpDown.ValueChanged += new System.EventHandler(this.luminanceUpDown_ValueChanged);
      // 
      // saturationUpDown
      // 
      this.saturationUpDown.Location = new System.Drawing.Point(90, 226);
      this.saturationUpDown.Name = "saturationUpDown";
      this.saturationUpDown.Size = new System.Drawing.Size(41, 21);
      this.saturationUpDown.TabIndex = 7;
      this.saturationUpDown.ValueChanged += new System.EventHandler(this.saturationUpDown_ValueChanged);
      // 
      // hueUpDown
      // 
      this.hueUpDown.Location = new System.Drawing.Point(24, 226);
      this.hueUpDown.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
      this.hueUpDown.Name = "hueUpDown";
      this.hueUpDown.Size = new System.Drawing.Size(41, 21);
      this.hueUpDown.TabIndex = 6;
      this.hueUpDown.ValueChanged += new System.EventHandler(this.hueUpDown_ValueChanged);
      // 
      // huePictureBox
      // 
      this.huePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.huePictureBox.Location = new System.Drawing.Point(39, 30);
      this.huePictureBox.Name = "huePictureBox";
      this.huePictureBox.Size = new System.Drawing.Size(24, 180);
      this.huePictureBox.TabIndex = 5;
      this.huePictureBox.TabStop = false;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(137, 230);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(17, 12);
      this.label3.TabIndex = 0;
      this.label3.Text = "L:";
      // 
      // luminanceTrackBar
      // 
      this.luminanceTrackBar.AutoSize = false;
      this.luminanceTrackBar.Location = new System.Drawing.Point(143, 17);
      this.luminanceTrackBar.Maximum = 100;
      this.luminanceTrackBar.Name = "luminanceTrackBar";
      this.luminanceTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
      this.luminanceTrackBar.Size = new System.Drawing.Size(25, 206);
      this.luminanceTrackBar.TabIndex = 5;
      this.luminanceTrackBar.TickFrequency = 0;
      this.luminanceTrackBar.Scroll += new System.EventHandler(this.luminanceTrackBar_Scroll);
      // 
      // saturationTrackBar
      // 
      this.saturationTrackBar.AutoSize = false;
      this.saturationTrackBar.Location = new System.Drawing.Point(77, 17);
      this.saturationTrackBar.Maximum = 100;
      this.saturationTrackBar.Name = "saturationTrackBar";
      this.saturationTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
      this.saturationTrackBar.Size = new System.Drawing.Size(25, 206);
      this.saturationTrackBar.TabIndex = 4;
      this.saturationTrackBar.TickFrequency = 0;
      this.saturationTrackBar.Scroll += new System.EventHandler(this.saturationTrackBar_Scroll);
      // 
      // hueTrackBar
      // 
      this.hueTrackBar.AutoSize = false;
      this.hueTrackBar.Location = new System.Drawing.Point(11, 17);
      this.hueTrackBar.Maximum = 359;
      this.hueTrackBar.Name = "hueTrackBar";
      this.hueTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
      this.hueTrackBar.Size = new System.Drawing.Size(25, 206);
      this.hueTrackBar.TabIndex = 3;
      this.hueTrackBar.TickFrequency = 0;
      this.hueTrackBar.Scroll += new System.EventHandler(this.hueTrackBar_Scroll);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(5, 230);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(17, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "H:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(71, 230);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(17, 12);
      this.label2.TabIndex = 0;
      this.label2.Text = "S:";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.colorPreviewLabel);
      this.groupBox2.Location = new System.Drawing.Point(229, 12);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(115, 127);
      this.groupBox2.TabIndex = 102;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "颜色预览";
      // 
      // colorPreviewLabel
      // 
      this.colorPreviewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.colorPreviewLabel.Location = new System.Drawing.Point(6, 17);
      this.colorPreviewLabel.Name = "colorPreviewLabel";
      this.colorPreviewLabel.Size = new System.Drawing.Size(100, 100);
      this.colorPreviewLabel.TabIndex = 8;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.transparencyLabel);
      this.groupBox3.Controls.Add(this.blueUpDown);
      this.groupBox3.Controls.Add(this.greenUpDown);
      this.groupBox3.Controls.Add(this.redUpDown);
      this.groupBox3.Controls.Add(this.label12);
      this.groupBox3.Controls.Add(this.rgbHexTextBox);
      this.groupBox3.Controls.Add(this.label17);
      this.groupBox3.Controls.Add(this.label11);
      this.groupBox3.Controls.Add(this.label10);
      this.groupBox3.Location = new System.Drawing.Point(12, 276);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(332, 52);
      this.groupBox3.TabIndex = 103;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "RGB";
      // 
      // transparencyLabel
      // 
      this.transparencyLabel.BackColor = System.Drawing.Color.Red;
      this.transparencyLabel.Location = new System.Drawing.Point(305, 22);
      this.transparencyLabel.Name = "transparencyLabel";
      this.transparencyLabel.Size = new System.Drawing.Size(16, 16);
      this.transparencyLabel.TabIndex = 100;
      this.transparencyLabel.Click += new System.EventHandler(this.transparencyLabel_Click);
      // 
      // blueUpDown
      // 
      this.blueUpDown.Location = new System.Drawing.Point(156, 20);
      this.blueUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.blueUpDown.Name = "blueUpDown";
      this.blueUpDown.Size = new System.Drawing.Size(41, 21);
      this.blueUpDown.TabIndex = 11;
      this.blueUpDown.ValueChanged += new System.EventHandler(this.blueUpDown_ValueChanged);
      // 
      // greenUpDown
      // 
      this.greenUpDown.Location = new System.Drawing.Point(90, 20);
      this.greenUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.greenUpDown.Name = "greenUpDown";
      this.greenUpDown.Size = new System.Drawing.Size(41, 21);
      this.greenUpDown.TabIndex = 10;
      this.greenUpDown.ValueChanged += new System.EventHandler(this.greenUpDown_ValueChanged);
      // 
      // redUpDown
      // 
      this.redUpDown.Location = new System.Drawing.Point(24, 20);
      this.redUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.redUpDown.Name = "redUpDown";
      this.redUpDown.Size = new System.Drawing.Size(41, 21);
      this.redUpDown.TabIndex = 9;
      this.redUpDown.ValueChanged += new System.EventHandler(this.redUpDown_ValueChanged);
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(137, 24);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(17, 12);
      this.label12.TabIndex = 0;
      this.label12.Text = "B:";
      // 
      // rgbHexTextBox
      // 
      this.rgbHexTextBox.Location = new System.Drawing.Point(225, 20);
      this.rgbHexTextBox.Name = "rgbHexTextBox";
      this.rgbHexTextBox.Size = new System.Drawing.Size(72, 21);
      this.rgbHexTextBox.TabIndex = 19;
      this.rgbHexTextBox.TextChanged += new System.EventHandler(this.rgbHexTextBox_TextChanged);
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Location = new System.Drawing.Point(212, 24);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(11, 12);
      this.label17.TabIndex = 0;
      this.label17.Text = "#";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(71, 24);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(17, 12);
      this.label11.TabIndex = 0;
      this.label11.Text = "G:";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(5, 24);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(17, 12);
      this.label10.TabIndex = 0;
      this.label10.Text = "R:";
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(168, 334);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(102, 334);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // ColorPickerDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(354, 368);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(362, 395);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(362, 395);
      this.Name = "ColorPickerDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "颜色拾取器";
      this.Load += new System.EventHandler(this.ColorPickerDialog_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.luminancePictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.saturationPictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.luminanceUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.saturationUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.hueUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.huePictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.luminanceTrackBar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.saturationTrackBar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.hueTrackBar)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.blueUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.greenUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.redUpDown)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.PictureBox luminancePictureBox;
    private System.Windows.Forms.PictureBox saturationPictureBox;
    private System.Windows.Forms.PictureBox huePictureBox;
    private System.Windows.Forms.TrackBar luminanceTrackBar;
    private System.Windows.Forms.TrackBar saturationTrackBar;
    private System.Windows.Forms.TrackBar hueTrackBar;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label colorPreviewLabel;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox rgbHexTextBox;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.NumericUpDown redUpDown;
    private System.Windows.Forms.NumericUpDown blueUpDown;
    private System.Windows.Forms.NumericUpDown greenUpDown;
    private System.Windows.Forms.NumericUpDown luminanceUpDown;
    private System.Windows.Forms.NumericUpDown saturationUpDown;
    private System.Windows.Forms.NumericUpDown hueUpDown;
    private System.Windows.Forms.Label transparencyLabel;

  }
}