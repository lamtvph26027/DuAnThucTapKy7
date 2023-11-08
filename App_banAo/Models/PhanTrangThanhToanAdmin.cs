using App_data.Models;
using App_data.ViewModels;

namespace App_banAo.Models
{
    public class PhanTrangThanhToanAdmin
    {
        public IEnumerable<HoaDon> listhoadons { get; set; } = Enumerable.Empty<HoaDon>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
