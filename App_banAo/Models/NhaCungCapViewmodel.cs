using App_data.Models;

namespace App_banAo.Models
{
    public class NhaCungCapViewmodel
    {
        public IEnumerable<NhaCungCap>  NhaCungCaps { get; set; } = Enumerable.Empty<NhaCungCap>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();

    }
}
