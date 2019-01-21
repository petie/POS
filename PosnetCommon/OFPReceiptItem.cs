// Decompiled with JetBrains decompiler
// Type: OnlineFPCommon.OFPReceiptItem
// Assembly: OnlineFPCommon, Version=2018.0.1.0, Culture=neutral, PublicKeyToken=8b54098295e79123
// MVID: 96CC98E8-1BF6-4466-B7FE-A0A74E8FCBEF
// Assembly location: C:\Program Files (x86)\Comarch OPT!MA Detal\OnlineFPCommon.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OnlineFPCommon
{
  [Serializable]
  public class OFPReceiptItem
  {
    private static Dictionary<char, byte> mazoviaCode;

    private OFPReceiptItem()
    {
    }

    public OFPReceiptItem(string name, long amount, int precision, long price, OFPVatRate vat)
    {
      this.Name = name;
      this.Amount = amount;
      this.Precision = precision;
      this.Price = price;
      this.Vat = vat;
      this.DiscountType = OFPDiscount.None;
    }

    public OFPReceiptItem(string name, long amount, int precision, string unit, long price, OFPVatRate vat)
    {
      this.Name = name;
      this.Amount = amount;
      this.Precision = precision;
      this.Unit = unit;
      this.Price = price;
      this.Vat = vat;
      this.DiscountType = OFPDiscount.None;
    }

    public OFPReceiptItem(string name, long amount, int precision, long price, OFPVatRate vat, long total)
    {
      this.Name = name;
      this.Amount = amount;
      this.Precision = precision;
      this.Price = price;
      this.Vat = vat;
      this.DiscountType = OFPDiscount.None;
      this.CheckTotal(total);
    }

    public OFPReceiptItem(string name, long amount, int precision, string unit, long price, OFPVatRate vat, long total)
    {
      this.Name = name;
      this.Amount = amount;
      this.Precision = precision;
      this.Unit = unit;
      this.Price = price;
      this.Vat = vat;
      this.DiscountType = OFPDiscount.None;
      this.CheckTotal(total);
    }

    public string Name { get; set; }

    public long Amount { get; set; }

    public int Precision { get; set; }

    public string Unit { get; set; }

    public long Price { get; set; }

    public OFPVatRate Vat { get; set; }

    public OFPDiscount DiscountType { get; set; }

    public long DiscountValue { get; set; }

    public long Total
    {
      get
      {
        return this.CalculateTotal();
      }
    }

    private long CalculateTotal()
    {
      long num1 = this.Amount * this.Price;
      long num2 = 1;
      for (int index = 0; index < this.Precision; ++index)
      {
        num1 /= 10L;
        num2 *= 10L;
      }
      long num3 = this.Amount * this.Price - num1 * num2;
      if (num3 >= num2 / 2L && num3 > 0L)
        ++num1;
      return num1;
    }

    public void CheckTotal(long total)
    {
      if (this.Total != total)
        throw new OFPException("Wysłana wartość towaru " + this.Name + " (" + (object) ((Decimal) total / new Decimal(100)) + ") nie zgadza się z wartością wyliczoną (" + (object) ((Decimal) this.Total / new Decimal(100)) + ")");
    }

    private static void FillMazoviaCodes()
    {
      if (OFPReceiptItem.mazoviaCode != null)
        return;
      OFPReceiptItem.mazoviaCode = new Dictionary<char, byte>();
      for (int index = 0; index < (int) sbyte.MaxValue; ++index)
        OFPReceiptItem.mazoviaCode.Add((char) index, (byte) index);
      OFPReceiptItem.mazoviaCode.Add('ą', (byte) 134);
      OFPReceiptItem.mazoviaCode.Add('ł', (byte) 146);
      OFPReceiptItem.mazoviaCode.Add('ś', (byte) 158);
      OFPReceiptItem.mazoviaCode.Add('ć', (byte) 141);
      OFPReceiptItem.mazoviaCode.Add('ń', (byte) 164);
      OFPReceiptItem.mazoviaCode.Add('ź', (byte) 166);
      OFPReceiptItem.mazoviaCode.Add('ę', (byte) 145);
      OFPReceiptItem.mazoviaCode.Add('ó', (byte) 162);
      OFPReceiptItem.mazoviaCode.Add('ż', (byte) 167);
      OFPReceiptItem.mazoviaCode.Add('Ą', (byte) 143);
      OFPReceiptItem.mazoviaCode.Add('Ł', (byte) 156);
      OFPReceiptItem.mazoviaCode.Add('Ś', (byte) 152);
      OFPReceiptItem.mazoviaCode.Add('Ć', (byte) 149);
      OFPReceiptItem.mazoviaCode.Add('Ń', (byte) 165);
      OFPReceiptItem.mazoviaCode.Add('Ź', (byte) 160);
      OFPReceiptItem.mazoviaCode.Add('Ę', (byte) 144);
      OFPReceiptItem.mazoviaCode.Add('Ó', (byte) 163);
      OFPReceiptItem.mazoviaCode.Add('Ż', (byte) 161);
      OFPReceiptItem.mazoviaCode.Add('ü', (byte) 129);
      OFPReceiptItem.mazoviaCode.Add('ä', (byte) 132);
      OFPReceiptItem.mazoviaCode.Add('ë', (byte) 137);
      OFPReceiptItem.mazoviaCode.Add('ö', (byte) 148);
      OFPReceiptItem.mazoviaCode.Add('ß', (byte) 225);
      OFPReceiptItem.mazoviaCode.Add('Ü', (byte) 154);
      OFPReceiptItem.mazoviaCode.Add('Ä', (byte) 142);
      OFPReceiptItem.mazoviaCode.Add('Ö', (byte) 153);
    }

    private static string RemoveDiacritics(string stIn)
    {
      string str = stIn.Normalize(NormalizationForm.FormD);
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < str.Length; ++index)
      {
        switch (CharUnicodeInfo.GetUnicodeCategory(str[index]))
        {
          case UnicodeCategory.NonSpacingMark:
            continue;
          case UnicodeCategory.SpaceSeparator:
            stringBuilder.Append(' ');
            continue;
          default:
            stringBuilder.Append(str[index]);
            continue;
        }
      }
      return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static string DiacriticsOnlyMazovia(string name)
    {
      OFPReceiptItem.FillMazoviaCodes();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < name.Length; ++index)
      {
        if (OFPReceiptItem.mazoviaCode.ContainsKey(name[index]))
          stringBuilder.Append(name[index]);
        else
          stringBuilder.Append(OFPReceiptItem.RemoveDiacritics(name[index].ToString()));
      }
      return stringBuilder.ToString();
    }

    public static byte[] stringToMazovia(string str)
    {
      OFPReceiptItem.FillMazoviaCodes();
      byte[] numArray1 = new byte[str.Length];
      for (int index = 0; index < str.Length; ++index)
      {
        if (OFPReceiptItem.mazoviaCode.ContainsKey(str[index]))
        {
          numArray1[index] = OFPReceiptItem.mazoviaCode[str[index]];
        }
        else
        {
          byte[] numArray2 = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(str[index].ToString()));
          if (numArray2.Length != 0)
            numArray1[index] = numArray2[0];
        }
      }
      return numArray1;
    }
  }
}
