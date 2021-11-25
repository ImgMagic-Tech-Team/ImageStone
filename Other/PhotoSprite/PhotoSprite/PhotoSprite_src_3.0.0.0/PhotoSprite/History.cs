using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSprite
{
  /// <summary>
  /// ͼ������ʷ��¼
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
    /// ��ȡ�����ó�ʼ���ļ�Ŀ¼
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
    /// ��ȡ bool ֵ��ָʾ�Ƿ���Գ���
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
    /// ��ȡ bool ֵ��ָʾ�Ƿ�����ظ�
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
    /// ��ȡ bool ֵ��ָʾͼ���Ƿ��Ѿ��޸Ĺ�
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
    /// ��ȡ�����ʷ��¼��
    /// </summary>
    public int Max
    {
      get
      {
        return max;
      }
    }

    /// <summary>
    /// ��ȡ��ǰ�Ѽ�¼�������ʷ��¼��
    /// </summary>
    public int Count
    {
      get
      {
        return count;
      }
    }

    /// <summary>
    /// ��ȡ�����õ�ǰͼ���ļ�
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

        // ����ѭ��
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
    /// ��ȡ��ǰͼ���ļ���
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
    /// ��ȡ��һ��ͼ���ļ���
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
    /// ������ʷ��¼��
    /// </summary>
    /// <param name="initDirectory">��ʼ���ļ�Ŀ¼</param>
    /// <param name="max">ͳ�ƴ���</param>
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
    ///// �ؽ���ʷ��¼
    ///// </summary>
    ///// <param name="max">ͳ�ƴ���</param>
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
    /// ��λ��ָ����ͼ����ʷ��¼
    /// </summary>
    /// <param name="no">��¼��</param>
    public void Seek(int no)
    {
      if (no < 0) no = 0;
      if (no >= max) no = max - 1;

      if (no > count) count = no;

      current = no;
    } // end of Seek

    /// <summary>
    /// ���浱ǰͼ�񣬲���¼�µ�ǰ�ļ�
    /// </summary>
    public void Save()
    {
      save = current;
    } // end of Save


    /// <summary>
    /// ����ʷ��¼ָ��ı�ʱ����
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
