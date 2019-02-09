using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.DataAccess;
using POS.Models;

namespace POS.Tests.Integration
{
    public class SeedData
    {
        internal static void PopulateTestData(PosDbContext db)
        {
            db.Tax.AddRange(GetTaxRates());
            db.Products.AddRange(GetProducts());
            db.SaveChanges();
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product("5907771443270","Kiełki na patelnie 200g Uniflora",4.99m, "szt", 2),
                new Product("5907814660503","Daktyle bez pestek BIO 150g BioPlanet",6.70m, "szt", 3),
                new Product("5903874560722","Ciastka owsiane bez cukru BIO 190g Symbio",8.90m, "szt", 4)
            };
        }

        internal static List<Tax> GetTaxRates()
        {
            return new List<Tax>
            {
                new Tax("0.00","C",0.00m),
                new Tax("5.00","D",5.00m),
                new Tax("8.00","B",8.00m),
                new Tax("23.00","A",23.00m)
            };
        }
    }
}
