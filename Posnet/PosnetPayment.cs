namespace Posnet
{
    public class PosnetPayment
    {
        public PosnetPayment()
        {
        }

        public PosnetPayment(int type, string name, long value)
        {
            Type = type;
            Name = name;
            Value = value;
        }

        public int Type { get; set; }

        public string Name { get; set; }

        public long Value { get; set; }
    }
}
