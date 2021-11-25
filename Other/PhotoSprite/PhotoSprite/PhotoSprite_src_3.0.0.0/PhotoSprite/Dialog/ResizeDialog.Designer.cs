namespace PhotoSprite.Dialog
{
  partial class ResizeDialog
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
      this.heightUpDown = new System.Windows.Forms.NumericUpDown();
      this.widthUpDown = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.constrainCheckBox = new System.Windows.Forms.CheckBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.heightUpDown);
      this.groupBox.Controls.Add(this.widthUpDown);
      this.groupBox.Controls.Add(this.label3);
      this.groupBox.Controls.Add(this.label4);
      this.groupBox.Controls.Add(this.label5);
      this.groupBox.Controls.Add(this.label6);
      this.groupBox.Controls.Add(this.constrainCheckBox);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(152, 110);
      this.groupBox.TabIndex = 0;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "Í¼Ïñ³ß´çµ÷½Ú";
      // 
      // heightUpDown
      // 
      this.heightUpDown.Location = new System.Drawing.Point(38, 47);
      this.heightUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
      this.heightUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.heightUpDown.Name = "heightUpDown";
      this.heightUpDown.Size = new System.Drawing.Size(74, 21);
      this.heightUpDown.TabIndex = 4;
      this.heightUpDown.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
      this.heightUpDown.ValueChanged += new System.EventHandler(this.heightUpDown_ValueChanged);
      // 
      // widthUpDown
      // 
      this.widthUpDown.Location = new System.Drawing.Point(38, 20);
      this.widthUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
      this.widthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.widthUpDown.Name = "widthUpDown";
      this.widthUpDown.Size = new System.Drawing.Size(74, 21);
      this.widthUpDown.TabIndex = 3;
      this.widthUpDown.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
      this.widthUpDown.ValueChanged += new System.EventHandler(this.widthUpDown_ValueChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(9, 56);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(23, 12);
      this.label3.TabIndex = 8;
      this.label3.Text = "¸ß:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(118, 56);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(29, 12);
      this.label4.TabIndex = 5;
      this.label4.Text = "ÏñËØ";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(118, 29);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(29, 12);
      this.label5.TabIndex = 6;
      this.label5.Text = "ÏñËØ";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(9, 29);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(23, 12);
      this.label6.TabIndex = 7;
      this.label6.Text = "¿í:";
      // 
      // constrainCheckBox
      // 
      this.constrainCheckBox.AutoSize = true;
      this.constrainCheckBox.Checked = true;
      this.constrainCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.constrainCheckBox.Location = new System.Drawing.Point(11, 84);
      this.constrainCheckBox.Name = "constrainCheckBox";
      this.constrainCheckBox.Size = new System.Drawing.Size(84, 16);
      this.constrainCheckBox.TabIndex = 5;
      this.constrainCheckBox.Text = "Ô¼Êø×Ýºá±È";
      this.constrainCheckBox.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(179, 56);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "È¡Ïû";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(179, 17);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "È·¶¨";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // ResizeDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(240, 133);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(248, 160);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(248, 160);
      this.Name = "ResizeDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "³ß´ç";
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.CheckBox constrainCheckBox;
    private System.Windows.Forms.NumericUpDown heightUpDown;
    private System.Windows.Forms.NumericUpDown widthUpDown;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
  }
}