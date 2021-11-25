using System;
using System.Reflection;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// PhotoSprite ͼ����Ϣ����
  /// </summary>
  public class ImageInfo
  {
    /// <summary>
    /// ÿ�����ֽ��� BytesPerPixel
    /// </summary>
    public const int BPP = 4;

    /// <summary>
    /// ÿ�������ֽ��� BytesPer2Pixels
    /// </summary>
    public const int BP2P = 8;


    /**************************************************
     * 
     * �����Ϣ
     * 
     * ��Ʒ�����汾��
     * 
     **************************************************/

    /// <summary>
    /// ��ȡ PhotoSprite ��Ʒ��
    /// </summary>
    public string Product
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        if (attributes.Length == 0)
          return "";

        return ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    /// <summary>
    /// ��ȡ PhotoSprite ��ǰ�汾��
    /// </summary>
    public string Version
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }


  }
}
