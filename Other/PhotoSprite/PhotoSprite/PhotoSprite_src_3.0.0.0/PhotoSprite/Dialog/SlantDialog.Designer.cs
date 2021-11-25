namespace PhotoSprite.Dialog
{
  partial class SlantDialog
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
      this.horzUpDown = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.vertUpDown = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.horzUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.vertUpDown)).BeginInit();
      this.groupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // horzUpDown
      // 
      this.horzUpDown.Location = new System.Drawing.Point(44, 20);
      this.horzUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.horzUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
      this.horzUpDown.Name = "horzUpDown";
      this.horzUpDown.Size = new System.Drawing.Size(74, 21);
      this.horzUpDown.TabIndex = 3;
      this.horzUpDown.ValueChanged += new System.EventHandler(this.horzUpDown_ValueChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 12);
      this.label2.TabIndex = 0;
      this.label2.Text = "垂直:";
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(56, 106);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(113, 106);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // vertUpDown
      // 
      this.vertUpDown.Location = new System.Drawing.Point(44, 47);
      this.vertUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.vertUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
      this.vertUpDown.Name = "vertUpDown";
      this.vertUpDown.Size = new System.Drawing.Size(74, 21);
      this.vertUpDown.TabIndex = 4;
      this.vertUpDown.ValueChanged += new System.EventHandler(this.vertUpDown_ValueChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(124, 56);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(29, 12);
      this.label4.TabIndex = 0;
      this.label4.Text = "像素";
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.vertUpDown);
      this.groupBox.Controls.Add(this.horzUpDown);
      this.groupBox.Controls.Add(this.label2);
      this.groupBox.Controls.Add(this.label4);
      this.groupBox.Controls.Add(this.label3);
      this.groupBox.Controls.Add(this.label1);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(160, 80);
      this.groupBox.TabIndex = 13;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "倾斜量调节";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(124, 29);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(29, 12);
      this.label3.TabIndex = 0;
      this.label3.Text = "像素";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "水平:";
      // 
      // SlantDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(182, 139);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.groupBox);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(190, 166);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(190, 166);
      this.Name = "SlantDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "倾斜";
      ((System.ComponentModel.ISupportInitialize)(this.horzUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.vertUpDown)).EndInit();
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.NumericUpDown horzUpDown;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.NumericUpDown vertUpDown;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
  }
}