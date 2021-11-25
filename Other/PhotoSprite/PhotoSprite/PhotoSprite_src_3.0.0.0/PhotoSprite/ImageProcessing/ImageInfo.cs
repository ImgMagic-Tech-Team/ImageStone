using System;
using System.Reflection;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// PhotoSprite 图像信息基类
  /// </summary>
  public class ImageInfo
  {
    /// <summary>
    /// 每像素字节数 BytesPerPixel
    /// </summary>
    public const int BPP = 4;

    /// <summary>
    /// 每两像素字节数 BytesPer2Pixels
    /// </summary>
    public const int BP2P = 8;


    /**************************************************
     * 
     * 组件信息
     * 
     * 产品名、版本号
     * 
     **************************************************/

    /// <summary>
    /// 获取 PhotoSprite 产品名
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
    /// 获取 PhotoSprite 当前版本号
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
