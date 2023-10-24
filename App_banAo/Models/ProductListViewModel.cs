using App_data.Models;
using App_data.ViewModels;

namespace App_banAo.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<AllChiTietSanPham> chiTietSanPhams { get; set; }=new List<AllChiTietSanPham>();
        public PagingInfo PagingInfo  { get; set; }= new PagingInfo();
        //public IEnumerable<SanPham> SanPhams { get; set; } = Enumerable.Empty<SanPham>();
    }
}
