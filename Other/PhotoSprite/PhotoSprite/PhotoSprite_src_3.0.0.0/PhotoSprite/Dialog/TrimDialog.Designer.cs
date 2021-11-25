namespace PhotoSprite.Dialog
{
  partial class TrimDialog
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
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.rightCheckBox = new System.Windows.Forms.CheckBox();
      this.leftCheckBox = new System.Windows.Forms.CheckBox();
      this.bottomCheckBox = new System.Windows.Forms.CheckBox();
      this.topCheckBox = new System.Windows.Forms.CheckBox();
      this.groupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(121, 44);
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
      this.btnOk.Location = new System.Drawing.Point(121, 12);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.rightCheckBox);
      this.groupBox.Controls.Add(this.leftCheckBox);
      this.groupBox.Controls.Add(this.bottomCheckBox);
      this.groupBox.Controls.Add(this.topCheckBox);
      this.groupBox.Location = new System.Drawing.Point(12, 7);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(104, 84);
      this.groupBox.TabIndex = 24;
      this.groupBox.TabStop = false;
      // 
      // rightCheckBox
      // 
      this.rightCheckBox.AutoSize = true;
      this.rightCheckBox.Checked = true;
      this.rightCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.rightCheckBox.Location = new System.Drawing.Point(66, 37);
      this.rightCheckBox.Name = "rightCheckBox";
      this.rightCheckBox.Size = new System.Drawing.Size(36, 16);
      this.rightCheckBox.TabIndex = 5;
      this.rightCheckBox.Text = "右";
      this.rightCheckBox.UseVisualStyleBackColor = true;
      this.rightCheckBox.CheckedChanged += new System.EventHandler(this.rightCheckBox_CheckedChanged);
      // 
      // leftCheckBox
      // 
      this.leftCheckBox.AutoSize = true;
      this.leftCheckBox.Checked = true;
      this.leftCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.leftCheckBox.Location = new System.Drawing.Point(6, 37);
      this.leftCheckBox.Name = "leftCheckBox";
      this.leftCheckBox.Size = new System.Drawing.Size(36, 16);
      this.leftCheckBox.TabIndex = 4;
      this.leftCheckBox.Text = "左";
      this.leftCheckBox.UseVisualStyleBackColor = true;
      this.leftCheckBox.CheckedChanged += new System.EventHandler(this.leftCheckBox_CheckedChanged);
      // 
      // bottomCheckBox
      // 
      this.bottomCheckBox.AutoSize = true;
      this.bottomCheckBox.Checked = true;
      this.bottomCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.bottomCheckBox.Location = new System.Drawing.Point(36, 57);
      this.bottomCheckBox.Name = "bottomCheckBox";
      this.bottomCheckBox.Size = new System.Drawing.Size(36, 16);
      this.bottomCheckBox.TabIndex = 6;
      this.bottomCheckBox.Text = "下";
      this.bottomCheckBox.UseVisualStyleBackColor = true;
      this.bottomCheckBox.CheckedChanged += new System.EventHandler(this.bottomCheckBox_CheckedChanged);
      // 
      // topCheckBox
      // 
      this.topCheckBox.AutoSize = true;
      this.topCheckBox.Checked = true;
      this.topCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.topCheckBox.Location = new System.Drawing.Point(36, 17);
      this.topCheckBox.Name = "topCheckBox";
      this.topCheckBox.Size = new System.Drawing.Size(36, 16);
      this.topCheckBox.TabIndex = 3;
      this.topCheckBox.Text = "上";
      this.topCheckBox.UseVisualStyleBackColor = true;
      this.topCheckBox.CheckedChanged += new System.EventHandler(this.topCheckBox_CheckedChanged);
      // 
      // TrimDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(184, 109);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(192, 136);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(192, 136);
      this.Name = "TrimDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "修整";
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.CheckBox rightCheckBox;
    private System.Windows.Forms.CheckBox leftCheckBox;
    private System.Windows.Forms.CheckBox bottomCheckBox;
    private System.Windows.Forms.CheckBox topCheckBox;
  }
}