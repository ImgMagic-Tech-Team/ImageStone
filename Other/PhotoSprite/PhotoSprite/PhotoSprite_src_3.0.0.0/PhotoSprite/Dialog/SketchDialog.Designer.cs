namespace PhotoSprite.Dialog
{
  partial class SketchDialog
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
      this.thresholdUpDown = new System.Windows.Forms.NumericUpDown();
      this.radSketch = new System.Windows.Forms.RadioButton();
      this.radPencil = new System.Windows.Forms.RadioButton();
      this.thresholdTrackBar = new System.Windows.Forms.TrackBar();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.thresholdUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox
      // 
      this.groupBox.Controls.Add(this.thresholdUpDown);
      this.groupBox.Controls.Add(this.radSketch);
      this.groupBox.Controls.Add(this.radPencil);
      this.groupBox.Controls.Add(this.thresholdTrackBar);
      this.groupBox.Location = new System.Drawing.Point(12, 12);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new System.Drawing.Size(268, 79);
      this.groupBox.TabIndex = 11;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "颜色调节";
      // 
      // thresholdUpDown
      // 
      this.thresholdUpDown.Location = new System.Drawing.Point(210, 22);
      this.thresholdUpDown.Name = "thresholdUpDown";
      this.thresholdUpDown.Size = new System.Drawing.Size(50, 21);
      this.thresholdUpDown.TabIndex = 4;
      this.thresholdUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.thresholdUpDown.ValueChanged += new System.EventHandler(this.thresholdUpDown_ValueChanged);
      // 
      // radSketch
      // 
      this.radSketch.AutoSize = true;
      this.radSketch.Checked = true;
      this.radSketch.Location = new System.Drawing.Point(71, 52);
      this.radSketch.Name = "radSketch";
      this.radSketch.Size = new System.Drawing.Size(47, 16);
      this.radSketch.TabIndex = 6;
      this.radSketch.TabStop = true;
      this.radSketch.Text = "素描";
      this.radSketch.UseVisualStyleBackColor = true;
      this.radSketch.CheckedChanged += new System.EventHandler(this.radSketch_CheckedChanged);
      // 
      // radPencil
      // 
      this.radPencil.AutoSize = true;
      this.radPencil.Location = new System.Drawing.Point(6, 52);
      this.radPencil.Name = "radPencil";
      this.radPencil.Size = new System.Drawing.Size(47, 16);
      this.radPencil.TabIndex = 5;
      this.radPencil.TabStop = true;
      this.radPencil.Text = "铅笔";
      this.radPencil.UseVisualStyleBackColor = true;
      this.radPencil.CheckedChanged += new System.EventHandler(this.radPencil_CheckedChanged);
      // 
      // thresholdTrackBar
      // 
      this.thresholdTrackBar.AutoSize = false;
      this.thresholdTrackBar.Location = new System.Drawing.Point(6, 20);
      this.thresholdTrackBar.Maximum = 100;
      this.thresholdTrackBar.Name = "thresholdTrackBar";
      this.thresholdTrackBar.Size = new System.Drawing.Size(200, 26);
      this.thresholdTrackBar.TabIndex = 3;
      this.thresholdTrackBar.TickFrequency = 0;
      this.thresholdTrackBar.Value = 5;
      this.thresholdTrackBar.Scroll += new System.EventHandler(this.thresholdTrackBar_Scroll);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(215, 97);
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
      this.btnOk.Location = new System.Drawing.Point(149, 97);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(51, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // SketchDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 134);
      this.Controls.Add(this.groupBox);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(300, 161);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(300, 161);
      this.Name = "SketchDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "素描";
      this.Load += new System.EventHandler(this.SketchDialog_Load);
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.thresholdUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.TrackBar thresholdTrackBar;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.RadioButton radSketch;
    private System.Windows.Forms.RadioButton radPencil;
    private System.Windows.Forms.NumericUpDown thresholdUpDown;
  }
}