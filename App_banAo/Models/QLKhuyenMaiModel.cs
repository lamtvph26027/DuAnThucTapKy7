using App_data.Models;
using App_data.ViewModels;

namespace App_banAo.Models
{
    public class QLKhuyenMaiModel
    {
        public IEnumerable<ChiTietSanPham> ChiTietSanPhams { get; set; }= new List<ChiTietSanPham>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
