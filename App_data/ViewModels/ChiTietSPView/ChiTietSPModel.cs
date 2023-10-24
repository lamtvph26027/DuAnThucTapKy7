using App_data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.ViewModels.ChiTietSPView
{
    public  class ChiTietSPModel
    {
        
        public Guid IdAnh { get; set; }
        public Guid IdMauSac { get; set; }
        public Guid IdSanPham { get; set; }
        public Guid IdLoaiSP { get; set; }
        public Guid? IdKhuyenMai { get; set; }
        public Guid IdNhaCungCap { get; set; }
        public Guid IdSize { get; set; }
        public int DonGia { get; set; }
        public int soluong { get; set; }
        public int TrangThai  { get; set; }
        public virtual List<AnhView>? listAnhs { get; set; }
      
        public virtual List<MauSacView>? listMauSacs { get; set; }
      
    
        public virtual List<SizeView>? listSizes { get; set; }
       
    }
}
