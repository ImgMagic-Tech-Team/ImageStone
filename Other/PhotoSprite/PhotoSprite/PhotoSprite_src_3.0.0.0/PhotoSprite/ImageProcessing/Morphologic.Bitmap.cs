using System;
using System.Drawing;

namespace PhotoSprite.ImageProcessing
{
  public partial class Morphologic : GrayProcessing
  {
    /************************************************************
     * 
     * 水平、垂直、十字型腐蚀
     * 水平、垂直、十字型膨胀
     * 开、闭运算
     * 细化、粗化
     * 
     ************************************************************/

    
    /// <summary>
    /// 水平腐蚀
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <returns></returns>
    public Bitmap ErosionHorz(Bitmap b)
    {
      // 先将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行水平腐蚀处理
      byte[,] dstGray = ErosionHorz(srcGray);

      b.Dispose();

      // 转换为灰度图像
      return Array2Image(dstGray);
    } // end of ErosionHorz


    /// <summary>
    /// 垂直腐蚀
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <returns></returns>
    public Bitmap ErosionVert(Bitmap b)
    {
      // 先将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行垂直腐蚀处理
      byte[,] dstGray = ErosionVert(srcGray);

      b.Dispose();

      // 转换为灰度图像
      return Array2Image(dstGray);
    } // end of ErosionVert


    /// <summary>
    /// 十字型腐蚀
    /// </summary>
    /// <param name="b">二值位图流</param>
    /// <returns></returns>
    public Bitmap ErosionCross(Bitmap b)
    {
      // 先将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行十字型腐蚀处理
      byte[,] dstGray = ErosionCross(srcGray);

      b.Dispose();

      // 转换为灰度图像
      return Array2Image(dstGray);
    } // end of ErosionCross


    /// <summary>
    /// 水平膨胀
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap DilationHorz(Bitmap b)
    {
      // 先将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行水平膨胀处理
      byte[,] dstGray = DilationHorz(srcGray);

      b.Dispose();

      // 转换为灰度图像
      return Array2Image(dstGray);
    } // end of DilationHorz


    /// <summary>
    /// 垂直膨胀
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap DilationVert(Bitmap b)
    {
      // 先将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行垂直膨胀处理
      byte[,] dstGray = DilationVert(srcGray);

      b.Dispose();

      // 转换为灰度图像
      return Array2Image(dstGray);
    } // end of DilationVert


    /// <summary>
    /// 十字型膨胀
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap DilationCross(Bitmap b)
    {
      // 先将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行十字型膨胀处理
      byte[,] dstGray = DilationCross(srcGray);

      b.Dispose();

      // 转换为灰度图像
      return Array2Image(dstGray);
    } // end of DilationCross


    /// <summary>
    /// 开运算
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Opening(Bitmap b)
    {
      // 先腐蚀，后膨胀
      b = ErosionCross(b);
      b = DilationCross(b);

      return b;
    } // end of Opening


    /// <summary>
    /// 闭运算
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Closing(Bitmap b)
    {
      // 先膨胀，后腐蚀
      b = DilationCross(b);
      b = ErosionCross(b);

      return b;
    } // end of Closing


    /// <summary>
    /// 细化
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Thinning(Bitmap b)
    {
      // 先将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行细化处理
      byte[,] dstGray = Thinning(srcGray);

      b.Dispose();

      // 转换为灰度图像
      return Array2Image(dstGray);
    } // end of Thinning


    /// <summary>
    /// 粗化
    /// </summary>
    /// <param name="b">位图流</param>
    /// <returns></returns>
    public Bitmap Thickening(Bitmap b)
    {
      // 先将原始二值图转化为二维数组
      byte[,] srcGray = Image2Array(b);

      // 进行粗化处理
      byte[,] dstGray = Thickening(srcGray);

      b.Dispose();

      // 转换为灰度图像
      return Array2Image(dstGray);
    } // end of Thickening


  }
}
