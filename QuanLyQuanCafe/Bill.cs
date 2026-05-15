using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanCafe.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateCheckIn { get; set; }

        public DateTime? DateCheckOut { get; set; }

        public int IdTable { get; set; }

        public int Status { get; set; }

        public int Discount { get; set; }

        public double TotalPrice { get; set; }

        [ForeignKey("IdTable")]
        public virtual TableFood TableFood { get; set; }
    }
}