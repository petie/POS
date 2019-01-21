// Decompiled with JetBrains decompiler
// Type: OnlineFPPosnetProtocol.PostnetProtExceptionItem
// Assembly: OnlineFPPosnetProtocol, Version=2018.0.1.0, Culture=neutral, PublicKeyToken=8b54098295e79123
// MVID: 619FDB08-8046-432B-AE81-BF4303AA2094
// Assembly location: C:\Program Files (x86)\Comarch OPT!MA Detal\drivers\OnlineFPPosnetProtocol.dll

namespace OnlineFPPosnetProtocol
{
  internal class PostnetProtExceptionItem
  {
    public int ExceptionId { get; set; }

    public string Mnemonik { get; set; }

    public string Description { get; set; }

    public PostnetProtExceptionItem(int eId, string mn, string desc)
    {
      this.ExceptionId = eId;
      this.Mnemonik = mn;
      this.Description = desc;
    }
  }
}
