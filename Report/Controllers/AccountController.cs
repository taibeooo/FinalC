using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Report.Models;
using System.Security.Claims;
using Report.Data;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace Report.Controllers
{
    public class AccountController : Controller
    {
        private readonly ReportContext _context;

        public AccountController(ReportContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User _userFromPage)
        {

            var userInDb = _context.User.FirstOrDefault(u => u.UserEmail == _userFromPage.UserEmail || u.UserName == _userFromPage.UserName);

            if (userInDb != null)
            {
                // Băm mật khẩu được nhập vào.
                string hashedPassword = HashPassword(_userFromPage.UserPassword);

                // So sánh mật khẩu đã băm với mật khẩu đã lưu trong cơ sở dữ liệu.
                if (userInDb.UserPassword == hashedPassword)
                {
                    // Đăng nhập thành công
                    var claims = new List<Claim>();

                    if (!string.IsNullOrEmpty(userInDb.UserEmail))
                    {
                        claims.Add(new Claim(ClaimTypes.Name, userInDb.UserEmail));
                    }

                    if (!string.IsNullOrEmpty(userInDb.UserName))
                    {
                        claims.Add(new Claim("FullName", userInDb.UserName));
                    }

                    if (!string.IsNullOrEmpty(userInDb.UserRole))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userInDb.UserRole));
                    }

                    // Tiếp tục xử lý SignInAsync...


                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties { };
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }
                // Đăng nhập không thành công
                ViewBag.LoginStatus = 0;
            

            
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,UserPassword,UserEmail")] User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem UserEmail đã tồn tại trong cơ sở dữ liệu
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail);

                if (existingUser != null)
                {
                    // Nếu tìm thấy trùng lặp, thêm thông báo lỗi và hiển thị lại trang
                    ModelState.AddModelError("UserEmail", "Email đã được sử dụng.");
                    return View(user);
                }

                // Hash mật khẩu trước khi lưu vào cơ sở dữ liệu
                user.UserPassword = HashPassword(user.UserPassword);

                // Thiết lập giá trị mặc định cho UserRole
                user.UserRole = "customer";

                _context.Add(user);
                await _context.SaveChangesAsync();

                // Đặt biến TempData để hiển thị thông báo thành công trong tệp Razor
                TempData["RegisterSuccess"] = true;

                // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }
        public IActionResult ForgotPassWord()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string userEmail,string userName)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                ViewBag.ForgotPasswordStatus = "Vui lòng nhập địa chỉ email của bạn.";
                return View();
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserEmail == userEmail && u.UserName == userName);

            if (user == null)
            {
                ViewBag.ForgotPasswordStatus = "Không tìm thấy người dùng với địa chỉ email/username này.";
                return View();
            }

            // Tạo mật khẩu ngẫu nhiên chứa cả chữ số và ký tự đặc biệt
            string newPassword = GenerateRandomPassword();

            // Cập nhật mật khẩu mới vào cơ sở dữ liệu
            user.UserPassword = HashPassword(newPassword);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Trong hàm ForgotPassword sau khi tạo mật khẩu mới
            ViewBag.GeneratedPassword = newPassword; // newPassword là mật khẩu mới đã tạo
            return View("ForgotPasswordConfirmation");

        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
        }




        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                // Chuyển giá trị băm thành một chuỗi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }

                // Cắt chuỗi kết quả thành 40 ký tự
                return sb.ToString().Substring(0, 40);
            }
        }

        

    }
}
