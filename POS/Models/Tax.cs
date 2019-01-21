namespace POS.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string FiscalSymbol { get; set; }
        public decimal Value { get; set; }
    }
}
