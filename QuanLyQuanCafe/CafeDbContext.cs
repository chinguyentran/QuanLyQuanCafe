using Microsoft.EntityFrameworkCore;

namespace QuanLyQuanCafe.Models
{
    public class CafeDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<TableFood> TableFoods { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillInfos> BillInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS01;Database=QuanLyQuanCafeDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}
