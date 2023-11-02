using App_data.Models;
using App_data.ViewModels;

namespace App_banAo.Models
{
    public class QLSanPhamKhuyenMai
    {
        public IEnumerable<AllSanPhamByKM> ChiTietSanPhams { get; set; } = new List<AllSanPhamByKM>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
