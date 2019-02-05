// Decompiled with JetBrains decompiler
// Type: PosnetLibDemo.Program
// Assembly: PosnetLibDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96A3BE02-8937-4E72-BCA3-AE3EBA98B77D
// Assembly location: C:\POSNET_OCX_2.0_001\Program_demo\x64\PosnetLibDemo.exe

using System;
using System.Windows.Forms;

namespace PosnetLibDemo
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}
