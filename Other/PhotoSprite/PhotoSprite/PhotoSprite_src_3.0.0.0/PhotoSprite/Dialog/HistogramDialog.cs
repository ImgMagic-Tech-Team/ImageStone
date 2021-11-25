using System;
using System.Drawing;
using System.Windows.Forms;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.Dialog
{
  public partial class HistogramDialog : Form
  {
    Bitmap standardImage = null;
    Histogram histogram;
    Statistics statistics;

    public HistogramDialog(Bitmap b)
    {
      InitializeComponent();

      this.standardImage = b;
      histogram = new Histogram(this.standardImage);
      UpdateHistogram();
    }

    private void histogramPictureBox_MouseLeave(object sender, EventArgs e)
    {
      this.Cursor = Cursors.Default;

      this.levelLabel.Text = "";
      this.countLabel.Text = "";
      this.probabilityLabel.Text = "";
    }

    private void histogramPictureBox_MouseMove(object sender, MouseEventArgs e)
    {
      this.Cursor = Cursors.Cross;

      int level = e.X;
      this.levelLabel.Text = level.ToString();
      this.countLabel.Text = statistics.Value[level].ToString();
      double percent = statistics.Probability[level] / statistics.Probability[statistics.MaxIndex];
      this.probabilityLabel.Text = Convert.ToString((int)(percent * 100) / 100.0);
    }

    private void colorModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateHistogram();
    }

    private void viewByLogCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      UpdateHistogram();
    }

    /// <summary>
    /// 更新统计情况
    /// </summary>
    private void UpdateHistogram()
    {
      int diagramHeight = this.histogramPictureBox.Height;
      bool viewByLog = this.viewByLogCheckBox.Checked;
      Histogram.ColorMode colorMode = Histogram.ColorMode.Red;

      switch (this.colorModeComboBox.SelectedIndex)
      {
        case 0:
          statistics = histogram.Red;
          colorMode = Histogram.ColorMode.Red;
          break;

        case 1:
          statistics = histogram.Green;
          colorMode = Histogram.ColorMode.Green;
          break;

        case 2:
          statistics = histogram.Blue;
          colorMode = Histogram.ColorMode.Blue;
          break;
      } // switch

      this.histogramPictureBox.Image = histogram.DrawDiagram(diagramHeight, viewByLog, colorMode);

      this.meanLabel.Text = Convert.ToString((int)(statistics.Mean * 100) / 100.0);
      this.stdDevlabel.Text = Convert.ToString((int)(statistics.StdDev * 100) / 100.0);
      this.medianLabel.Text = statistics.Median.ToString();
      this.maxLabel.Text = statistics.Maximum.ToString();
      this.minLabel.Text = statistics.Minimum.ToString();
    } // end of UpdateHistogram

  }
}