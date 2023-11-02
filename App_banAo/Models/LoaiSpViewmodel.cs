using App_data.Models;

namespace App_banAo.Models
{
    public class LoaiSpViewmodel
    {
        public IEnumerable<LoaiSP>  loaiSPs { get; set; } = Enumerable.Empty<LoaiSP>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();

    }
}
