namespace Posnet
{
    internal class PosnetExceptionItem
    {
        public int ExceptionId { get; set; }

        public string Mnemonik { get; set; }

        public string Description { get; set; }

        public PosnetExceptionItem(int eId, string mn, string desc)
        {
            ExceptionId = eId;
            Mnemonik = mn;
            Description = desc;
        }
    }
}
