using System;
using System.Collections;
using System.Drawing;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 图像分割处理类
  /// </summary>
  public partial class Segmentation : GrayProcessing
  {
    /************************************************************
     * 
     * 图像分割处理类核心算法
     * 
     * 标记、面积、周长、轮廓跟踪、提取、投影
     * 
     ************************************************************/


    /// <summary>
    /// 用新的标记号替换掉标记数组中旧的标记号
    /// </summary>
    /// <param name="Sign">二值图像标记数组</param>
    /// <param name="srcSign">原始标记号</param>
    /// <param name="dstSign">目标标记号</param>
    private void ReplaceSign(ref ushort[,] Sign, ushort srcSign, ushort dstSign)
    {
      int width = Sign.GetLength(0);
      int height = Sign.GetLength(1);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (Sign[x, y] == srcSign)
            Sign[x, y] = dstSign;
        } // x
      } // y
    } // end of ReplaceSign


    /// <summary>
    /// 标记一幅二值图像
    /// </summary>
    /// <param name="b">二值图数据数组</param>
    /// <returns></returns>
    public ushort[,] ImageSign(byte[,] b)
    {
      int width = b.GetLength(0);
      int height = b.GetLength(1);

      // 标记号，最多可以标记 65536 个不同的连通区域
      // 注意标记号从 1 开始依次递增，标记号 0 代表背景
      ushort signNo = 1;

      // 用堆栈记录所有空标记
      Stack Seat = new Stack();

      // 二值图像连通区域标识，存储的是区域标识，而非图像数据
      ushort[,] Sign = new ushort[width, height];


      // 初始化最顶行标记
      for (int x = 0; x < width; x++)
      {
        if (b[x, 0] != 0) continue;

        // 处理所有连续的黑点
        while (x < width && b[x, 0] == 0)
        {
          Sign[x, 0] = signNo;
          x++;
        } // while

        signNo++;
      } // x


      // 处理最左列及最右列标记
      for (int y = 1; y < height; y++)
      {
        // 第左列
        if (b[0, y] == 0)
        {
          if (b[0, y - 1] == 0)
            Sign[0, y] = Sign[0, y - 1];
          else
            Sign[0, y] = signNo++;
        }

        // 最右列
        if (b[width - 1, y] == 0)
        {
          if (b[width - 1, y - 1] == 0)
            Sign[width - 1, y] = Sign[width - 1, y - 1];
          else
            Sign[width - 1, y] = signNo++;
        }
      } // y


      // 上面已经处理了图像最顶行、最左列、最右列，
      // 故下面只处理排除这三行后的主图像区
      int topRect = 1;
      int bottomRect = height;
      int leftRect = 1;
      int rightRect = width - 1;

      // 从左到右开始标记
      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // 如果当前点不为黑点，则跳过不处理
          if (b[x, y] != 0) continue;

          // 右上
          if (b[x + 1, y - 1] == 0)
          {
            // 将当前点置为与右上点相同的标记
            ushort sign = Sign[x, y] = Sign[x + 1, y - 1];

            // 当左前点为黑点，且左前点的标记与右上点的标记不同时
            if (b[x - 1, y] == 0 && Sign[x - 1, y] != sign)
            {
              // 进栈：记录左前点标记，因为该标记将被替换掉
              Seat.Push(Sign[x - 1, y]);

              // 用右上点的标记号替换掉所有与左前点标记号相同的点
              ReplaceSign(ref Sign, Sign[x - 1, y], sign);
            }

            // 当左上点为黑点，且左上点的标记与右上点的标记不同时
            else if (b[x - 1, y - 1] == 0 && Sign[x - 1, y - 1] != sign)
            {
              // 进栈：记录左上点标记，因为该标记将被替换掉
              Seat.Push(Sign[x - 1, y - 1]);

              // 用右上点的标记号替换掉所有与左上点标记号相同的点
              ReplaceSign(ref Sign, Sign[x - 1, y - 1], sign);
            }
          } // 右上完

          // 正上
          else if (b[x, y - 1] == 0)
          {
            // 将当前点置为与正上点相同的标记
            Sign[x, y] = Sign[x, y - 1];
          }

          // 左上
          else if (b[x - 1, y - 1] == 0)
          {
            // 将当前点置为与左上点相同的标记
            Sign[x, y] = Sign[x - 1, y - 1];
          }

          // 左前
          else if (b[x - 1, y] == 0)
          {
            // 将当前点置为与左前点相同的标记
            Sign[x, y] = Sign[x - 1, y];
          }

          // 右上、正上、左上及左前四点均不为黑点，即表示新区域开始
          else
          {
            // 避免区域数超过 0xFFFF
            if (signNo >= 0xFFFF)
              return Sign;

            // 如果堆栈里无空标记，
            // 则使用新标记，否则使用该空标记
            if (Seat.Count == 0)
              Sign[x, y] = signNo++;
            else
              Sign[x, y] = (ushort)Seat.Pop(); // 出栈
          }

        } // x
      } // y


      // 堆栈里存在空标记，则使用完没有空标记
      while (Seat.Count > 0)
      {
        ReplaceSign(ref Sign, (ushort)(--signNo), (ushort)Seat.Pop());
      } // while

      return Sign;
    } // end of ImageSign


    /// <summary>
    /// 区域面积
    /// </summary>
    /// <param name="Sign">二值图像标记数组</param>
    /// <returns></returns>
    public int[] ImageArea(ushort[,] Sign)
    {
      int width = Sign.GetLength(0);
      int height = Sign.GetLength(1);

      // 找出最大标记号，即找出区域数
      int max = 0;
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (Sign[x, y] > max)
            max = Sign[x, y];
        } // x
      } // y

      // 面积统计数组
      int[] Area = new int[max + 1];

      // 计算区域面积
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          Area[Sign[x, y]]++;
        } // x
      } // y

      return Area;
    } // end of ImageArea


    /// <summary>
    /// 区域周长
    /// </summary>
    /// <param name="Sign">二值图像标记数组</param>
    /// <returns></returns>
    public int[] ImagePerimeter(ushort[,] Sign)
    {
      // 进行轮廓跟踪，划出轮廓线
      Sign = ContourTrace(Sign);

      int width = Sign.GetLength(0);
      int height = Sign.GetLength(1);

      // 找出最大标记号，即找出区域数
      int max = 0;
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (Sign[x, y] > max)
            max = Sign[x, y];
        } // x
      } // y

      // 周长统计数组
      int[] Perimeter = new int[max + 1];

      // 计算区域周长
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          Perimeter[Sign[x, y]]++;
        } // x
      } // y

      return Perimeter;
    } // end of ImagePerimeter


    /// <summary>
    /// 轮廓跟踪
    /// </summary>
    /// <param name="b">区域标记数组</param>
    /// <returns></returns>
    public ushort[,] ContourTrace(ushort[,] Sign)
    {
      int width = Sign.GetLength(0);
      int height = Sign.GetLength(1);

      // 面积及区域数
      int[] Area = ImageArea(Sign);
      int areaNumber = Area.Length;

      // 图像边界
      ushort[,] Boundary = new ushort[width, height];

      // 找到起始点或回到起始点？
      bool findStartPoint = false;

      // 扫描到一个边界点？
      bool findPoint;

      // 起始边界点及当前边界点
      Point Start = new Point(0, 0);
      Point Current = new Point(0, 0);

      // 起始点在左上方，扫描方向为逆时针
      Point[] Direct = new Point[8];
      Direct[0].X = -1; Direct[0].Y = 1;    // SW
      Direct[1].X = 0;  Direct[1].Y = 1;    // S
      Direct[2].X = 1;  Direct[2].Y = 1;    // SE
      Direct[3].X = 1;  Direct[3].Y = 0;    // E
      Direct[4].X = 1;  Direct[4].Y = -1;   // NE
      Direct[5].X = 0;  Direct[5].Y = -1;   // N
      Direct[6].X = -1; Direct[6].Y = -1;   // NW
      Direct[7].X = -1; Direct[7].Y = 0;    // W

      // 先找到最左上方的边界点
      for (int sign = 1; sign < areaNumber; sign++)
      {
        findStartPoint = false;

        for (int y = 0; y < height && !findStartPoint; y++)
        {
          for (int x = 0; x < width && !findStartPoint; x++)
          {
            if (Sign[x, y] == sign)
            {
              findStartPoint = true;

              // 记录下当前边界起始点
              Start = new Point(x, y);
              Boundary[x, y] = (ushort)sign;
            }
          } // x
        } // y

        // 当前扫描方向
        int direction = 0;

        // 从初始点开始扫描
        Current = Start;

        // 开始跟踪边界
        findStartPoint = false;
        while (!findStartPoint)
        {
          // 扫描次数
          int searchNumber = 0;

          findPoint = false;
          while (!findPoint)
          {
            // 沿扫描方向查看一个像素
            int x = Current.X + Direct[direction].X;
            int y = Current.Y + Direct[direction].Y;

            if ((x >= 0 && x < width && y >= 0 && y < height) && (Sign[x, y] == sign))
            {
              // 找到边界点
              findPoint = true;

              Current.X += Direct[direction].X;
              Current.Y += Direct[direction].Y;

              // 回到起始点，边界扫描完毕
              if (Current.X == Start.X && Current.Y == Start.Y)
              {
                findStartPoint = true;
                break;
              }

              Boundary[Current.X, Current.Y] = (ushort)sign;

              // 扫描方向顺时针旋转两格
              direction -= 2;
              direction += 8; // 避免出现负数
              direction %= 8;
            }
            else
            {
              // 扫描方向逆时针旋转一格
              direction++;
              direction %= 8;

              searchNumber++;
              if (searchNumber > 8)
              {
                // 为孤立点
                findStartPoint = true;
                findPoint = true;
              }
            }

          } // findPoint
        } // findStartPoint
      } // sign

      return Boundary;
    } // end of ContourTrace


    /// <summary>
    /// 轮廓提取
    /// </summary>
    /// <param name="b">二值图数据数组</param>
    /// <returns></returns>
    public byte[,] ContourPick(byte[,] b)
    {
      int width = b.GetLength(0);
      int height = b.GetLength(1);

      byte[,] dst = new byte[width, height];

      // 不考虑图像最外围一圈
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          if (b[x, y] == 0)
          {
            int sum =
              b[x - 1, y - 1] + b[x, y - 1] + b[x + 1, y - 1] +
              b[x - 1, y    ]               + b[x + 1, y    ] +
              b[x - 1, y + 1] + b[x, y + 1] + b[x + 1, y + 1];

            // 如果周围八点全为黑点，则置当前点为白点
            if (sum == 0)
              dst[x, y] = 255;
          }
          else
          {
            dst[x, y] = 255;
          }
        } // x
      } // y

      return dst;
    } // end of ContourPick


    /// <summary>
    /// 图像投影
    /// </summary>
    /// <param name="b">二值图数据数组</param>
    /// <param name="isHorz">是否水平投影</param>
    /// <returns></returns>
    public byte[,] Project(byte[,] b, bool isHorz)
    {
      int width = b.GetLength(0);
      int height = b.GetLength(1);

      // 初始化为白色
      byte[,] dst = InitArray(width, height, 255);

      if (isHorz)
      {
        for (int y = 0; y < height; y++)
        {
          int sum = 0;
          for (int x = 0; x < width; x++)
          {
            if (b[x, y] == 0)
              sum++;
          } // x

          for (int x = 0; x < sum; x++)
          {
            dst[x, y] = 0;
          } // x
        } // y
      }
      else
      {
        for (int x = 0; x < width; x++)
        {
          int sum = 0;
          for (int y = 0; y < height; y++)
          {
            if (b[x, y] == 0)
              sum++;
          } // x

          for (int y = height-sum; y < height; y++)
          {
            dst[x, y] = 0;
          } // x
        } // y
      }

      return dst;
    } // end of Project


  }
}
