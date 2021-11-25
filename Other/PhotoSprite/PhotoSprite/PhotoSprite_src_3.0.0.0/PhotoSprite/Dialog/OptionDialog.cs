using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace PhotoSprite.Dialog
{
  public partial class OptionDialog : Form
  {
    public OptionDialog()
    {
      InitializeComponent();
    }

    /// <summary>
    /// 获取默认图像打开目录
    /// </summary>
    public string OpenFolder
    {
      get
      {
        return openFolderTextBox.Text;
      }
    }

    /// <summary>
    /// 获取默认图像保存目录
    /// </summary>
    public string SaveFolder
    {
      get
      {
        return saveFolderTextBox.Text;
      }
    }

    /// <summary>
    /// 获取临时文件存放目录
    /// </summary>
    public string TmpFolder
    {
      get
      {
        return tmpFolderTextBox.Text;
      }
    }

    /// <summary>
    /// 获取最大撤消次数
    /// </summary>
    public int UndoTimes
    {
      get
      {
        return (int)undoTimesUpDown.Value;
      }
    }

    private void OptionDialog_Load(object sender, EventArgs e)
    {
      System.IO.StreamReader sr = new StreamReader(Application.StartupPath + @"\ps.cfg", System.Text.Encoding.Default);
      this.openFolderTextBox.Text = sr.ReadLine().Replace("OpenFolder: ", "");
      this.saveFolderTextBox.Text = sr.ReadLine().Replace("SaveFolder: ", "");
      this.tmpFolderTextBox.Text = sr.ReadLine().Replace("TmpFolder: ", "");
      this.undoTimesUpDown.Value = Convert.ToInt32(sr.ReadLine().Replace("UndoTimes: ", ""));
      sr.Close();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      System.IO.StreamWriter sw = new StreamWriter(Application.StartupPath + @"\ps.cfg", false, System.Text.Encoding.Default );
      sw.WriteLine("OpenFolder: " + this.openFolderTextBox.Text);
      sw.WriteLine("SaveFolder: " + this.saveFolderTextBox.Text);
      sw.WriteLine("TmpFolder: " + this.tmpFolderTextBox.Text);
      sw.WriteLine("UndoTimes: " + this.undoTimesUpDown.Value.ToString());
      sw.Close();
    }

    private void associationCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      RegistryKey RegKey = Registry.ClassesRoot;

      if (this.associationCheckBox.Checked)
      {
        try
        {
          // 关联 .PSF 文件
          RegKey.CreateSubKey(".psf").SetValue("", "PhotoSprite.Psf");

          RegKey = RegKey.CreateSubKey("PhotoSprite.Psf");
          RegKey.SetValue("", "PhotoSprite Special Format(PSF)");
          RegKey.CreateSubKey("DefaultIcon").SetValue("", Application.ExecutablePath + ",1");

          RegKey = RegKey.CreateSubKey("shell");
          RegKey = RegKey.CreateSubKey("Open");
          RegKey.SetValue("", "&Open with PhotoSprite");
          RegKey.CreateSubKey("Command").SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"");
        }
        catch
        {
          MessageBox.Show("关联 PSF 文件失败！", "友情提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
      else
      {
        try
        {
          // 删除 .PSF 文件关联
          RegKey.DeleteSubKey(".psf");
          RegKey.DeleteSubKeyTree("PhotoSprite.Psf");
        }
        catch
        {
        }
      }

      RegKey.Close();
    }

  }
}