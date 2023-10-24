using App_data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.ViewModels
{
    public class QLKhuyenMaiView
    {
        public Guid Id { get; set; }
        public Guid idkhuyenMai { get; set; }
        public List<AllChiTietSanPham> CTSPVs { get; set; }= new List<AllChiTietSanPham>();
    }
}
