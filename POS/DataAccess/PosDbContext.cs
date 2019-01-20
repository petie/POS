using Microsoft.EntityFrameworkCore;
using POS.Models;

namespace POS.DataAccess
{
    public class PosDbContext : DbContext
    {
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options)
        { }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<PaymentInfo> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ReceiptItem> ReceiptItems { get; set; }
        public DbSet<Tax> Tax { get; set; }
    }
}
