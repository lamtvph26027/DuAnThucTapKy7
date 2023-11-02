using App_data.Models;

namespace App_banAo.Models
{
    public class AnhViewModel
    {
        public IEnumerable<Anh>  Anhs { get; set; } = Enumerable.Empty<Anh>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();


    }
}
