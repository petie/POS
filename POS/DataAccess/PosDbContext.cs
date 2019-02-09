using Microsoft.EntityFrameworkCore;
using POS.Models;

namespace POS.DataAccess
{
    public class PosDbContext : DbContext
    {
        public PosDbContext()
        {

        }
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options)
        { }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<PaymentInfo> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<ReceiptItem> ReceiptItems { get; set; }
        public virtual DbSet<Tax> Tax { get; set; }
    }
}
