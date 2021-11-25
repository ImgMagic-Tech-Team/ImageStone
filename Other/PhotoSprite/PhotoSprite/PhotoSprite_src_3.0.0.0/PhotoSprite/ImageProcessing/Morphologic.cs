using System;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 形态学处理类
  /// </summary>
  public partial class Morphologic
  {
    /************************************************************
     * 
     * 形态学处理类核心算法
     * 
     * 水平、垂直、十字型腐蚀
     * 水平、垂直、十字型膨胀
     * 细化、粗化
     * 
     ************************************************************/


    /// <summary>
    /// 水平腐蚀
    /// </summary>
    /// <param name="src">二值化数组</param>
    /// <returns></returns>
    public byte[,] ErosionHorz(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // 初始化目标数组为 255，即白色
      byte[,] dst = InitArray(width, height, 255);

      // 1*3 结构元素
      // 0 0 0
      // 由于使用 1*3 的结构元素，为防止越界，所以不处理最左边和最右边的两列像素
      int topRect = 0;
      int bottomRect = height;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // 假设目标图像中当前点为黑色
          byte c = 0;

          // 如果原图像中的 (-1,0), (0,0), (1,0) 三个点之一有白点，
          // 则将目标图像中的当前点，即(0,0)点赋予白色
          for (int i = -1; i <= 1; i++)
          {
            if (src[x + i, y] > 127)
            {
              c = 255;
              break;
            }
          } // i

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of ErosionHorz


    /// <summary>
    /// 垂直腐蚀
    /// </summary>
    /// <param name="src">二值化数组</param>
    /// <returns></returns>
    public byte[,] ErosionVert(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // 初始化目标数组为 255，即白色
      byte[,] dst = InitArray(width, height, 255);

      // 3*1 结构元素
      // 0
      // 0
      // 0
      // 由于使用 3*1 的结构元素，为防止越界，所以不处理最上边和最下边的两行像素
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 0;
      int rightRect = width;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // 假设目标图像中当前点为黑色
          byte c = 0;

          // 如果原图像中的 (0,-1), (0,0), (0,1) 三个点之一有白点，
          // 则将目标图像中的当前点，即(0,0)点赋予白色
          for (int j = -1; j <= 1; j++)
          {
            if (src[x, y + j] > 127)
            {
              c = 255;
              break;
            }
          } // j

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of ErosionVert


    /// <summary>
    /// 十字型腐蚀
    /// </summary>
    /// <param name="src">二值化数组</param>
    /// <returns></returns>
    public byte[,] ErosionCross(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // 初始化目标数组为 255，即白色
      byte[,] dst = InitArray(width, height, 255);

      // 3*3 结构元素
      // 1 0 1
      // 0 0 0
      // 1 0 1
      // 由于使用 3*3 的结构元素，为防止越界，所以不处理最上、下、左、右四边的像素
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // 假设目标图像中当前点为黑色
          byte c = 0;

          // 如果原图像中的 (-1,0), (0,0), (1,0), (0,-1), (0, 1) 五个点之一有白点，
          // 则将目标图像中的当前点，即(0,0)点赋予白色
          for (int i = -1; i <= 1; i++)
          {
            for (int j = -1; j <= 1; j++)
            {
              // 根据 3*3 十字型结构元素，
              // 不必判断当前像素左上、右上、左下、右下四角上的四点
              if ((i + 2) % 2 == 1 && (j + 2) % 2 == 1)
                continue;

              if (src[x + i, y + j] > 127)
              {
                c = 255;
                break;
              }
            } // j
          } // i

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of ErosionCross


    /// <summary>
    /// 水平膨胀
    /// </summary>
    /// <param name="src">二值化数组</param>
    /// <returns></returns>
    public byte[,] DilationHorz(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // 初始化目标数组为 255，即白色
      byte[,] dst = InitArray(width, height, 255);

      // 1*3 结构元素
      // 0 0 0
      // 由于使用 1*3 的结构元素，为防止越界，所以不处理最左边和最右边的两列像素
      int topRect = 0;
      int bottomRect = height;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // 假设目标图像中当前点为白色
          byte c = 255;

          // 如果原图像中的 (-1,0), (0,0), (1,0) 三个点之一有黑点，
          // 则将目标图像中的当前点，即(0,0)点赋予黑色
          for (int i = -1; i <= 1; i++)
          {
            if (src[x + i, y] < 128)
            {
              c = 0;
              break;
            }
          } // i

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of DilationHorz


    /// <summary>
    /// 垂直膨胀
    /// </summary>
    /// <param name="src">二值化数组</param>
    /// <returns></returns>
    public byte[,] DilationVert(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // 初始化目标数组为 255，即白色
      byte[,] dst = InitArray(width, height, 255);

      // 3*1 结构元素
      // 0
      // 0
      // 0
      // 由于使用 3*1 的结构元素，为防止越界，所以不处理最上边和最下边的两行像素
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 0;
      int rightRect = width;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // 假设目标图像中当前点为白色
          byte c = 255;

          // 如果原图像中的 (-1,0), (0,0), (1,0) 三个点之一有黑点，
          // 则将目标图像中的当前点，即(0,0)点赋予黑色
          for (int j = -1; j <= 1; j++)
          {
            if (src[x, y + j] < 128)
            {
              c = 0;
              break;
            }
          } // j

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of DilationVert


    /// <summary>
    /// 十字型膨胀
    /// </summary>
    /// <param name="src">二值化数组</param>
    /// <returns></returns>
    public byte[,] DilationCross(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // 初始化目标数组为 255，即白色
      byte[,] dst = InitArray(width, height, 255);

      // 3*3 结构元素
      // 1 0 1
      // 0 0 0
      // 1 0 1
      // 由于使用 3*3 的结构元素，为防止越界，所以不处理最上、下、左、右四边的像素
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // 假设目标图像中当前点为白色
          byte c = 255;

          // 如果原图像中 (-1,0), (0,0), (1,0), (0,-1), (0, 1) 五个点之一有黑点，
          // 则将目标图像中的当前点，即(0,0)点赋予黑色
          for (int i = -1; i <= 1; i++)
          {
            for (int j = -1; j <= 1; j++)
            {
              // 根据 3*3 结构元素，
              // 不必判断当前像素左上、右上、左下、右下四角上的四点
              if ((i + 2) % 2 == 1 && (j + 2) % 2 == 1)
                continue;

              if (src[x + i, y + j] < 128)
              {
                c = 0;
                break;
              }
            } // j
          } // i

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of DilationCross


    /// <summary>
    /// 细化
    /// </summary>
    /// <param name="src">二值化数组</param>
    /// <returns></returns>
    public byte[,] Thinning(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // 需要细化吗？
      bool needThinning = true;

      byte[,] S = new byte[5, 5];
      int num = 0;

      // 5*5 结构元素
      // 由于使用 5*5 的结构元素，为防止越界，所以不处理外围的 2 行、 2 列像素
      int topRect = 2;
      int bottomRect = height - 2;
      int leftRect = 2;
      int rightRect = width - 2;

      while (needThinning)
      {
        needThinning = false;

        // 初始化目标数组为 255，即白色
        byte[,] dst = InitArray(width, height, 255);

        for (int y = topRect; y < bottomRect; y++)
        {
          for (int x = leftRect; x < rightRect; x++)
          {
            // 如果原图像中当前点为白色，则跳过
            if (src[x, y] > 127)
              continue;

            // 获得当前点相邻的 5*5 区域内像素值，
            // 注意：白色用 0 代表，黑色用 1 代表
            for (int i = -2; i <= 2; i++)
            {
              for (int j = -2; j <= 2; j++)
              {
                if (src[x + j, y + i] > 127)
                  S[i + 2, j + 2] = 0; // 白色
                else
                  S[i + 2, j + 2] = 1; // 黑色
              } // j
            } // i


            // 判断条件 1 是否成立
            num = S[1, 1] + S[1, 2] + S[1, 3] +
                  S[2, 1]           + S[2, 3] +
                  S[3, 1] + S[3, 2] + S[3, 3];
            if (!(num >= 2 && num <= 6))
            {
              dst[x, y] = 0;
              continue;
            }


            // 判断条件 2 是否成立
            num = 0;

            if (S[1, 2] == 0 && S[1, 1] == 1) num++;
            if (S[1, 1] == 0 && S[2, 1] == 1) num++;
            if (S[2, 1] == 0 && S[3, 1] == 1) num++;
            if (S[3, 1] == 0 && S[3, 2] == 1) num++;
            if (S[3, 2] == 0 && S[3, 3] == 1) num++;
            if (S[3, 3] == 0 && S[2, 3] == 1) num++;
            if (S[2, 3] == 0 && S[1, 3] == 1) num++;
            if (S[1, 3] == 0 && S[1, 2] == 1) num++;

            if (!(num == 1))
            {
              dst[x, y] = 0;
              continue;
            }


            // 判断条件 3 是否成立
            if (!(S[1, 2] * S[2, 1] * S[2, 3] == 0))
            {
              num = 0;

              if (S[0, 2] == 0 && S[0, 1] == 1) num++;
              if (S[0, 1] == 0 && S[1, 1] == 1) num++;
              if (S[1, 1] == 0 && S[2, 1] == 1) num++;
              if (S[2, 1] == 0 && S[2, 2] == 1) num++;
              if (S[2, 2] == 0 && S[2, 3] == 1) num++;
              if (S[2, 3] == 0 && S[1, 3] == 1) num++;
              if (S[1, 3] == 0 && S[0, 3] == 1) num++;
              if (S[0, 3] == 0 && S[0, 2] == 1) num++;

              if (num == 1)
              {
                dst[x, y] = 0;
                continue;
              }
            }


            // 判断条件 4 是否成立
            if (!(S[1, 2] * S[2, 1] * S[3, 2] == 0))
            {
              num = 0;

              if (S[1, 1] == 0 && S[1, 0] == 1) num++;
              if (S[1, 0] == 0 && S[2, 0] == 1) num++;
              if (S[2, 0] == 0 && S[3, 0] == 1) num++;
              if (S[3, 0] == 0 && S[3, 1] == 1) num++;
              if (S[3, 1] == 0 && S[3, 2] == 1) num++;
              if (S[3, 2] == 0 && S[2, 2] == 1) num++;
              if (S[2, 2] == 0 && S[1, 2] == 1) num++;
              if (S[1, 2] == 0 && S[1, 1] == 1) num++;

              if (num == 1)
              {
                dst[x, y] = 0;
                continue;
              }
            }


            // 如果条件均满足，则删除该点
            dst[x, y] = 255;

            // 继续细化！
            needThinning = true;
          } // x
        } // y

        // 复制细化后的图像作为新一轮细化对象
        src = (byte[,])dst.Clone();
      } // while

      return src;
    } // end of Thinning


    /// <summary>
    /// 粗化
    /// </summary>
    /// <param name="src">二值化数组</param>
    /// <returns></returns>
    public byte[,] Thickening(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // 对已二值化像素各颜色分量进行求补
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          src[x, y] ^= 255;
        } // x
      } // y

      return Thinning(src);
    } // end of Thickening


  }
}
