using App_data.ViewModels;

namespace App_banAo.Models
{
    public class PhanTrangSanPhamByKM
    {
        public IEnumerable<AllSanPhamByKM> listsp { get; set; } = new List<AllSanPhamByKM>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
