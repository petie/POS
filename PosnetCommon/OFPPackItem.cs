// Decompiled with JetBrains decompiler
// Type: OnlineFPCommon.OFPPackItem
// Assembly: OnlineFPCommon, Version=2018.0.1.0, Culture=neutral, PublicKeyToken=8b54098295e79123
// MVID: 96CC98E8-1BF6-4466-B7FE-A0A74E8FCBEF
// Assembly location: C:\Program Files (x86)\Comarch OPT!MA Detal\OnlineFPCommon.dll

namespace OnlineFPCommon
{
  public class OFPPackItem
  {
    public OFPPackItem()
    {
    }

    public OFPPackItem(int number, int amount, long price)
    {
      this.Number = number;
      this.Amount = amount;
      this.Price = price;
      this.VatRate = OFPVatRate.Z;
    }

    public int Number { get; set; }

    public int Amount { get; set; }

    public long Price { get; set; }

    public string Name { get; set; }

    public string Unit { get; set; }

    public OFPVatRate VatRate { get; set; }
  }
}
