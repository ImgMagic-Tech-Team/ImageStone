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
    /// 获取要显示的字符
    /// </summary>
    public string ArtString
    {
      get
      {
        return this.textTextBox.Text;
      }
    }

    /// <summary>
    /// 获取块宽
    /// </summary>
    public int BlockWidth
    {
      get
      {
        return (int)this.blockWidthUpDown.Value;
      }
    }

    /// <summary>
    /// 获取块高
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