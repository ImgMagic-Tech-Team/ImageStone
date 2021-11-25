namespace PhotoSprite.Dialog
{
  partial class TranslationDialog
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
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.vertUpDown = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.horzUpDown = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.rad3 = new System.Windows.Forms.RadioButton();
      this.rad2 = new System.Windows.Forms.RadioButton();
      this.rad1 = new System.Windows.Forms.RadioButton();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.vertUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.horzUpDown)).BeginInit();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 12);
      this.label2.TabIndex = 5;
      this.label2.Text = "垂直:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 12);
      this.label1.TabIndex = 5;
      this.label1.Text = "水平:";
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(180, 103);
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
      this.btnOk.Location = new System.Drawing.Point(114, 103);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.vertUpDown);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.horzUpDown);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(162, 85);
      this.groupBox1.TabIndex = 10;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "偏移量";
      // 
      // vertUpDown
      // 
      this.vertUpDown.Location = new System.Drawing.Point(47, 53);
      this.vertUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.vertUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
      this.vertUpDown.Name = "vertUpDown";
      this.vertUpDown.Size = new System.Drawing.Size(74, 21);
      this.vertUpDown.TabIndex = 4;
      this.vertUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.vertUpDown.ValueChanged += new System.EventHandler(this.vertUpDown_ValueChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(127, 55);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(29, 12);
      this.label4.TabIndex = 9;
      this.label4.Text = "像素";
      // 
      // horzUpDown
      // 
      this.horzUpDown.Location = new System.Drawing.Point(47, 20);
      this.horzUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.horzUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
      this.horzUpDown.Name = "horzUpDown";
      this.horzUpDown.Size = new System.Drawing.Size(74, 21);
      this.horzUpDown.TabIndex = 3;
      this.horzUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.horzUpDown.ValueChanged += new System.EventHandler(this.horzUpDown_ValueChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(127, 22);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(29, 12);
      this.label3.TabIndex = 7;
      this.label3.Text = "像素";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rad3);
      this.groupBox2.Controls.Add(this.rad2);
      this.groupBox2.Controls.Add(this.rad1);
      this.groupBox2.Location = new System.Drawing.Point(180, 12);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(114, 85);
      this.groupBox2.TabIndex = 11;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "未知区域";
      // 
      // rad3
      // 
      this.rad3.AutoSize = true;
      this.rad3.Checked = true;
      this.rad3.Location = new System.Drawing.Point(8, 62);
      this.rad3.Name = "rad3";
      this.rad3.Size = new System.Drawing.Size(71, 16);
      this.rad3.TabIndex = 7;
      this.rad3.TabStop = true;
      this.rad3.Text = "四周环绕";
      this.rad3.UseVisualStyleBackColor = true;
      this.rad3.CheckedChanged += new System.EventHandler(this.rad3_CheckedChanged);
      // 
      // rad2
      // 
      this.rad2.AutoSize = true;
      this.rad2.Location = new System.Drawing.Point(8, 40);
      this.rad2.Name = "rad2";
      this.rad2.Size = new System.Drawing.Size(95, 16);
      this.rad2.TabIndex = 6;
      this.rad2.Text = "重复边缘像素";
      this.rad2.UseVisualStyleBackColor = true;
      this.rad2.CheckedChanged += new System.EventHandler(this.rad2_CheckedChanged);
      // 
      // rad1
      // 
      this.rad1.AutoSize = true;
      this.rad1.Location = new System.Drawing.Point(8, 18);
      this.rad1.Name = "rad1";
      this.rad1.Size = new System.Drawing.Size(47, 16);
      this.rad1.TabIndex = 5;
      this.rad1.Text = "透明";
      this.rad1.UseVisualStyleBackColor = true;
      this.rad1.CheckedChanged += new System.EventHandler(this.rad1_CheckedChanged);
      // 
      // TranslationDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(306, 135);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox1);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(314, 162);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(314, 162);
      this.Name = "TranslationDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "平移";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.vertUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.horzUpDown)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton rad1;
    private System.Windows.Forms.RadioButton rad3;
    private System.Windows.Forms.RadioButton rad2;
    private System.Windows.Forms.NumericUpDown horzUpDown;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown vertUpDown;
    private System.Windows.Forms.Label label4;
  }
}