namespace PhotoSprite.Dialog
{
  partial class HistogramDialog
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
      this.label1 = new System.Windows.Forms.Label();
      this.colorModeComboBox = new System.Windows.Forms.ComboBox();
      this.viewByLogCheckBox = new System.Windows.Forms.CheckBox();
      this.histogramPictureBox = new System.Windows.Forms.PictureBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.minLabel = new System.Windows.Forms.Label();
      this.maxLabel = new System.Windows.Forms.Label();
      this.medianLabel = new System.Windows.Forms.Label();
      this.stdDevlabel = new System.Windows.Forms.Label();
      this.meanLabel = new System.Windows.Forms.Label();
      this.probabilityLabel = new System.Windows.Forms.Label();
      this.countLabel = new System.Windows.Forms.Label();
      this.levelLabel = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.btnOk = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.histogramPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 11);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "通道:";
      // 
      // colorModeComboBox
      // 
      this.colorModeComboBox.FormattingEnabled = true;
      this.colorModeComboBox.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue"});
      this.colorModeComboBox.Location = new System.Drawing.Point(53, 7);
      this.colorModeComboBox.Name = "colorModeComboBox";
      this.colorModeComboBox.Size = new System.Drawing.Size(105, 20);
      this.colorModeComboBox.TabIndex = 2;
      this.colorModeComboBox.Text = "Red";
      this.colorModeComboBox.SelectedIndexChanged += new System.EventHandler(this.colorModeComboBox_SelectedIndexChanged);
      // 
      // viewByLogCheckBox
      // 
      this.viewByLogCheckBox.AutoSize = true;
      this.viewByLogCheckBox.Location = new System.Drawing.Point(184, 11);
      this.viewByLogCheckBox.Name = "viewByLogCheckBox";
      this.viewByLogCheckBox.Size = new System.Drawing.Size(114, 16);
      this.viewByLogCheckBox.TabIndex = 3;
      this.viewByLogCheckBox.Text = "按 Log 方式显示";
      this.viewByLogCheckBox.UseVisualStyleBackColor = true;
      this.viewByLogCheckBox.CheckedChanged += new System.EventHandler(this.viewByLogCheckBox_CheckedChanged);
      // 
      // histogramPictureBox
      // 
      this.histogramPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.histogramPictureBox.Location = new System.Drawing.Point(25, 42);
      this.histogramPictureBox.Name = "histogramPictureBox";
      this.histogramPictureBox.Size = new System.Drawing.Size(257, 192);
      this.histogramPictureBox.TabIndex = 3;
      this.histogramPictureBox.TabStop = false;
      this.histogramPictureBox.MouseLeave += new System.EventHandler(this.histogramPictureBox_MouseLeave);
      this.histogramPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.histogramPictureBox_MouseMove);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 252);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(47, 12);
      this.label2.TabIndex = 0;
      this.label2.Text = "平均值:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 276);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(59, 12);
      this.label3.TabIndex = 0;
      this.label3.Text = "标准偏差:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 300);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(35, 12);
      this.label4.TabIndex = 0;
      this.label4.Text = "中值:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(12, 324);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(47, 12);
      this.label5.TabIndex = 0;
      this.label5.Text = "最大值:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 348);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(47, 12);
      this.label6.TabIndex = 0;
      this.label6.Text = "最小值:";
      // 
      // minLabel
      // 
      this.minLabel.AutoSize = true;
      this.minLabel.Location = new System.Drawing.Point(89, 348);
      this.minLabel.Name = "minLabel";
      this.minLabel.Size = new System.Drawing.Size(23, 12);
      this.minLabel.TabIndex = 7;
      this.minLabel.Text = "Min";
      this.minLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // maxLabel
      // 
      this.maxLabel.AutoSize = true;
      this.maxLabel.Location = new System.Drawing.Point(89, 324);
      this.maxLabel.Name = "maxLabel";
      this.maxLabel.Size = new System.Drawing.Size(23, 12);
      this.maxLabel.TabIndex = 8;
      this.maxLabel.Text = "Max";
      this.maxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // medianLabel
      // 
      this.medianLabel.AutoSize = true;
      this.medianLabel.Location = new System.Drawing.Point(89, 300);
      this.medianLabel.Name = "medianLabel";
      this.medianLabel.Size = new System.Drawing.Size(41, 12);
      this.medianLabel.TabIndex = 6;
      this.medianLabel.Text = "Median";
      this.medianLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // stdDevlabel
      // 
      this.stdDevlabel.AutoSize = true;
      this.stdDevlabel.Location = new System.Drawing.Point(89, 276);
      this.stdDevlabel.Name = "stdDevlabel";
      this.stdDevlabel.Size = new System.Drawing.Size(47, 12);
      this.stdDevlabel.TabIndex = 4;
      this.stdDevlabel.Text = "Std Dev";
      this.stdDevlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // meanLabel
      // 
      this.meanLabel.AutoSize = true;
      this.meanLabel.Location = new System.Drawing.Point(89, 252);
      this.meanLabel.Name = "meanLabel";
      this.meanLabel.Size = new System.Drawing.Size(29, 12);
      this.meanLabel.TabIndex = 5;
      this.meanLabel.Text = "Mean";
      this.meanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // probabilityLabel
      // 
      this.probabilityLabel.AutoSize = true;
      this.probabilityLabel.Location = new System.Drawing.Point(218, 300);
      this.probabilityLabel.Name = "probabilityLabel";
      this.probabilityLabel.Size = new System.Drawing.Size(71, 12);
      this.probabilityLabel.TabIndex = 14;
      this.probabilityLabel.Text = "Probability";
      // 
      // countLabel
      // 
      this.countLabel.AutoSize = true;
      this.countLabel.Location = new System.Drawing.Point(218, 276);
      this.countLabel.Name = "countLabel";
      this.countLabel.Size = new System.Drawing.Size(35, 12);
      this.countLabel.TabIndex = 12;
      this.countLabel.Text = "Count";
      // 
      // levelLabel
      // 
      this.levelLabel.AutoSize = true;
      this.levelLabel.Location = new System.Drawing.Point(218, 252);
      this.levelLabel.Name = "levelLabel";
      this.levelLabel.Size = new System.Drawing.Size(35, 12);
      this.levelLabel.TabIndex = 13;
      this.levelLabel.Text = "Level";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(165, 300);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(35, 12);
      this.label10.TabIndex = 10;
      this.label10.Text = "概率:";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(165, 276);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(35, 12);
      this.label11.TabIndex = 9;
      this.label11.Text = "统计:";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(165, 252);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(47, 12);
      this.label12.TabIndex = 11;
      this.label12.Text = "亮度级:";
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(167, 336);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "确定";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // HistogramDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(306, 371);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.probabilityLabel);
      this.Controls.Add(this.countLabel);
      this.Controls.Add(this.levelLabel);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.label12);
      this.Controls.Add(this.minLabel);
      this.Controls.Add(this.maxLabel);
      this.Controls.Add(this.medianLabel);
      this.Controls.Add(this.stdDevlabel);
      this.Controls.Add(this.meanLabel);
      this.Controls.Add(this.histogramPictureBox);
      this.Controls.Add(this.viewByLogCheckBox);
      this.Controls.Add(this.colorModeComboBox);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(314, 398);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(314, 398);
      this.Name = "HistogramDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "直方图";
      ((System.ComponentModel.ISupportInitialize)(this.histogramPictureBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox colorModeComboBox;
    private System.Windows.Forms.CheckBox viewByLogCheckBox;
    private System.Windows.Forms.PictureBox histogramPictureBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label minLabel;
    private System.Windows.Forms.Label maxLabel;
    private System.Windows.Forms.Label medianLabel;
    private System.Windows.Forms.Label stdDevlabel;
    private System.Windows.Forms.Label meanLabel;
    private System.Windows.Forms.Label probabilityLabel;
    private System.Windows.Forms.Label countLabel;
    private System.Windows.Forms.Label levelLabel;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Button btnOk;
  }
}