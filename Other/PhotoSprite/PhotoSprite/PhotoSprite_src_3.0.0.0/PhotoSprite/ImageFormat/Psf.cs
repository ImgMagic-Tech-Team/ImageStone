using System;
using System.Drawing;
using System.Drawing.Imaging;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.ImageFormat
{
  /// <summary>
  /// PhotoSprite 图像格式类
  /// </summary>
  public class PsfFormat : ImageInfo
  {
    /// <summary>
    /// 按 PhotoSprite 图像格式标准打开 PSF 图像文件
    /// </summary>
    /// <param name="srcFile">PSF 文件名</param>
    /// <returns></returns>
    public Bitmap Open(string srcFile)
    {
      // 打开图像文件
      System.IO.FileStream myFS = new System.IO.FileStream(srcFile,
        System.IO.FileMode.Open, System.IO.FileAccess.Read);

      // 文件头信息缓冲区
      byte[] HeaderBuffer = new byte[9];

      // 读取文件头信息
      myFS.Read(HeaderBuffer, 0, 9);

      // 获取图像识别标记
      string Mark = "";
      Mark += (char)HeaderBuffer[0];
      Mark += (char)HeaderBuffer[1];
      Mark += (char)HeaderBuffer[2];

      // 获取版本号
      byte Version = HeaderBuffer[3];

      // 获取图像宽高
      int width = HeaderBuffer[4] * 256 + HeaderBuffer[5];
      int height = HeaderBuffer[6] * 256 + HeaderBuffer[7];

      // 图像数据缓冲区
      byte[] DataBuffer = new byte[width * height * BPP];
      int pDataBuffer = 0;
      int LenDataBuffer = (int)myFS.Length - 9;

      // 读取图像数据内容
      myFS.Read(DataBuffer, 0, LenDataBuffer);

      myFS.Close();

      // 下面将文件流转换为图像位图流
      Bitmap b = new Bitmap(width, height);

      // 如果是不可识别版本，则返回一空白图像
      if (Mark != "PSF" && Version != 1) return b;

      BitmapData bmData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
      int stride = bmData.Stride;
      System.IntPtr scan0 = bmData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* p = (byte*)scan0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            p[3] = DataBuffer[pDataBuffer++]; // A
            p[2] = DataBuffer[pDataBuffer++]; // R
            p[1] = DataBuffer[pDataBuffer++]; // G
            p[0] = DataBuffer[pDataBuffer++]; // B

            p += BPP;
          } //  x
          p += offset;
        } // y
      }

      b.UnlockBits(bmData);

      return b;
    } // end of Open


    /// <summary>
    /// 按 PhotoSprite 图像格式标准保存 PSF 图像文件
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="dstFile">目标文件名</param>
    /// <returns></returns>
    public bool Save(Bitmap b, string dstFile)
    {
      bool isSaveOK = true;

      int width = b.Width;
      int height = b.Height;

      // 图像数据缓冲区
      byte[] DataBuffer = new byte[width * height * BPP];
      int pDataBuffer = 0;

      BitmapData bmData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      int stride = bmData.Stride;
      System.IntPtr scan0 = bmData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* p = (byte*)scan0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            DataBuffer[pDataBuffer++] = p[3]; // A
            DataBuffer[pDataBuffer++] = p[2]; // R
            DataBuffer[pDataBuffer++] = p[1]; // G
            DataBuffer[pDataBuffer++] = p[0]; // B

            p += BPP;
          } //  x
          p += offset;
        } // y
      }

      b.UnlockBits(bmData);

      // 文件头信息缓冲区
      byte[] HeaderBuffer = new byte[9];
      HeaderBuffer[0] = (byte)'P';
      HeaderBuffer[1] = (byte)'S';
      HeaderBuffer[2] = (byte)'F';
      HeaderBuffer[3] = 1;    // 版本号
      HeaderBuffer[4] = (byte)(width / 256);
      HeaderBuffer[5] = (byte)(width % 256);
      HeaderBuffer[6] = (byte)(height / 256);
      HeaderBuffer[7] = (byte)(height % 256);
      HeaderBuffer[8] = 0xFF; // alpha 标记

      try
      {
        // 开始写 PSF 格式文件
        System.IO.FileStream myFS = new System.IO.FileStream(dstFile,
          System.IO.FileMode.Create, System.IO.FileAccess.Write);

        // 写入文件头信息
        myFS.Write(HeaderBuffer, 0, 9);

        // 写入图像数据内容
        myFS.Write(DataBuffer, 0, pDataBuffer);

        myFS.Close();
      }
      catch
      {
        isSaveOK = false;
      }

      return isSaveOK;
    } // end of Save


  }
}
