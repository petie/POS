using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Posnet
{
    [Serializable]
    public class PosnetReceiptItem
    {
        private static Dictionary<char, byte> mazoviaCode;

        private PosnetReceiptItem()
        {
        }

        public PosnetReceiptItem(string name, long amount, int precision, long price, VatRate vat)
        {
            Name = name;
            Amount = amount;
            Precision = precision;
            Price = price;
            Vat = vat;
            DiscountType = Discount.None;
        }

        public PosnetReceiptItem(string name, long amount, int precision, string unit, long price, VatRate vat)
        {
            Name = name;
            Amount = amount;
            Precision = precision;
            Unit = unit;
            Price = price;
            Vat = vat;
            DiscountType = Discount.None;
        }

        public PosnetReceiptItem(string name, long amount, int precision, long price, VatRate vat, long total)
        {
            Name = name;
            Amount = amount;
            Precision = precision;
            Price = price;
            Vat = vat;
            DiscountType = Discount.None;
            CheckTotal(total);
        }

        public PosnetReceiptItem(string name, long amount, int precision, string unit, long price, VatRate vat, long total)
        {
            Name = name;
            Amount = amount;
            Precision = precision;
            Unit = unit;
            Price = price;
            Vat = vat;
            DiscountType = Discount.None;
            CheckTotal(total);
        }

        public string Name { get; set; }

        public long Amount { get; set; }

        public int Precision { get; set; }

        public string Unit { get; set; }

        public long Price { get; set; }

        public VatRate Vat { get; set; }

        public Discount DiscountType { get; set; }

        public long DiscountValue { get; set; }

        public long Total
        {
            get
            {
                return CalculateTotal();
            }
        }

        private long CalculateTotal()
        {
            long num1 = Amount * Price;
            long num2 = 1;
            for (int index = 0; index < Precision; ++index)
            {
                num1 /= 10L;
                num2 *= 10L;
            }
            long num3 = Amount * Price - num1 * num2;
            if (num3 >= num2 / 2L && num3 > 0L)
                ++num1;
            return num1;
        }

        public void CheckTotal(long total)
        {
            if (Total != total)
                throw new FiscalException("Wysłana wartość towaru " + Name + " (" + total / new Decimal(100) + ") nie zgadza się z wartością wyliczoną (" + Total / new Decimal(100) + ")");
        }

        private static void FillMazoviaCodes()
        {
            if (mazoviaCode != null)
                return;
            mazoviaCode = new Dictionary<char, byte>();
            for (int index = 0; index < sbyte.MaxValue; ++index)
                mazoviaCode.Add((char)index, (byte)index);
            mazoviaCode.Add('ą', 134);
            mazoviaCode.Add('ł', 146);
            mazoviaCode.Add('ś', 158);
            mazoviaCode.Add('ć', 141);
            mazoviaCode.Add('ń', 164);
            mazoviaCode.Add('ź', 166);
            mazoviaCode.Add('ę', 145);
            mazoviaCode.Add('ó', 162);
            mazoviaCode.Add('ż', 167);
            mazoviaCode.Add('Ą', 143);
            mazoviaCode.Add('Ł', 156);
            mazoviaCode.Add('Ś', 152);
            mazoviaCode.Add('Ć', 149);
            mazoviaCode.Add('Ń', 165);
            mazoviaCode.Add('Ź', 160);
            mazoviaCode.Add('Ę', 144);
            mazoviaCode.Add('Ó', 163);
            mazoviaCode.Add('Ż', 161);
            mazoviaCode.Add('ü', 129);
            mazoviaCode.Add('ä', 132);
            mazoviaCode.Add('ë', 137);
            mazoviaCode.Add('ö', 148);
            mazoviaCode.Add('ß', 225);
            mazoviaCode.Add('Ü', 154);
            mazoviaCode.Add('Ä', 142);
            mazoviaCode.Add('Ö', 153);
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
            FillMazoviaCodes();
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < name.Length; ++index)
            {
                if (mazoviaCode.ContainsKey(name[index]))
                    stringBuilder.Append(name[index]);
                else
                    stringBuilder.Append(RemoveDiacritics(name[index].ToString()));
            }
            return stringBuilder.ToString();
        }

        public static byte[] stringToMazovia(string str)
        {
            FillMazoviaCodes();
            byte[] numArray1 = new byte[str.Length];
            for (int index = 0; index < str.Length; ++index)
            {
                if (mazoviaCode.ContainsKey(str[index]))
                {
                    numArray1[index] = mazoviaCode[str[index]];
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
