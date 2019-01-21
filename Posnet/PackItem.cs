namespace Posnet
{
    public class PackItem
    {
        public PackItem()
        {
        }

        public PackItem(int number, int amount, long price)
        {
            Number = number;
            Amount = amount;
            Price = price;
            VatRate = VatRate.Z;
        }

        public int Number { get; set; }

        public int Amount { get; set; }

        public long Price { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public VatRate VatRate { get; set; }
    }
}
