using App_data.Models;
using App_data.ViewModels.ThongKe;

namespace App_banAo.Models
{
    public class PhanTrangThongKeKH
    {
        public IEnumerable<ThongKeKHMuaNhieu> listkhs { get; set; } = Enumerable.Empty<ThongKeKHMuaNhieu>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
