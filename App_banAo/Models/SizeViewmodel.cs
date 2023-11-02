using App_data.Models;

namespace App_banAo.Models
{
    public class SizeViewmodel
    {
        public IEnumerable<Size>  sizes { get; set; } = Enumerable.Empty<Size>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();

    }
}
