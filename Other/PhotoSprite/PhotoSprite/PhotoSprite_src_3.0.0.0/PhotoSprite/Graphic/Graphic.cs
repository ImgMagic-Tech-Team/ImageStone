/***************************************************************************
 * 
 * 
 * PhotoSprite 图像精灵
 * 
 * 本组件包含水印处理、图像位处理、图像滤镜等众多图像处理功能
 * 为 PhotoSprite 图像处理核心部分
 * 
 * 
 * 
 * 作者：联骏 
 * 2006-1-1 收集整理完成
 * 
 * 本程序在如下环境调试通过：
 *    Window 2000 Server SP4, Windows XP SP2
 *    Microsoft Visual Studio 2005 C#
 * 
 * 版权所有 Copy Right 2005 PhotoSprite.com
 * 如要转载，请注明出处
 * 
 * 如有任何问题，请访问我们的技术支持网站： www.PhotoSprite.com
 * QQ: 120314684   E-mail: zjzmzy@163.com
 * 
 ***************************************************************************/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PhotoSprite.ImageProcessing;
using PhotoSprite.ImageFormat;

namespace PhotoSprite
{
  /// <summary>
  /// 封装了所有 PhotoSprite 图像处理算法
  /// </summary>
  public partial class Graphic : ImageInfo
  {
    /**************************************************
     * 
     * 对象属性
     * 
     * X 坐标、Y 坐标
     * 目标宽度、目标高度、目标半径、百分比
     * 
     **************************************************/

    /// <summary>
    /// 获取或设置 X 坐标
    /// </summary>
    public int X
    {
      get
      {
        return x;
      }
      set
      {
        x = value;
      }
    }
    /// <summary>
    /// X 坐标
    /// </summary>
    int x = 0;


    /// <summary>
    /// 获取或设置 Y 坐标
    /// </summary>
    public int Y
    {
      get
      {
        return y;
      }
      set
      {
        y = value;
      }
    }
    /// <summary>
    /// Y 坐标
    /// </summary>
    int y = 0;


    /// <summary>
    /// 获取或设置图像宽度
    /// </summary>
    public int Width
    {
      get
      {
        return width;
      }
      set
      {
        width = value;
      }
    }
    /// <summary>
    /// 图像宽度
    /// </summary>
    int width = 0;


    /// <summary>
    /// 获取或设置图像高度
    /// </summary>
    public int Height
    {
      get
      {
        return height;
      }
      set
      {
        height = value;
      }
    }
    /// <summary>
    /// 图像高度
    /// </summary>
    int height = 0;


    /// <summary>
    /// 获取或设置目标半径
    /// </summary>
    public int Radius
    {
      get
      {
        return radius;
      }
      set
      {
        radius = value;
      }
    }
    /// <summary>
    /// 目标半径
    /// </summary>
    int radius = 0;


    /// <summary>
    /// 获取或设置图像百分率
    /// </summary>
    public double Percent
    {
      get
      {
        return percent;
      }
      set
      {
        percent = value;
      }
    }
    /// <summary>
    /// 图像百分率
    /// </summary>
    double percent = 0.5;


    /**************************************************
     * 
     * 自定义属性
     * 
     * 对齐方式、适应方式
     * 
     **************************************************/


    /// <summary>
    /// 获取或设置对齐方式
    /// </summary>
    public Function.AlignMode Align
    {
      get
      {
        return align;
      }
      set
      {
        align = value;
      }
    }
    /// <summary>
    /// 对齐方式
    /// </summary>
    Function.AlignMode align = Function.AlignMode.TopLeft;


    /// <summary>
    /// 获取或设置适应方式
    /// </summary>
    public Function.FitMode Fit
    {
      get
      {
        return fit;
      }
      set
      {
        fit = value;
      }
    }
    /// <summary>
    /// 适应方式
    /// </summary>
    Function.FitMode fit = Function.FitMode.WidthHeight;


    /**************************************************
     * 
     * 基本属性
     * 
     * 源文件名、目标文件名
     * 
     **************************************************/

    /// <summary>
    /// 获取或设置原始文件名
    /// </summary>
    public string SourceFile
    {
      get
      {
        return sourceFile;
      }
      set
      {
        sourceFile = value;
      }
    }
    /// <summary>
    /// 原始文件名
    /// </summary>
    string sourceFile;

    /// <summary>
    /// 获取或设置目标文件名
    /// </summary>
    public string DestFile
    {
      get
      {
        return destFile;
      }
      set
      {
        destFile = value;
      }
    }
    /// <summary>
    /// 目标文件名
    /// </summary>
    string destFile;


    /************************************************************
     * 
     * 打开、保存、转换格式
     * 
     ************************************************************/


    /// <summary>
    /// 根据文件扩展名获取图像格式类型
    /// </summary>
    /// <param name="strImageFile">图像文件名</param>
    /// <returns></returns>
    private System.Drawing.Imaging.ImageFormat GetImageFormat(string strImageFile)
    {
      System.Drawing.Imaging.ImageFormat imageFormat;
      strImageFile = strImageFile.ToUpper();

      switch (strImageFile.Substring(strImageFile.LastIndexOf(".") + 1))
      {
        case "JPG":
        case "JPEG":
          imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
          break;

        case "GIF":
          imageFormat = System.Drawing.Imaging.ImageFormat.Gif;
          break;

        case "BMP":
          imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
          break;

        case "PNG":
          imageFormat = System.Drawing.Imaging.ImageFormat.Png;
          break;

        case "TIF":
        case "TIFF":
          imageFormat = System.Drawing.Imaging.ImageFormat.Tiff;
          break;

        default:
          imageFormat = System.Drawing.Imaging.ImageFormat.MemoryBmp;
          break;
      }

      return imageFormat;
    } // end of GetImageFormat


    /// <summary>
    /// 打开图像文件
    /// </summary>
    /// <param name="fileName">图像文件名</param>
    /// <returns></returns>
    public Bitmap Open(string fileName)
    {
      if (fileName.Substring(fileName.LastIndexOf(".") + 1).ToUpper() == "PSF")
      {
        PsfFormat pf = new PsfFormat();
        return pf.Open(fileName);
      }

      Bitmap srcImage = (Bitmap)Bitmap.FromFile(fileName, true);
      int width = srcImage.Width;
      int height = srcImage.Height;

      // 以下做法是为释放源文件，可避免程序异常
      Bitmap dstImage = new Bitmap(width, height, 
        System.Drawing.Imaging.PixelFormat.Format32bppArgb);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      g.DrawImage(srcImage,
        new System.Drawing.Rectangle(0, 0, width, height),
        new System.Drawing.Rectangle(0, 0, width, height), 
        System.Drawing.GraphicsUnit.Pixel);

      g.Save();
      g.Dispose();

      srcImage.Dispose();

      return dstImage;
    } // end of Open


    /// <summary>
    /// 按指定格式保存图像文件
    /// </summary>
    /// <param name="b">位图流</param>
    public void Save(Bitmap b)
    {
      if (destFile.Substring(destFile.LastIndexOf(".") + 1).ToUpper() == "PSF")
      {
        PsfFormat pf = new PsfFormat();
        pf.Save(b, destFile);
        return;
      }

      System.Drawing.Imaging.ImageFormat imageFormat = GetImageFormat(destFile);
      b.Save(destFile, imageFormat);
    } // end of Save


    /// <summary>
    /// 按图像文件后缀名对图像格式进行自动转换
    /// </summary>
    public void ConvertFormat()
    {
      Save(Open(sourceFile));
    } // end of ConvertFormat


  }
}
