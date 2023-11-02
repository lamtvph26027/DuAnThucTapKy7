using App_data.Models;

namespace App_banAo.Models
{
    public class MauSacViewModel
    {
        public IEnumerable<MauSac> mauSacs { get; set; } = Enumerable.Empty<MauSac>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();


    }
}
