using App_data.Models;

namespace App_banAo.Models
{
    public class ChatLieuViewModel
    {
        public IEnumerable<ChatLieu>   chatLieus { get; set; } = Enumerable.Empty<ChatLieu>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();

    }
}
