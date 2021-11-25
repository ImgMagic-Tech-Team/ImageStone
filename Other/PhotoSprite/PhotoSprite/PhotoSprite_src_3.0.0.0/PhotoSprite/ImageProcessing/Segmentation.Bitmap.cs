using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoSprite.ImageProcessing
{
  partial class Segmentation
  {
    /************************************************************
     * 
     * 面积、周长、区域显示、消除小区域、轮廓跟踪、提取、投影
     * 
     ************************************************************/


    /// <summary>
    /// 获取每个区域的面积信息
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <returns></returns>
    public int[] ImageArea(Bitmap b)
    {
      // 将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行区域标记
      ushort[,] Sign = ImageSign(srcGray);

      // 区域面积
      int[] Area = ImageArea(Sign);

      return Area;
    } // end of ImageArea


    /// <summary>
    /// 获取每个区域的周长信息
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <returns></returns>
    public int[] ImagePerimeter(Bitmap b)
    {
      // 将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行区域标记
      ushort[,] Sign = ImageSign(srcGray);

      // 区域周长
      int[] Perimeter = ImagePerimeter(Sign);

      return Perimeter;
    } // end of ImagePerimeter


    /// <summary>
    /// 按指定的区域号绘制出对应的区域
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <param name="Region">区域号</param>
    /// <param name="showContour">指定bool值，是显示轮廓线，否显示区域块</param>
    /// <returns></returns>
    public Bitmap ImageRegion(Bitmap b, ushort[] Region, bool showContour)
    {
      // 将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行区域标记
      ushort[,] Sign = ImageSign(srcGray);

      // 按轮廓线进行显示
      if (showContour)
        Sign = ContourTrace(Sign);

      int len = Region.Length;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            ushort sign = Sign[x, y];
            bool showRegion = false;

            for (int i = 0; i < len; i++)
            {
              if (sign == Region[i])
              {
                showRegion = true;
                break;
              }
            } // i

            // 绘制区域
            if (showRegion)
            {
              p[0] = p[1] = p[2] = 0;
            }
            else
            {
              p[0] = p[1] = p[2] = 255;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of ImageRegion


    /// <summary>
    /// 消除小区域
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <param name="percent">区域面积占图像面积的百分比[0, 100]</param>
    /// <returns></returns>
    public Bitmap ClearSmallArea(Bitmap b, int percent)
    {
      // 将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行区域标记
      ushort[,] Sign = ImageSign(srcGray);

      // 区域面积
      int[] Area = ImageArea(Sign);

      int width = b.Width;
      int height = b.Height;

      // 面积阈值
      int area = width * height * percent / 100;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            ushort sign = Sign[x, y];

            // 将原始背景和小区域全部变为背景
            if (sign == 0 || Area[sign] < area)
            {
              p[0] = p[1] = p[2] = 255;
            }
            else
            {
              p[0] = p[1] = p[2] = 0;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of ClearSmallArea


    /// <summary>
    /// 轮廓跟踪
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <returns></returns>
    public Bitmap ContourTrace(Bitmap b)
    {
      // 将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行区域标记
      ushort[,] Sign = ImageSign(srcGray);

      // 轮廓跟踪
      ushort[,] Boundary = ContourTrace(Sign);

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            if (Boundary[x, y] != 0)
            {
              p[0] = p[1] = p[2] = 0;
            }
            else
            {
              p[0] = p[1] = p[2] = 255;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of ContourTrace


    /// <summary>
    /// 轮廓提取
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <returns></returns>
    public Bitmap ContourPick(Bitmap b)
    {
      // 将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 轮廓提取
      byte[,] dstGray = ContourPick(srcGray);

      // 转换为灰度图像
      return Array2Image(dstGray);
    } // end of ContourPick


    /// <summary>
    /// 图像投影
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <param name="isHorz">是否水平投影</param>
    /// <returns></returns>
    public Bitmap Project(Bitmap b, bool isHorz)
    {
      // 将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 图像投影
      byte[,] dstGray = Project(srcGray, isHorz);

      return Array2Image(dstGray);
    } // end of Project


  }
}
