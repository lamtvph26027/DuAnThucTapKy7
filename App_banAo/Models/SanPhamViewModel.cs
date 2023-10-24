using App_data.Models;

namespace App_banAo.Models
{
    public class SanPhamViewModel
    {
        public IEnumerable<SanPham> SanPhams { get; set; } = Enumerable.Empty<SanPham>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
