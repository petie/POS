// Decompiled with JetBrains decompiler
// Type: OnlineFPCommon.OFPPayment
// Assembly: OnlineFPCommon, Version=2018.0.1.0, Culture=neutral, PublicKeyToken=8b54098295e79123
// MVID: 96CC98E8-1BF6-4466-B7FE-A0A74E8FCBEF
// Assembly location: C:\Program Files (x86)\Comarch OPT!MA Detal\OnlineFPCommon.dll

namespace OnlineFPCommon
{
  public class OFPPayment
  {
    public OFPPayment()
    {
    }

    public OFPPayment(int type, string name, long value)
    {
      this.Type = type;
      this.Name = name;
      this.Value = value;
    }

    public int Type { get; set; }

    public string Name { get; set; }

    public long Value { get; set; }
  }
}
