namespace PhotoSprite.Dialog
{
  partial class ArtStringDialog
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
      this.blockWidthUpDown = new System.Windows.Forms.NumericUpDown();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.textTextBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.blockHeightUpDown = new System.Windows.Forms.NumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.blockWidthUpDown)).BeginInit();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.blockHeightUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // blockWidthUpDown
      // 
      this.blockWidthUpDown.Location = new System.Drawing.Point(47, 50);
      this.blockWidthUpDown.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
      this.blockWidthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.blockWidthUpDown.Name = "blockWidthUpDown";
      this.blockWidthUpDown.Size = new System.Drawing.Size(50, 21);
      this.blockWidthUpDown.TabIndex = 4;
      this.blockWidthUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.textTextBox);
      this.groupBox.Controls.Add(this.label3);
      this.groupBox.Controls.Add(this.label2);
      this.groupBox.Controls.Add(this.label1);
      this.groupBox.Controls.Add(this.blockHeightUpDown);
      this.groupBox.Controls.Add(this.blockWidthUpDown);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(268, 83);
      this.groupBox.TabIndex = 10;
      this.groupBox.TabStop = false;
      // 
      // textTextBox
      // 
      this.textTextBox.Location = new System.Drawing.Point(47, 17);
      this.textTextBox.Name = "textTextBox";
      this.textTextBox.Size = new System.Drawing.Size(215, 21);
      this.textTextBox.TabIndex = 3;
      this.textTextBox.Text = "$@o*+:., ";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(121, 54);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(35, 12);
      this.label3.TabIndex = 9;
      this.label3.Text = "块高:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 54);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 12);
      this.label2.TabIndex = 9;
      this.label2.Text = "块宽:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 12);
      this.label1.TabIndex = 9;
      this.label1.Text = "文字:";
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(155, 101);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(89, 101);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // blockHeightUpDown
      // 
      this.blockHeightUpDown.Location = new System.Drawing.Point(162, 50);
      this.blockHeightUpDown.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
      this.blockHeightUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.blockHeightUpDown.Name = "blockHeightUpDown";
      this.blockHeightUpDown.Size = new System.Drawing.Size(50, 21);
      this.blockHeightUpDown.TabIndex = 5;
      this.blockHeightUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
      // 
      // ArtStringDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 133);
      this.Controls.Add(this.groupBox);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(300, 160);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(300, 160);
      this.Name = "ArtStringDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "艺术字符";
      ((System.ComponentModel.ISupportInitialize)(this.blockWidthUpDown)).EndInit();
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.blockHeightUpDown)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.NumericUpDown blockWidthUpDown;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.TextBox textTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown blockHeightUpDown;
  }
}