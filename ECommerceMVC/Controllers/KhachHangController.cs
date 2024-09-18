using AutoMapper;
using ECommerceMVC.Data;
using ECommerceMVC.Helper;
using ECommerceMVC.Helpers;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceMVC.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Hshop2023Context db;
        private readonly IMapper _mapper;
        public KhachHangController(Hshop2023Context context, IMapper mapper)
        {
            _mapper = mapper;
            db= context;
        }
		#region Register
		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}

		[HttpPost]
		public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
		{
			if (ModelState.IsValid)
			{
				// Map the ViewModel to the entity
				var KhachHang = _mapper.Map<KhachHang>(model);

				// Generate Random Key and Hash Password
				KhachHang.RandomKey = MyUtils.GenerateRamdomKey();
				KhachHang.MatKhau = model.MatKhau.ToMd5Hash(KhachHang.RandomKey);
				KhachHang.HieuLuc = true;
				KhachHang.VaiTro = 0;

				// Handle Image Upload (if provided)
				if (Hinh != null)
				{
					try
					{
						KhachHang.Hinh = MyUtils.UploadHinh(Hinh, "KhachHang");
					}
					catch (Exception ex)
					{
						// Log the error and return view with error message
						Console.WriteLine($"Image upload failed: {ex.Message}");
						ModelState.AddModelError("", "Image upload failed. Please try again.");
						return View(model);
					}
				}
				else
				{
					// Set a default image or leave it null
					KhachHang.Hinh = null; // Or set a default image path if required
				}

				try
				{
					// Save to database
					db.Add(KhachHang);
					db.SaveChanges();
					return RedirectToAction("Index", "HangHoa");
				}
				catch (Exception ex)
				{
					// Log the error and return view with error message
					Console.WriteLine($"Error saving to database: {ex.Message}");
					ModelState.AddModelError("", "Registration failed. Please try again.");
				}
			}
			else
			{
				// Log model validation errors
				var errors = ModelState.Values.SelectMany(v => v.Errors);
				foreach (var error in errors)
				{
					Console.WriteLine($"Model validation error: {error.ErrorMessage}");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}
		#endregion

		#region Login
		[HttpGet]
        public IActionResult DangNhap(string? ReturnURL)
        {
            ViewBag.ReturnURL = ReturnURL;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnURL)
        {
            ViewBag.ReturnURL = ReturnURL;
            if (ModelState.IsValid)
            {
                var khachHang=db.KhachHangs.SingleOrDefault(KH => KH.MaKh == model.UserName);
                if (khachHang == null)
                {
                    ModelState.AddModelError("loi", "Không có tài khoản");
                }

                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("loi", "Tài Khoản đang bị khóa !!");
                    }
                    else
                    {
                        if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("loi", "Sai Mật Khẩu !!");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, khachHang.Email),
                                new Claim(ClaimTypes.Name, khachHang.HoTen),
                                new Claim(MySetting.CLAIM_CUSTOMERID  , khachHang.MaKh),
                                new Claim(ClaimTypes.Role, "Customer")
                            };

                            var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            if (Url.IsLocalUrl(ReturnURL))
                            {
                                return Redirect(ReturnURL);
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        }
                    }
                }
            }
            return View();
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            return View(); 
        }
        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
