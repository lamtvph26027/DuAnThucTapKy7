using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.ViewModels.ChiTietSPView
{
    public class LoaiSPView
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }
        public int TrangThai { get; set; }
        public Guid? IdLoaiSPCha { get; set; }
        public Guid IdChatLieu { get; set; }
        public virtual List<ChatLieuView> listChatLieus { get; set; }
    }
}
