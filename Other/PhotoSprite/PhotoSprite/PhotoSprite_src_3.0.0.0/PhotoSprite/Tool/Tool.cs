using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PhotoSprite.Tool
{
  public class Tool
  {
    protected Color foreColor = Color.Red;
    protected Color backColor = Color.Transparent;
    protected int fontSize = 12;
    protected FontFamily fontFamily;
    protected FontStyle fontStyle;
    protected int brushWidth = 1;
    protected int tolerance = 30;
    protected HatchStyle hatchStyle;
    protected bool hasBrushStyle = false;

    /// <summary>
    /// ��ȡ�����û�ˢͼ��
    /// </summary>
    public HatchStyle FillStyle
    {
      get
      {
        return hatchStyle;
      }
      set
      {
        hatchStyle = value;
      }
    }

    /// <summary>
    /// ��ȡ������һ�� bool ֵ����ָ����ˢ�Ƿ���ͼ��
    /// </summary>
    public bool HasBrushStyle
    {
      get
      {
        return hasBrushStyle;
      }
      set
      {
        hasBrushStyle = value;
      }
    }

    /// <summary>
    /// ��ȡ�����������
    /// </summary>
    public FontFamily Family
    {
      get
      {
        return fontFamily;
      }
      set
      {
        fontFamily = value;
      }
    }

    /// <summary>
    /// ��ȡ������������
    /// </summary>
    public FontStyle Style
    {
      get
      {
        return fontStyle;
      }
      set
      {
        fontStyle = value;
      }
    }

    /// <summary>
    /// ��ȡ�������ֺ�
    /// </summary>
    public int FontSize
    {
      get
      {
        return fontSize;
      }
      set
      {
        fontSize = value;
      }
    }

    /// <summary>
    /// ��ȡ�����û��ʿ��
    /// </summary>
    public int BrushWidth
    {
      get
      {
        return brushWidth;
      }
      set
      {
        brushWidth = value;
      }
    }

    /// <summary>
    /// ��ȡ������ǰ����ɫ
    /// </summary>
    public Color ForeColor
    {
      get
      {
        return foreColor;
      }
      set
      {
        foreColor = value;
      }
    }

    /// <summary>
    /// ��ȡ�����ñ�����ɫ
    /// </summary>
    public Color BackColor
    {
      get
      {
        return backColor;
      }
      set
      {
        backColor = value;
      }
    }

    /// <summary>
    /// ��ȡ��������ɫ�ݲ�
    /// </summary>
    public int Tolerance
    {
      get
      {
        return tolerance;
      }
      set
      {
        tolerance = value;
      }
    }

  }
}
