using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
    public class Size
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }

        public int TrangThai { get; set; }
        public virtual IEnumerable<ChiTietSanPham> ChiTietSanPhams { get; set; }
    }
}
