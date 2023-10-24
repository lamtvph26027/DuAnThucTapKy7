using App_banAo.Services;
using App_data.Models;
using App_data.ViewModels.ChiTietSPView;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace App_banAo.Controllers
{
    public class GioHangController : Controller
    {
        Uri urlapi = new Uri("https://localhost:7016/api");
        private readonly HttpClient _httpClient;
    
        private readonly ILogger<GioHangController> _logger;
        AppDbcontext _context = new AppDbcontext();
        List<ChiTietGioHang> listCTGH = new List<ChiTietGioHang>();
        List<ChiTietSanPham> listCTSP = new List<ChiTietSanPham>();
        List<SanPham> listSP = new List<SanPham>();
        List<MauSac> listMauSac= new List<MauSac> ();
        List<Size> listSize= new List<Size> ();
       
        public GioHangController(ILogger<GioHangController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = urlapi;
            _context = new AppDbcontext();
            listCTSP=new List<ChiTietSanPham> ();

            // List ChitietGioHang
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/ChiTietGioHang").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listCTGH = JsonConvert.DeserializeObject<List<ChiTietGioHang>>(data);
            }
            // List ChiTietSanPham
            HttpResponseMessage response1 = _httpClient.GetAsync(_httpClient.BaseAddress + "/ChiTietSP").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data1 = response1.Content.ReadAsStringAsync().Result;
                listCTSP = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(data1);
            }
            // List SanPham
            HttpResponseMessage response2 = _httpClient.GetAsync(_httpClient.BaseAddress + "/SanPham").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data2 = response2.Content.ReadAsStringAsync().Result;
                listSP = JsonConvert.DeserializeObject<List<SanPham>>(data2);
            }
            // List MauSac
            HttpResponseMessage response3 = _httpClient.GetAsync(_httpClient.BaseAddress + "/MauSac").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data2 = response2.Content.ReadAsStringAsync().Result;
                listMauSac = JsonConvert.DeserializeObject<List<MauSac>>(data2);
            }
            // List Size
            HttpResponseMessage response4 = _httpClient.GetAsync(_httpClient.BaseAddress + "/Size").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data2 = response2.Content.ReadAsStringAsync().Result;
                listSize = JsonConvert.DeserializeObject<List<Size>>(data2);
            }

        }
        public List<LoaiSP> LoadLoaiSP()
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/LoaiSP").Result;
            List<LoaiSP> ListLoaiSP = new List<LoaiSP>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ListLoaiSP = JsonConvert.DeserializeObject<List<LoaiSP>>(data);
            }

            return ListLoaiSP.ToList();
        }
        public List<SanPham> LoadSP()
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/SanPham").Result;
            List<SanPham> listSP = new List<SanPham>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listSP = JsonConvert.DeserializeObject<List<SanPham>>(data);
            }
            return listSP.ToList();
        }
        public List<ChiTietSanPham> LoadChiTietSP()
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/ChiTietSP").Result;
            List<ChiTietSanPham> listBT = new List<ChiTietSanPham>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listBT = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(data);
            }
            return listBT.ToList();
        }
        // HIỂN THỊ SẢN PHẨM TRONG GIỎ HÀNG
        // tong so luong
        private int TongSoLuong()
        {

            int iTongSoLuong = 0;

            if (listCTGH != null)
            {
                iTongSoLuong = listCTGH.Sum(n => n.SoLuong);
            }
            return iTongSoLuong;
        }
        //tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }
        public async Task<IActionResult> ShowCart()
        {
            //list ctsp
            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await _httpClient.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;
            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await _httpClient.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //list km
            string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            var response1 = await _httpClient.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            ViewBag.KhuyenMaiView = KhuyenMais;
            // Nếu chưa đăng nhập thì hiển thị toàn bộ cart trong session
            if (HttpContext.Session.GetString("IdUser") == null)
            {
                var products = GioHangServices.GetObjFormSession(HttpContext.Session, "Cart");
                if (products.Count == 0)
                {
                    TempData["Message"] = "Không có sản phẩm nào trong giỏ hàng!";
                }
                ViewData["listChiTietSanPham"] = LoadChiTietSP();
                ViewData["listSP"] = LoadSP();
                int cout = 0;
                for (int i = 0; i < products.Sum(c=>c.SoLuong); i++)
                {
                    cout++;
                }
                ViewData["cout"] = cout;


                return View(products);
            }
            else // Lấy toàn bộ chitietgiohang của người dùng 
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));// Lấy iduser đăng nhập
                var ctgh = listCTGH.Where(c => c.IdNguoiDung == iduser).ToList();
                if (ctgh.Count == 0)
                {
                    TempData["Message"] = "Không có sản phẩm nào trong giỏ hàng!";
                }
                ViewData["listChiTietSanPham"] = LoadChiTietSP();
                ViewData["listSP"] = LoadSP();
                int cout = 0;
                for (int i = 0; i < ctgh.Sum(c => c.SoLuong); i++)
                {
                    cout++;
                }
                ViewData["cout"] = cout;

                return View(ctgh);
            }
        }
        // THÊM VÀO GIỎ +1 soluong
        public async Task<IActionResult> AddToCart(Guid id)
        {
            // Lấy Chi Tiet San Pham từ Id
            var product = listCTSP.Where(c => c.Id.Equals(id)).FirstOrDefault();

            if (HttpContext.Session.GetString("IdUser") == null)  // Nếu không đăng nhập, thêm sp thì lưu vào session giỏ hàng
            {
                //Đọc dữ liệu từ Session xem trong Cart nó có cái gì chưa?
                var cartproducts = GioHangServices.GetObjFormSession(HttpContext.Session, "Cart");
                if (cartproducts.Count == 0)
                {
                    var cartitem = new ChiTietGioHang()
                    {
                        IdNguoiDung = new Guid(),
                        IdChiTietSP = product.Id,
                        DonGia=product.DonGia,
                        SoLuong = 1,
                    };
                    cartproducts.Add(cartitem); // Nếu Cart rỗng thì thêm sp vào luôn
                    // Đưa dữ liệu về lại Session
                    GioHangServices.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                }
                else
                {
                    if (!GioHangServices.CheckProductInCart(id, cartproducts)) // Cart không rỗng nhưng k chứa sản phẩm đó
                    {
                        var cartitem = new ChiTietGioHang()
                        {
                            IdNguoiDung = new Guid(),
                            IdChiTietSP = product.Id,
                            DonGia = product.DonGia,
                            SoLuong = 1,
                        };
                        cartproducts.Add(cartitem); // Nếu Cart chưa chứa sản phẩm thì thêm sp vào luôn
                                                    // Đưa dữ liệu về lại Session
                        GioHangServices.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên so luong
                    {
                        var cartitem = cartproducts.Where(c => c.IdChiTietSP == id).FirstOrDefault();
                        cartitem.SoLuong+=1;

                        GioHangServices.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                        var x = GioHangServices.GetObjFormSession(HttpContext.Session, "Cart");
                    }
                }
            } // Ngược lại có đăng nhập
            else
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IdNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 

                if (cartDetailList.Count == 0)// Giỏ rỗng thì thêm mới
                {
                    var cartitem = new ChiTietGioHang()
                    {
                        IdNguoiDung = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                        IdChiTietSP = product.Id,
                        DonGia = product.DonGia,
                        SoLuong = 1,
                    };
                    var url = $"https://localhost:7016/api/ChiTietGioHang?soluong={cartitem.SoLuong}&IdChiTietSp={cartitem.IdChiTietSP}&IdNguoiDung={cartitem.IdNguoiDung}";
                    var response = await _httpClient.PostAsync(url, null);

                }
                else // Không rỗng thì ktra có chứa sản phẩm đấy không
                {
                    if (!GioHangServices.CheckProductInCart(id, cartDetailList)) // 
                    {
                        var cartitem = new ChiTietGioHang()
                        {
                            IdNguoiDung = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                            IdChiTietSP = product.Id,
                            DonGia = product.DonGia,
                            SoLuong = 1,
                        };
                        var url = $"https://localhost:7016/api/ChiTietGioHang?soluong={cartitem.SoLuong}&IdChiTietSp={cartitem.IdChiTietSP}&IdNguoiDung={cartitem.IdNguoiDung}";
                        var response = await _httpClient.PostAsync(url, null);
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên 1
                    {
                        var cartitem = cartDetailList.Where(c => c.IdChiTietSP == id).FirstOrDefault();
                        cartitem.SoLuong+= 1;
                        var url = $"https://localhost:7016/api/ChiTietGioHang?soluong={cartitem.SoLuong}&IdChiTietSp={cartitem.IdChiTietSP}&IdNguoiDung={cartitem.IdNguoiDung}";
                        var response = await _httpClient.PutAsync(url, null);

                    }
                }
            }
            return RedirectToAction("ShowCart");
        }
        //Thêm Vào Giỏ Hàng + bằng số lượng
        public async Task<IActionResult> AddToCartSoLuong(Guid id,int soluong)
        {
            // Lấy Chi Tiet San Pham từ Id
            var product = listCTSP.Where(c => c.Id.Equals(id)).FirstOrDefault();

            if (HttpContext.Session.GetString("IdUser") == null)  // Nếu không đăng nhập, thêm sp thì lưu vào session giỏ hàng
            {
                //Đọc dữ liệu từ Session xem trong Cart nó có cái gì chưa?
                var cartproducts = GioHangServices.GetObjFormSession(HttpContext.Session, "Cart");
                if (cartproducts.Count == 0)
                {
                    var cartitem = new ChiTietGioHang()
                    {
                        IdNguoiDung = new Guid(),
                        IdChiTietSP = product.Id,
                        DonGia = product.DonGia,
                        SoLuong = soluong,
                    };
                    cartproducts.Add(cartitem); // Nếu Cart rỗng thì thêm sp vào luôn
                    // Đưa dữ liệu về lại Session
                    GioHangServices.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                }
                else
                {
                    if (!GioHangServices.CheckProductInCart(id, cartproducts)) // Cart không rỗng nhưng k chứa sản phẩm đó
                    {
                        var cartitem = new ChiTietGioHang()
                        {
                            IdNguoiDung = new Guid(),
                            IdChiTietSP = product.Id,
                            DonGia = product.DonGia,
                            SoLuong = soluong,
                        };
                        cartproducts.Add(cartitem); // Nếu Cart chưa chứa sản phẩm thì thêm sp vào luôn
                                                    // Đưa dữ liệu về lại Session
                        GioHangServices.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên so luong
                    {
                        var cartitem = cartproducts.Where(c => c.IdChiTietSP == id).FirstOrDefault();
                        cartitem.SoLuong += soluong;

                        GioHangServices.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                        var x = GioHangServices.GetObjFormSession(HttpContext.Session, "Cart");
                    }
                }
            } // Ngược lại có đăng nhập
            else
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IdNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 

                if (cartDetailList.Count == 0)// Giỏ rỗng thì thêm mới
                {
                    var cartitem = new ChiTietGioHang()
                    {
                        IdNguoiDung = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                        IdChiTietSP = product.Id,
                        DonGia = product.DonGia,
                        SoLuong = soluong,
                    };
                    var url = $"https://localhost:7016/api/ChiTietGioHang?soluong={cartitem.SoLuong}&IdChiTietSp={cartitem.IdChiTietSP}&IdNguoiDung={cartitem.IdNguoiDung}";
                    var response = await _httpClient.PostAsync(url, null);

                }
                else // Không rỗng thì ktra có chứa sản phẩm đấy không
                {
                    if (!GioHangServices.CheckProductInCart(id, cartDetailList)) // 
                    {
                        var cartitem = new ChiTietGioHang()
                        {
                            IdNguoiDung = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                            IdChiTietSP = product.Id,
                            DonGia = product.DonGia,
                            SoLuong = soluong,
                        };
                        var url = $"https://localhost:7016/api/ChiTietGioHang?soluong={cartitem.SoLuong}&IdChiTietSp={cartitem.IdChiTietSP}&IdNguoiDung={cartitem.IdNguoiDung}";
                        var response = await _httpClient.PostAsync(url, null);
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên soluong
                    {
                        var cartitem = cartDetailList.Where(c => c.IdChiTietSP == id).FirstOrDefault();
                        cartitem.SoLuong += soluong;
                        var url = $"https://localhost:7016/api/ChiTietGioHang?soluong={cartitem.SoLuong}&IdChiTietSp={cartitem.IdChiTietSP}&IdNguoiDung={cartitem.IdNguoiDung}";
                        var response = await _httpClient.PutAsync(url, null);

                    }
                }
            }
            return RedirectToAction("ShowCart");
        }
        // CẬP NHẬT GIỎ HÀNG
        public async Task<IActionResult> Update_Quantity(IFormCollection f)
        {
            var countsp = listCTSP.Where(c => c.Id == Guid.Parse(f["ID_product"].ToString())).FirstOrDefault(); // Sản phẩm bị sửa
            Guid id = Guid.Parse(f["ID_product"].ToString()); // Lấy id sp bị chỉnh sửa trong giỏ
            // đã đăng nhập
            if (HttpContext.Session.GetString("IdUser") != null)
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IdNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 

                if (int.Parse(f["Quantity"].ToString()) == 0) // bằng 0 thì xóa luôn
                {
                    var x = cartDetailList.Where(c => c.IdChiTietSP == id).FirstOrDefault();
                    var url = $"https://localhost:7016/api/ChiTietGioHang/{x.Id}";
                    var response = await _httpClient.DeleteAsync(url);
                }
                else if (int.Parse(f["Quantity"].ToString()) > countsp.soluong) // Lớn hơn thì thông báo
                {
                    TempData["OverQuantity"] = "Vượt quá số lượng sản phẩm trong kho!";
                    return RedirectToAction("ShowCart");
                }
                else
                { // cập nhật vào giỏ hàng
                    var x = cartDetailList.Where(c => c.IdChiTietSP == id).FirstOrDefault();
                    x.SoLuong = int.Parse(f["Quantity"].ToString());
                    var url = $"https://localhost:7016/api/ChiTietGioHang/{x.Id}?soluong={x.SoLuong}&IdChiTietSp={x.IdChiTietSP}&IdNguoiDung={x.IdNguoiDung}";
                    var response = await _httpClient.PutAsync(url, null);

                }
                return RedirectToAction("ShowCart");
            }
            else // chưa đăng nhập
            {
                var products = GioHangServices.GetObjFormSession(HttpContext.Session, "Cart"); // Lấy cart trong session
                if (int.Parse(f["Quantity"].ToString()) == 0)
                {
                    var x = products.Where(c => c.IdChiTietSP == id).FirstOrDefault();
                    products.Remove(x);
                }
                else if (int.Parse(f["Quantity"].ToString()) > countsp.soluong)
                {
                    TempData["OverQuantity"] = "Vượt quá số lượng sản phẩm trong kho!";
                    return RedirectToAction("ShowCart");
                }
                else
                {
                    var x = products.Where(c => c.IdChiTietSP == id).FirstOrDefault();
                    x.SoLuong = int.Parse(f["Quantity"].ToString());
                }
                GioHangServices.SetObjToJson(HttpContext.Session, "Cart", products);
                return RedirectToAction("ShowCart");
            }
        }
        // XÓA SẢN PHẨM TRONG GIỎ HÀNG
        public async Task<IActionResult> RemoveAll()
        {
            if (HttpContext.Session.GetString("IdUser") != null)// đã đăng nhập
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IdNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 
                for (int i = 0; i < cartDetailList.Count; i++)
                {
                    var url = $"https://localhost:7016/api/ChiTietGioHang/{cartDetailList[i].Id}";
                    var response = await _httpClient.DeleteAsync(url);
                }
                return RedirectToAction("ShowCart");
            }
            else
            {
                GioHangServices.RemoveAll(HttpContext.Session, "Cart");
                return RedirectToAction("ShowCart");
            }
        }
        public async Task<IActionResult> RemoveCartItem(Guid id)
        {
            if (HttpContext.Session.GetString("IdUser") != null)// đã đăng nhập
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IdNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 
                ChiTietGioHang cartItem = cartDetailList.Where(c => c.IdChiTietSP == id).FirstOrDefault();

                var url = $"https://localhost:7016/api/ChiTietGioHang/{cartItem.Id}";
                var response = await _httpClient.DeleteAsync(url);
                return RedirectToAction("ShowCart");
            }
            else
            {
                var products = GioHangServices.GetObjFormSession(HttpContext.Session, "Cart");
                var x = products.Where(c => c.IdChiTietSP == id).FirstOrDefault();
                products.Remove(x);
                GioHangServices.SetObjToJson(HttpContext.Session, "Cart", products);
                return RedirectToAction("ShowCart");
            }
        }
        
    }
}
