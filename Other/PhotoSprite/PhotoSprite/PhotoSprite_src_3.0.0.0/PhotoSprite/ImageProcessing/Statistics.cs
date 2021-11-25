using System;

namespace PhotoSprite.ImageProcessing
{
  /// <summary>
  /// 数据统计类
  /// </summary>
  public class Statistics
  {
    private int[] Sequence;
    private int length;
    private int min, max;
    private int minIndex, maxIndex;

    /// <summary>
    /// 获取数组序列
    /// </summary>
    public int[] Value
    {
      get 
      {
        return Sequence;
      }
    }

    /// <summary>
    /// 获取经均衡化后的数组序列
    /// </summary>
    public byte[] Equalizer
    {
      get 
      {
        // 先计算概率
        double[] Probability = this.Probability;

        // S 为亮度级的定积分，即离散图像的亮度变换函数
        double[] S = new double[256];

        // L 数组用于记录均衡化后的新亮度值
        byte[] L = new byte[256];

        // 进行均衡化处理
        for (int i = 0; i < 256; i++)
        {
          if (i == 0)
          {
            S[0] = Probability[0];
          }
          else
          {
            S[i] = S[i - 1] + Probability[i];
          }

          L[i] = (byte)(255 * S[i] + 0.5);
        } // i

        return L;
      }
    }

    /// <summary>
    /// 获取概率
    /// </summary>
    public double[] Probability
    {
      get
      {
        double total = (double)this.Sum;
        double[] probability = new double[256];

        // 计算各亮度级概率密度
        for (int i = 0; i < 256; i++)
        {
          probability[i] = Sequence[i] / total;
        } // i

        return probability;
      }
    }

    /// <summary>
    /// 获取序列累加和
    /// </summary>
    public int Sum
    {
      get
      {
        int total = 0;

        for (int i = 0; i < length; i++)
        {
          total += Sequence[i];
        } // i

        return total;
      }
    }

    /// <summary>
    /// 获取加权平均数
    /// </summary>
    public double Mean
    {
      get
      {
        int mean = 0;

        for (int i = 0; i < length; i++)
        {
          mean += i * Sequence[i];
        } // i

        return (double)mean / (double)this.Sum;
      }
    }

    /// <summary>
    /// 获取标准偏差
    /// </summary>
    public double StdDev
    {
      get
      {
        double mean = this.Mean;
        int total = this.Sum;

        double stddev = 0;
        for (int i = 0; i < length; i++)
        {
          double t = (double)i - mean;
          stddev += t * t * Sequence[i];
        } // i

        return Math.Sqrt(stddev / total);
      }
    }

    /// <summary>
    /// 获取中值
    /// </summary>
    public int Median
    {
      get 
      {
        int halfTotal = this.Sum / 2;

        // 查找中值
        int total = 0;
        int median = 0;
        while (total < halfTotal)
        {
          total += Sequence[median];
          median++;
        } // while

        return median - 1;
      }
    }
    
    /// <summary>
    /// 获取最大值
    /// </summary>
    public int Maximum
    {
      get
      { 
        return max;
      }
    }

    /// <summary>
    /// 获取最小值
    /// </summary>
    public int Minimum
    {
      get 
      {
        return min;
      }
    }

    /// <summary>
    /// 获取最大值索引
    /// </summary>
    public int MaxIndex
    {
      get
      {
        return maxIndex;
      }
    }

    /// <summary>
    /// 获取最小值索引
    /// </summary>
    public int MinIndex
    {
      get
      {
        return minIndex;
      }
    }


    /// <summary>
    /// 建立数据统计资料
    /// </summary>
    /// <param name="sequence">数组序列</param>
    public Statistics(int[] sequence)
    {
      this.Sequence = sequence;
      length = Sequence.Length;

      MaxMin();
    } // end of Statistics


    /// <summary>
    /// 求最大值、最小值
    /// </summary>
    private void MaxMin()
    {
      max = min = Sequence[0];
      maxIndex = minIndex = 0;

      // 计算最大、最小值
      for (int i = 1; i < length; i++)
      {
        int t = Sequence[i];

        if (t > max)
        {
          max = t;
          maxIndex = i;
        }

        if (t < min)
        {
          min = t;
          minIndex = i;
        }
      } // i
    } // end of MaxMin


  }
}
