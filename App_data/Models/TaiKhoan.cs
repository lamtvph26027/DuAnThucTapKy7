using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
    public class TaiKhoan
    {
        [Key]
        public Guid IdNguoiDung { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "ten ko dc qua 20 ki tu")]
        public string Ten { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "ho ko dc qua 20 ki tu")]
        public string TenDem { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "tenDem ko dc qua 20 ki tu")]
        public string Ho { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "password phai dai hon 5 ki tu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public int GioiTinh { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public DateTime NgaySinh { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is required.")]


        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(12, ErrorMessage = "phai dung dang so dien thoai tu 10->11 so")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public int TrangThai { get; set; }

        public Guid IdVaiTro { get; set; }
        public virtual GioHang? GioHang { get; set; }
        public virtual VaiTro VaiTros { get; set; }

        public virtual IEnumerable<HoaDon> HoaDons { get; set; }
        public virtual IEnumerable<DanhGia> DanhGias { get; set; }
    }
}
