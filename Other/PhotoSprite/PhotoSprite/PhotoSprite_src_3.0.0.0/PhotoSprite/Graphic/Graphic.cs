/***************************************************************************
 * 
 * 
 * PhotoSprite ͼ����
 * 
 * ���������ˮӡ����ͼ��λ����ͼ���˾����ڶ�ͼ������
 * Ϊ PhotoSprite ͼ������Ĳ���
 * 
 * 
 * 
 * ���ߣ����� 
 * 2006-1-1 �ռ��������
 * 
 * �����������»�������ͨ����
 *    Window 2000 Server SP4, Windows XP SP2
 *    Microsoft Visual Studio 2005 C#
 * 
 * ��Ȩ���� Copy Right 2005 PhotoSprite.com
 * ��Ҫת�أ���ע������
 * 
 * �����κ����⣬��������ǵļ���֧����վ�� www.PhotoSprite.com
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
  /// ��װ������ PhotoSprite ͼ�����㷨
  /// </summary>
  public partial class Graphic : ImageInfo
  {
    /**************************************************
     * 
     * ��������
     * 
     * X ���ꡢY ����
     * Ŀ���ȡ�Ŀ��߶ȡ�Ŀ��뾶���ٷֱ�
     * 
     **************************************************/

    /// <summary>
    /// ��ȡ������ X ����
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
    /// X ����
    /// </summary>
    int x = 0;


    /// <summary>
    /// ��ȡ������ Y ����
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
    /// Y ����
    /// </summary>
    int y = 0;


    /// <summary>
    /// ��ȡ������ͼ����
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
    /// ͼ����
    /// </summary>
    int width = 0;


    /// <summary>
    /// ��ȡ������ͼ��߶�
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
    /// ͼ��߶�
    /// </summary>
    int height = 0;


    /// <summary>
    /// ��ȡ������Ŀ��뾶
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
    /// Ŀ��뾶
    /// </summary>
    int radius = 0;


    /// <summary>
    /// ��ȡ������ͼ��ٷ���
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
    /// ͼ��ٷ���
    /// </summary>
    double percent = 0.5;


    /**************************************************
     * 
     * �Զ�������
     * 
     * ���뷽ʽ����Ӧ��ʽ
     * 
     **************************************************/


    /// <summary>
    /// ��ȡ�����ö��뷽ʽ
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
    /// ���뷽ʽ
    /// </summary>
    Function.AlignMode align = Function.AlignMode.TopLeft;


    /// <summary>
    /// ��ȡ��������Ӧ��ʽ
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
    /// ��Ӧ��ʽ
    /// </summary>
    Function.FitMode fit = Function.FitMode.WidthHeight;


    /**************************************************
     * 
     * ��������
     * 
     * Դ�ļ�����Ŀ���ļ���
     * 
     **************************************************/

    /// <summary>
    /// ��ȡ������ԭʼ�ļ���
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
    /// ԭʼ�ļ���
    /// </summary>
    string sourceFile;

    /// <summary>
    /// ��ȡ������Ŀ���ļ���
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
    /// Ŀ���ļ���
    /// </summary>
    string destFile;


    /************************************************************
     * 
     * �򿪡����桢ת����ʽ
     * 
     ************************************************************/


    /// <summary>
    /// �����ļ���չ����ȡͼ���ʽ����
    /// </summary>
    /// <param name="strImageFile">ͼ���ļ���</param>
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
    /// ��ͼ���ļ�
    /// </summary>
    /// <param name="fileName">ͼ���ļ���</param>
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

      // ����������Ϊ�ͷ�Դ�ļ����ɱ�������쳣
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
    /// ��ָ����ʽ����ͼ���ļ�
    /// </summary>
    /// <param name="b">λͼ��</param>
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
    /// ��ͼ���ļ���׺����ͼ���ʽ�����Զ�ת��
    /// </summary>
    public void ConvertFormat()
    {
      Save(Open(sourceFile));
    } // end of ConvertFormat


  }
}
