using App_data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.ViewModels.ChiTietSPView
{
    public  class ChiTietSPModel
    {
        
       public Guid id { get; set; }

        public int DonGia { get; set; }
        public int soluong { get; set; }
        public int TrangThai { get; set; }
        public List<Guid> listAnhs { get; set; }
      
        public  List<Guid> listMauSacs { get; set; }
      
    
        public List<Guid> listSizes { get; set; }
       
    }
}
