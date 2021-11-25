namespace PhotoSprite.Dialog
{
  partial class DirectionDialog
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
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox = new System.Windows.Forms.GroupBox();
      this.angleTextBox = new System.Windows.Forms.TextBox();
      this.angleChooser = new PhotoSprite.AngleChooser();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(13, 110);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.angleTextBox);
      this.groupBox.Controls.Add(this.angleChooser);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(111, 88);
      this.groupBox.TabIndex = 10;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "方向";
      // 
      // angleTextBox
      // 
      this.angleTextBox.Location = new System.Drawing.Point(72, 40);
      this.angleTextBox.Name = "angleTextBox";
      this.angleTextBox.ReadOnly = true;
      this.angleTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.angleTextBox.Size = new System.Drawing.Size(29, 21);
      this.angleTextBox.TabIndex = 4;
      this.angleTextBox.Text = "E";
      // 
      // angleChooser
      // 
      this.angleChooser.Angle = 0;
      this.angleChooser.Location = new System.Drawing.Point(6, 20);
      this.angleChooser.Name = "angleChooser";
      this.angleChooser.Size = new System.Drawing.Size(60, 60);
      this.angleChooser.TabIndex = 3;
      this.angleChooser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.angleChooser_MouseMove);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(72, 110);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // DirectionDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(136, 143);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox);
      this.Controls.Add(this.btnCancel);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(144, 170);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(144, 170);
      this.Name = "DirectionDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "八方向浮雕";
      this.Load += new System.EventHandler(this.DirectionDialog_Load);
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.TextBox angleTextBox;
    private AngleChooser angleChooser;
    private System.Windows.Forms.Button btnCancel;
  }
}