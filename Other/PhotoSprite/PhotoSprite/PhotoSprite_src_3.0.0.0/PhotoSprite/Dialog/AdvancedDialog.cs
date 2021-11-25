using System;
using System.Windows.Forms;

namespace PhotoSprite.Dialog
{
  public partial class AdvancedDialog : Form
  {
    private int[] sequence;

    /// <summary>
    /// ��ȡ��������
    /// </summary>
    public int[] Sequence
    {
      get
      {
        return sequence;
      }
    }

    public AdvancedDialog()
    {
      InitializeComponent();
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
      // ��ʼ����Ŀ¼Ϊ�������Ŀ¼
      openFileDialog.InitialDirectory = Application.StartupPath;

      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        string srcFile = openFileDialog.FileName;

        // ��ͼ���ļ�
        System.IO.FileStream myFS = new System.IO.FileStream(srcFile,
          System.IO.FileMode.Open, System.IO.FileAccess.Read);

        int len = (int)myFS.Length / 2;
        sequence = new int[len];

        // �洢ģ�����ݵĻ�����
        byte[] buffer = new byte[len * 2];

        // ��ȡ����
        myFS.Read(buffer, 0, len * 2);

        myFS.Close();

        // ����
        int m = 0;
        for (int i = 0; i < len; i++)
        {
          m = buffer[i * 2];
          m = m << 8;
          m |= buffer[i * 2 + 1];
          sequence[i] = (int)((short)m);
        } // i

        // �����ı���
        int N = (int)Math.Sqrt(len - 2);
        string strFormatString = "";
        this.templateRichTextBox.Text = "";
        for (int i = 0; i < len; i++)
        {
          strFormatString = "     " + sequence[i].ToString();
          this.templateRichTextBox.Text += strFormatString.Substring(strFormatString.Length - 5);

          if (i % N == N - 1)
            this.templateRichTextBox.Text += "\r\n";
        } // i

      }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      // ��ʼ������Ŀ¼Ϊ�������Ŀ¼
      saveFileDialog.InitialDirectory = Application.StartupPath;

      if (saveFileDialog.ShowDialog() == DialogResult.OK)
      {
        string dstFile = saveFileDialog.FileName;

        // ����ͼ���ļ�
        System.IO.FileStream myFS = new System.IO.FileStream(dstFile,
          System.IO.FileMode.Create, System.IO.FileAccess.Write);

        // ȡ�ı�������Ч���֣����ÿո�ָ���ÿ������
        char [] C = this.templateRichTextBox.Text.ToCharArray();
        bool bIsNewWord = true;
        string strNum = "";
        for (int i = 0; i < C.Length; i++)
        {
          byte c = (byte)C[i];

          // ��Ч���� [0123456789-]
          if (( c>= 48 && c <= 57) || (c==45))
          {
              strNum += C[i];
              bIsNewWord = true;
          }
          else
          {
            if (bIsNewWord)
            {
              strNum += " ";
              bIsNewWord = false;
            }
          }
        } // i

        // ȥ������Ŀո�
        strNum = strNum.Trim();

        // ������д�����
        string[] ValidNum = strNum.Split(' ');
        int len = ValidNum.Length;
        sequence = new int[len];
        for (int i = 0; i < len; i++)
        {
          sequence[i] = Convert.ToInt32(ValidNum[i]);
        } // i

        // �洢ģ�����ݵĻ�����
        byte[] buffer = new byte[len*2];
        int m = 0;

        for (int i = 0; i < len; i++)
        {
          m = sequence[i];
          buffer[i * 2] = (byte)(m >> 8);
          buffer[i * 2 + 1] = (byte)m;
        } // i

        // д������
        myFS.Write(buffer, 0, len*2);

        myFS.Close();
      }
    }
  }
}