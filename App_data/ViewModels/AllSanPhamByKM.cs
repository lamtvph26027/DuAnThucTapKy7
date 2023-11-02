using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.ViewModels
{
   public class AllSanPhamByKM
    {
        public Guid Id { get; set; }
        [StringLength(40, ErrorMessage = "Ten san pham khong duoc dai qua 40 tu.")]
        public string Ten { get; set; }
        public Guid TimIdKM { get; set; } 
        public string TenAnh { get; set; }
        public Guid IdLoaiSP { get; set; }
        public Guid? IdLoaiSpCha { get; set; }
        [Required]
        public string MoTa { get; set; }

        public int TrangThai { get; set; }
    }
}
