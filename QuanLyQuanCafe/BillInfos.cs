using System;

namespace QuanLyQuanCafe.Models
{
    public partial class BillInfos
    {
        public int Id { get; set; }
        public int IdBill { get; set; }
        public int IdFood { get; set; }
        public int Count { get; set; }
    }
}