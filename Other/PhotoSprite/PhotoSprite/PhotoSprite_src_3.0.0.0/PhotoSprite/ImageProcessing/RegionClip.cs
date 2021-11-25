using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 区域修整类
  /// </summary>
  public class RegionClip
  {
    private Region region = null;

    /// <summary>
    /// 获取或设置区域
    /// </summary>
    public Region SelectedRegion
    {
      get
      {
        return region;
      }
      set
      {
        region = value;
      }
    }


    /// <summary>
    /// 初始化区域修整类
    /// </summary>
    /// <param name="region">修整区域</param>
    public RegionClip(Region region)
    {
      this.region = region;
    }


    /// <summary>
    /// 去除掉图像中指定区域
    /// </summary>
    /// <param name="b">位图</param>
    /// <returns></returns>
    public Bitmap Remove(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height);
      Graphics g = System.Drawing.Graphics.FromImage(dstImage);
      Region all = new Region(new Rectangle(0, 0, width, height));
      Region validRegion = (Region)this.region.Clone();
      validRegion.Complement(all);
      g.SetClip(validRegion, System.Drawing.Drawing2D.CombineMode.Replace);
      g.DrawImage(b, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);

      b.Dispose();
      return dstImage;
    } // end of Remove


    /// <summary>
    /// 保留下图像中指定区域
    /// </summary>
    /// <param name="b">位图</param>
    /// <returns></returns>
    public Bitmap Hold(Bitmap b)
    {
      ImageTransform it = new ImageTransform();
      return it.Crop(b, (Region)this.region.Clone());
    } // end of Hold


    /// <summary>
    /// 将背景中指定区域用前景的相应区域替换
    /// </summary>
    /// <param name="bgImage">背景</param>
    /// <param name="fgImage">前景</param>
    /// <returns></returns>
    public Bitmap Replace(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      Graphics g = System.Drawing.Graphics.FromImage(bgImage);
      g.SetClip(this.region, System.Drawing.Drawing2D.CombineMode.Replace);
      g.DrawImage(fgImage, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);

      fgImage.Dispose();
      return bgImage;
    } // end of Replace


  }
}
