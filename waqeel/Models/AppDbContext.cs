using Microsoft.EntityFrameworkCore;

namespace waqeel.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        // إضافة waqeel.Models قبل كل اسم جدول لحل التعارض
        public DbSet<waqeel.Models.Account> Accounts { get; set; }
        public DbSet<waqeel.Models.PackingRecord> PackingRecords { get; set; }
        public DbSet<waqeel.Models.PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<waqeel.Models.Voucher> Vouchers { get; set; }
        public DbSet<waqeel.Models.JournalHeader> JournalHeaders { get; set; }
        public DbSet<waqeel.Models.JournalDetail> JournalDetails { get; set; }
        public DbSet<waqeel.Models.Item> Items { get; set; }
        public DbSet<waqeel.Models.Driver> Drivers { get; set; }
        public DbSet<waqeel.Models.Farmer> Farmers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // تأكد أن الباسورد هنا 123 كما في تثبيت PostgreSQL
            // Only configure here when options haven't been provided (e.g., when not using DI)
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=WaqeelDB;Username=postgres;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تحديد المفاتيح الأساسية للجداول
            modelBuilder.Entity<waqeel.Models.Account>().HasKey(a => a.AccountCode);
            modelBuilder.Entity<waqeel.Models.PurchaseInvoice>().HasKey(i => i.InvoiceNo);
            modelBuilder.Entity<waqeel.Models.Voucher>().HasKey(v => v.VoucherNo);
            modelBuilder.Entity<waqeel.Models.Item>().HasKey(it => it.ItemCode);
            modelBuilder.Entity<waqeel.Models.Driver>().HasKey(d => d.DriverId);
            modelBuilder.Entity<waqeel.Models.Farmer>().HasKey(f => f.Id);
        }
    }
}
