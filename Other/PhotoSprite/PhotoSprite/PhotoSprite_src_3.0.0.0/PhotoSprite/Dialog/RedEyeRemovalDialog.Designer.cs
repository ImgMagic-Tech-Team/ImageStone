namespace PhotoSprite.Dialog
{
  partial class RedEyeRemovalDialog
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
      this.colorLabel = new System.Windows.Forms.Label();
      this.tolerenceUpDown = new System.Windows.Forms.NumericUpDown();
      this.tolerenceTrackBar = new System.Windows.Forms.TrackBar();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.colorDialog = new System.Windows.Forms.ColorDialog();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tolerenceUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tolerenceTrackBar)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(113, 110);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.colorLabel);
      this.groupBox.Controls.Add(this.tolerenceUpDown);
      this.groupBox.Controls.Add(this.tolerenceTrackBar);
      this.groupBox.Controls.Add(this.label2);
      this.groupBox.Controls.Add(this.label1);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(310, 92);
      this.groupBox.TabIndex = 10;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "红眼效果调节";
      // 
      // colorLabel
      // 
      this.colorLabel.BackColor = System.Drawing.Color.Gray;
      this.colorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.colorLabel.Location = new System.Drawing.Point(54, 54);
      this.colorLabel.Name = "colorLabel";
      this.colorLabel.Size = new System.Drawing.Size(24, 24);
      this.colorLabel.TabIndex = 6;
      this.colorLabel.Click += new System.EventHandler(this.colorLabel_Click);
      // 
      // tolerenceUpDown
      // 
      this.tolerenceUpDown.Location = new System.Drawing.Point(252, 24);
      this.tolerenceUpDown.Name = "tolerenceUpDown";
      this.tolerenceUpDown.Size = new System.Drawing.Size(50, 21);
      this.tolerenceUpDown.TabIndex = 4;
      this.tolerenceUpDown.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
      this.tolerenceUpDown.ValueChanged += new System.EventHandler(this.tolerenceUpDown_ValueChanged);
      // 
      // tolerenceTrackBar
      // 
      this.tolerenceTrackBar.AutoSize = false;
      this.tolerenceTrackBar.Location = new System.Drawing.Point(46, 24);
      this.tolerenceTrackBar.Maximum = 100;
      this.tolerenceTrackBar.Name = "tolerenceTrackBar";
      this.tolerenceTrackBar.Size = new System.Drawing.Size(200, 26);
      this.tolerenceTrackBar.TabIndex = 3;
      this.tolerenceTrackBar.TickFrequency = 0;
      this.tolerenceTrackBar.Value = 70;
      this.tolerenceTrackBar.Scroll += new System.EventHandler(this.tolerenceTrackBar_Scroll);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 61);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 12);
      this.label2.TabIndex = 5;
      this.label2.Text = "颜色:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 12);
      this.label1.TabIndex = 5;
      this.label1.Text = "容差:";
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(179, 110);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(51, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "取消";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // RedEyeRemovalDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(332, 143);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.groupBox);
      this.Controls.Add(this.btnCancel);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "RedEyeRemovalDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "去除红眼";
      this.Load += new System.EventHandler(this.RedEyeRemovalDialog_Load);
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tolerenceUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tolerenceTrackBar)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.TrackBar tolerenceTrackBar;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.NumericUpDown tolerenceUpDown;
    private System.Windows.Forms.Label colorLabel;
    private System.Windows.Forms.ColorDialog colorDialog;
  }
}