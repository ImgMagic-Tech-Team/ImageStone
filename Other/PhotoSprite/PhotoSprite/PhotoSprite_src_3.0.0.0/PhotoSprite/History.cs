using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSprite
{
  /// <summary>
  /// 图像处理历史记录
  /// </summary>
  public class HistoryImage
  {
    private int current = -1;
    private string [] History;
    private string initDirectory = "";
    private int max = 0;
    private int count = 0;
    private int save = -1;

    /// <summary>
    /// 获取或设置初始化文件目录
    /// </summary>
    public string InitDirectory
    {
      get
      {
        return initDirectory;
      }
      set
      {
        initDirectory = value;
      }
    }

    /// <summary>
    /// 获取 bool 值，指示是否可以撤消
    /// </summary>
    public bool CanUndo
    {
      get
      {
        if (current > 0)
          return true;
        else
          return false;
      }
    }

    /// <summary>
    /// 获取 bool 值，指示是否可以重复
    /// </summary>
    public bool CanRedo
    {
      get
      {
        if (current < count - 1 && count != 0)
          return true;
        else
          return false;
      }
    }

    /// <summary>
    /// 获取 bool 值，指示图像是否已经修改过
    /// </summary>
    public bool IsDirty
    {
      get
      {
        if (current != save)
          return true;
        else
          return false;
      }
    }

    /// <summary>
    /// 获取最大历史记录数
    /// </summary>
    public int Max
    {
      get
      {
        return max;
      }
    }

    /// <summary>
    /// 获取当前已记录的最大历史记录数
    /// </summary>
    public int Count
    {
      get
      {
        return count;
      }
    }

    /// <summary>
    /// 获取或设置当前图像文件
    /// </summary>
    public int Current
    {
      get
      {
        return current;
      }
      set
      {
        current = value;

        // 队列循环
        if (current < 0)
        {
          if (count < max)
            current = 0;
          else
            current = max - 1;
        }
        else if (current >= max)
        {
          current = 0;
          count = max;
        }

        if (current >= count)
          count = current + 1;

        OnHistoryChanged();
      }
    }

    /// <summary>
    /// 获取当前图像文件名
    /// </summary>
    public string CurrentImage
    {
      get
      {
        if (current >= 0)
          return History[current];
        else
          return "";
      }
    }

    /// <summary>
    /// 获取下一个图像文件名
    /// </summary>
    public string NextImage
    {
      get
      {
        int next = (current + 1) % max;
        return History[next];
      }
    }

    /// <summary>
    /// 建立历史记录类
    /// </summary>
    /// <param name="initDirectory">初始化文件目录</param>
    /// <param name="max">统计次数</param>
    public HistoryImage(string initDirectory, int max)
    {
      this.initDirectory = initDirectory;
      this.max = max;

      History = new string[max];

      for (int i = 0; i < max; i++)
      {
        History[i] = initDirectory + i.ToString() + ".psf";
      } // i
    }

    ///// <summary>
    ///// 重建历史记录
    ///// </summary>
    ///// <param name="max">统计次数</param>
    //public void Rebuild(int max)
    //{
    //  this.initDirectory = initDirectory;
    //  this.max = max;

    //  History = new string[max];

    //  for (int i = 0; i < max; i++)
    //  {
    //    History[i] = initDirectory + i.ToString() + ".psf";
    //  } // i
    //}

    /// <summary>
    /// 定位到指定的图像历史记录
    /// </summary>
    /// <param name="no">记录号</param>
    public void Seek(int no)
    {
      if (no < 0) no = 0;
      if (no >= max) no = max - 1;

      if (no > count) count = no;

      current = no;
    } // end of Seek

    /// <summary>
    /// 保存当前图像，并记录下当前文件
    /// </summary>
    public void Save()
    {
      save = current;
    } // end of Save


    /// <summary>
    /// 在历史记录指针改变时发生
    /// </summary>
    public event EventHandler HistoryChanged;
    protected void OnHistoryChanged()
    {
      if (HistoryChanged != null)
      {
        HistoryChanged(this, EventArgs.Empty);
      }
    }


  }
}
