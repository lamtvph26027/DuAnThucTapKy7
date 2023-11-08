using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.ViewModels.ThongKe
{
    public class ThongKeMSSanPhamTheoSoLuong
    {
        public Guid idSanPham { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public int Gia { get; set; }
        public long DoanhThu { get; set; }
        public DateTime Ngay { get; set; }
    }
}
