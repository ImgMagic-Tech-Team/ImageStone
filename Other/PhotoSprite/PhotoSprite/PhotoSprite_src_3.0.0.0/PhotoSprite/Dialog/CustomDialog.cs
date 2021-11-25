using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace PhotoSprite.Dialog
{
  public partial class CustomDialog : Form
  {
    private int [] sequence = new int[27];

    /// <summary>
    /// 获取矩阵序列
    /// </summary>
    public int[] Sequence
    {
      get
      {
        return sequence;
      }
    }


    public CustomDialog()
    {
      InitializeComponent();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      Export();
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
      // 初始化打开目录为软件运行目录
      openFileDialog.InitialDirectory = Application.StartupPath;

      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        string srcFile = openFileDialog.FileName;

        // 打开图像文件
        System.IO.FileStream myFS = new System.IO.FileStream(srcFile,
          System.IO.FileMode.Open, System.IO.FileAccess.Read);

        // 存储模板数据的缓冲区
        byte[] buffer = new byte[54];

        // 读取数据
        myFS.Read(buffer, 0, 54);

        myFS.Close();

        // 导入
        int m = 0;
        for (int i = 0; i < 27; i++)
        {
          m = buffer[i * 2];
          m = m << 8;
          m |= buffer[i * 2 + 1];
          sequence[i] = (int)((short)m);
        } // i

        // 更新文本框
        Import();
      }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      // 初始化保存目录为软件运行目录
      saveFileDialog.InitialDirectory = Application.StartupPath;

      if (saveFileDialog.ShowDialog() == DialogResult.OK)
      {
        string dstFile = saveFileDialog.FileName;

        // 保存图像文件
        System.IO.FileStream myFS = new System.IO.FileStream(dstFile,
          System.IO.FileMode.Create, System.IO.FileAccess.Write);

        // 将文本框数据写入矩阵
        Export();

        // 存储模板数据的缓冲区
        byte[] buffer = new byte[54];
        int m = 0;

        for (int i = 0; i < 27; i++)
        {
          m = sequence[i];
          buffer[i * 2] = (byte)(m >> 8);
          buffer[i * 2 + 1] = (byte)m;
        } // i

        // 写入数据
        myFS.Write(buffer, 0, 54);

        myFS.Close();
      }
    }

    /// <summary>
    /// 将数据显示在文本框
    /// </summary>
    private void Import()
    {
      if (sequence[0] == 0) this.txt11.Text = "";
      else this.txt11.Text = sequence[0].ToString();

      if (sequence[1] == 0) this.txt12.Text = "";
      else this.txt12.Text = sequence[1].ToString();
      if (sequence[2] == 0) this.txt13.Text = "";
      else this.txt13.Text = sequence[2].ToString();
      if (sequence[3] == 0) this.txt14.Text = "";
      else this.txt14.Text = sequence[3].ToString();
      if (sequence[4] == 0) this.txt15.Text = "";
      else this.txt15.Text = sequence[4].ToString();

      if (sequence[5] == 0) this.txt21.Text = "";
      else this.txt21.Text = sequence[5].ToString();
      if (sequence[6] == 0) this.txt22.Text = "";
      else this.txt22.Text = sequence[6].ToString();
      if (sequence[7] == 0) this.txt23.Text = "";
      else this.txt23.Text = sequence[7].ToString();
      if (sequence[8] == 0) this.txt24.Text = "";
      else this.txt24.Text = sequence[8].ToString();
      if (sequence[9] == 0) this.txt25.Text = "";
      else this.txt25.Text = sequence[9].ToString();

      if (sequence[10] == 0) this.txt31.Text = "";
      else this.txt31.Text = sequence[10].ToString();
      if (sequence[11] == 0) this.txt32.Text = "";
      else this.txt32.Text = sequence[11].ToString();
      if (sequence[12] == 0) this.txt33.Text = "";
      else this.txt33.Text = sequence[12].ToString();
      if (sequence[13] == 0) this.txt34.Text = "";
      else this.txt34.Text = sequence[13].ToString();
      if (sequence[14] == 0) this.txt35.Text = "";
      else this.txt35.Text = sequence[14].ToString();
                                
      if (sequence[15] == 0) this.txt41.Text = "";
      else this.txt41.Text = sequence[15].ToString();
      if (sequence[16] == 0) this.txt42.Text = "";
      else this.txt42.Text = sequence[16].ToString();
      if (sequence[17] == 0) this.txt43.Text = "";
      else this.txt43.Text = sequence[17].ToString();
      if (sequence[18] == 0) this.txt44.Text = "";
      else this.txt44.Text = sequence[18].ToString();
      if (sequence[19] == 0) this.txt45.Text = "";
      else this.txt45.Text = sequence[19].ToString();
                                
      if (sequence[20] == 0) this.txt51.Text = "";
      else this.txt51.Text = sequence[20].ToString();
      if (sequence[21] == 0) this.txt52.Text = "";
      else this.txt52.Text = sequence[21].ToString();
      if (sequence[22] == 0) this.txt53.Text = "";
      else this.txt53.Text = sequence[22].ToString();
      if (sequence[23] == 0) this.txt54.Text = "";
      else this.txt54.Text = sequence[23].ToString();
      if (sequence[24] == 0) this.txt55.Text = "";
      else this.txt55.Text = sequence[24].ToString();

      if (sequence[25] == 0) this.txtScale.Text = "";
      else this.txtScale.Text = sequence[25].ToString();
      if (sequence[26] == 0) this.txtOffset.Text = "";
      else this.txtOffset.Text = sequence[26].ToString();
    } // end of Import

    /// <summary>
    /// 将文本框中数据转换为数据流
    /// </summary>
    private void Export()
    {
      if (this.txt11.Text == "") sequence[0] = 0;
      else sequence[0] = Convert.ToInt32(this.txt11.Text);
      if (this.txt12.Text=="") sequence[1] = 0;
      else sequence[1] = Convert.ToInt32(this.txt12.Text);
      if (this.txt13.Text == "") sequence[2] = 0;
      else sequence[2] = Convert.ToInt32(this.txt13.Text);
      if (this.txt14.Text == "") sequence[3] = 0;
      else sequence[3] = Convert.ToInt32(this.txt14.Text);
      if (this.txt15.Text == "") sequence[4] = 0;
      else sequence[4] = Convert.ToInt32(this.txt15.Text);

      if (this.txt21.Text == "") sequence[5] = 0;
      else sequence[5] = Convert.ToInt32(this.txt21.Text);
      if (this.txt22.Text == "") sequence[6] = 0;
      else sequence[6] = Convert.ToInt32(this.txt22.Text);
      if (this.txt23.Text == "") sequence[7] = 0;
      else sequence[7] = Convert.ToInt32(this.txt23.Text);
      if (this.txt24.Text == "") sequence[8] = 0;
      else sequence[8] = Convert.ToInt32(this.txt24.Text);
      if (this.txt25.Text == "") sequence[9] = 0;
      else sequence[9] = Convert.ToInt32(this.txt25.Text);

      if (this.txt31.Text == "") sequence[10] = 0;
      else sequence[10] = Convert.ToInt32(this.txt31.Text);
      if (this.txt32.Text == "") sequence[11] = 0;
      else sequence[11] = Convert.ToInt32(this.txt32.Text);
      if (this.txt33.Text == "") sequence[12] = 0;
      else sequence[12] = Convert.ToInt32(this.txt33.Text);
      if (this.txt34.Text == "") sequence[13] = 0;
      else sequence[13] = Convert.ToInt32(this.txt34.Text);
      if (this.txt35.Text == "") sequence[14] = 0;
      else sequence[14] = Convert.ToInt32(this.txt35.Text);

      if (this.txt41.Text == "") sequence[15] = 0;
      else sequence[15] = Convert.ToInt32(this.txt41.Text);
      if (this.txt42.Text == "") sequence[16] = 0;
      else sequence[16] = Convert.ToInt32(this.txt42.Text);
      if (this.txt43.Text == "") sequence[17] = 0;
      else sequence[17] = Convert.ToInt32(this.txt43.Text);
      if (this.txt44.Text == "") sequence[18] = 0;
      else sequence[18] = Convert.ToInt32(this.txt44.Text);
      if (this.txt45.Text == "") sequence[19] = 0;
      else sequence[19] = Convert.ToInt32(this.txt45.Text);

      if (this.txt51.Text == "") sequence[20] = 0;
      else sequence[20] = Convert.ToInt32(this.txt51.Text);
      if (this.txt52.Text == "") sequence[21] = 0;
      else sequence[21] = Convert.ToInt32(this.txt52.Text);
      if (this.txt53.Text == "") sequence[22] = 0;
      else sequence[22] = Convert.ToInt32(this.txt53.Text);
      if (this.txt54.Text == "") sequence[23] = 0;
      else sequence[23] = Convert.ToInt32(this.txt54.Text);
      if (this.txt55.Text == "") sequence[24] = 0;
      else sequence[24] = Convert.ToInt32(this.txt55.Text);

      if (this.txtScale.Text == "") sequence[25] = 0;
      else sequence[25] = Convert.ToInt32(this.txtScale.Text);
      if (this.txtOffset.Text == "") sequence[26] = 0;
      else sequence[26] = Convert.ToInt32(this.txtOffset.Text);
    } // end of Export

  }
}