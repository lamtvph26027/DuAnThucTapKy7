using App_data.ViewModels.ChiTietSPView;

namespace App_banAo.Models
{
    public class PhanTrangKhuyenMai
    {
        public IEnumerable<KhuyenMaiView> listkmv { get; set; } = Enumerable.Empty<KhuyenMaiView>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
