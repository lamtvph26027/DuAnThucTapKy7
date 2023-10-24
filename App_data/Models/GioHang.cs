using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
  public class GioHang
    {
        public Guid IdNguoiDung { get; set; }
        public DateTime NgayTao { get; set; }
        public virtual TaiKhoan? TaiKhoan { get; set; }
        public virtual IEnumerable<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }
}
