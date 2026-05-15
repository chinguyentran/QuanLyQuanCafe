using Microsoft.EntityFrameworkCore;

namespace QuanLyQuanCafe.Models
{
    public class CafeDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Food> Foods { get; set; }

        // Thêm 2 dòng này vào để hết lỗi
        public DbSet<TableFood> TableFoods { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Nhớ đổi TEN_MAY_TINH_CUA_BAN thành tên server SQL của bạn nhé
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS01;Database=QuanLyQuanCafeDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}