using System;
using System.Windows.Forms;

namespace PhotoSprite.Dialog
{
  public partial class ArtStringDialog : Form
  {
    public ArtStringDialog()
    {
      InitializeComponent();
    }

    /// <summary>
    /// ��ȡҪ��ʾ���ַ�
    /// </summary>
    public string ArtString
    {
      get
      {
        return this.textTextBox.Text;
      }
    }

    /// <summary>
    /// ��ȡ���
    /// </summary>
    public int BlockWidth
    {
      get
      {
        return (int)this.blockWidthUpDown.Value;
      }
    }

    /// <summary>
    /// ��ȡ���
    /// </summary>
    public int BlockHeight
    {
      get
      {
        return (int)this.blockHeightUpDown.Value;
      }
    }
  }
}