using System;
using System.Drawing;
using System.Drawing.Imaging;
using PhotoSprite.ImageProcessing;

namespace PhotoSprite.ImageFormat
{
  /// <summary>
  /// PhotoSprite ͼ���ʽ��
  /// </summary>
  public class PsfFormat : ImageInfo
  {
    /// <summary>
    /// �� PhotoSprite ͼ���ʽ��׼�� PSF ͼ���ļ�
    /// </summary>
    /// <param name="srcFile">PSF �ļ���</param>
    /// <returns></returns>
    public Bitmap Open(string srcFile)
    {
      // ��ͼ���ļ�
      System.IO.FileStream myFS = new System.IO.FileStream(srcFile,
        System.IO.FileMode.Open, System.IO.FileAccess.Read);

      // �ļ�ͷ��Ϣ������
      byte[] HeaderBuffer = new byte[9];

      // ��ȡ�ļ�ͷ��Ϣ
      myFS.Read(HeaderBuffer, 0, 9);

      // ��ȡͼ��ʶ����
      string Mark = "";
      Mark += (char)HeaderBuffer[0];
      Mark += (char)HeaderBuffer[1];
      Mark += (char)HeaderBuffer[2];

      // ��ȡ�汾��
      byte Version = HeaderBuffer[3];

      // ��ȡͼ����
      int width = HeaderBuffer[4] * 256 + HeaderBuffer[5];
      int height = HeaderBuffer[6] * 256 + HeaderBuffer[7];

      // ͼ�����ݻ�����
      byte[] DataBuffer = new byte[width * height * BPP];
      int pDataBuffer = 0;
      int LenDataBuffer = (int)myFS.Length - 9;

      // ��ȡͼ����������
      myFS.Read(DataBuffer, 0, LenDataBuffer);

      myFS.Close();

      // ���潫�ļ���ת��Ϊͼ��λͼ��
      Bitmap b = new Bitmap(width, height);

      // ����ǲ���ʶ��汾���򷵻�һ�հ�ͼ��
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
    /// �� PhotoSprite ͼ���ʽ��׼���� PSF ͼ���ļ�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="dstFile">Ŀ���ļ���</param>
    /// <returns></returns>
    public bool Save(Bitmap b, string dstFile)
    {
      bool isSaveOK = true;

      int width = b.Width;
      int height = b.Height;

      // ͼ�����ݻ�����
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

      // �ļ�ͷ��Ϣ������
      byte[] HeaderBuffer = new byte[9];
      HeaderBuffer[0] = (byte)'P';
      HeaderBuffer[1] = (byte)'S';
      HeaderBuffer[2] = (byte)'F';
      HeaderBuffer[3] = 1;    // �汾��
      HeaderBuffer[4] = (byte)(width / 256);
      HeaderBuffer[5] = (byte)(width % 256);
      HeaderBuffer[6] = (byte)(height / 256);
      HeaderBuffer[7] = (byte)(height % 256);
      HeaderBuffer[8] = 0xFF; // alpha ���

      try
      {
        // ��ʼд PSF ��ʽ�ļ�
        System.IO.FileStream myFS = new System.IO.FileStream(dstFile,
          System.IO.FileMode.Create, System.IO.FileAccess.Write);

        // д���ļ�ͷ��Ϣ
        myFS.Write(HeaderBuffer, 0, 9);

        // д��ͼ����������
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
