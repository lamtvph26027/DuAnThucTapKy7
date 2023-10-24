using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
    public class LoaiSP
    {
        public Guid Id { get; set; }
        [StringLength(50, ErrorMessage = "Loai san pham khong duoc qua 50 ki tu .")]
        public string Ten { get; set; }
        public int TrangThai { get; set; }
        public Guid? IdLoaiSPCha { get; set; }
        public Guid IdChatLieu { get; set; }
        public virtual IEnumerable<ChiTietSanPham> ChiTietSanPhams { get; set; }
        
        public virtual LoaiSP LoaiSPChas { get; set; }
        public virtual ChatLieu ChatLieus { get; set; }
    }
}
