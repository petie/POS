using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class Tax
    {
        public Tax()
        {

        }

        public Tax(string symbol, string fiscalSymbol, decimal value)
        {
            Symbol = symbol;
            FiscalSymbol = fiscalSymbol;
            Value = value;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string FiscalSymbol { get; set; }
        public decimal Value { get; set; }
    }
}
