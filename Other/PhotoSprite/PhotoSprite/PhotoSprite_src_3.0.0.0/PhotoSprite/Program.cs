using System;
using System.Windows.Forms;
using System.Threading;

namespace PhotoSprite
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] files)
    {
      string fileName = "";
      if (files.Length > 0)
        fileName = files[0];

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      // �������ڽ����д򿪶�� PhotoSprite ���
      bool createdNew;
      Mutex m = new Mutex(true, "PhotoSprite", out createdNew);
      if (createdNew)
      {
        Application.Run(new WinMain(fileName));
        m.ReleaseMutex();
      }
    }
  }
}