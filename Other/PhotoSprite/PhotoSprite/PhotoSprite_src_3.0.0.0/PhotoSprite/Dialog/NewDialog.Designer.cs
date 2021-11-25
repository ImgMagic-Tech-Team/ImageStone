namespace PhotoSprite.Dialog
{
  partial class NewDialog
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
      this.label2 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.heightUpDown);
      this.groupBox.Controls.Add(this.widthUpDown);
      this.groupBox.Controls.Add(this.label2);
      this.groupBox.Controls.Add(this.label4);
      this.groupBox.Controls.Add(this.label3);
      this.groupBox.Controls.Add(this.label1);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(152, 80);
      this.groupBox.TabIndex = 10;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "文件尺寸: 1.8 MB";
      // 
      // heightUpDown
      // 
      this.heightUpDown.Location = new System.Drawing.Point(35, 47);
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
      this.widthUpDown.Location = new System.Drawing.Point(35, 20);
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
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(23, 12);
      this.label2.TabIndex = 0;
      this.label2.Text = "高:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(115, 56);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(29, 12);
      this.label4.TabIndex = 0;
      this.label4.Text = "像素";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(115, 29);
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
      this.label1.Size = new System.Drawing.Size(23, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "宽:";
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
      // 
      // NewDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(172, 141);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(180, 168);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(180, 168);
      this.Name = "NewDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "新建";
      this.Load += new System.EventHandler(this.NewDialog_Load);
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.NumericUpDown heightUpDown;
    private System.Windows.Forms.NumericUpDown widthUpDown;
  }
}