using CatKitchenApp.Data;
using CatKitchenApp.Models;
using CatKitchenApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CatKitchenApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly CatKitchenDbContext _context;

        public AccountController(CatKitchenDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Authors.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);

                if (user != null)
                {
                    string roleName = user.RoleID == 1 ? "admin" : "user";

                    await Authenticate(model.Login, roleName); // Передаем текст "admin"
                    if (roleName == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Recipes");
                    }
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userExists = _context.Authors.Any(u => u.Login == model.Login);
                if (userExists)
                {
                    ModelState.AddModelError("Login", "Такой пользователь уже есть");
                    return View(model);
                }

                var user = new Author
                {
                    AuthorName = model.AuthorName,
                    Login = model.Login,
                    Password = model.Password,
                    Mail = model.Mail,
                    BirthDay = DateTime.Now, // Заглушка, если даты нет в форме
                    Phone = "-",             // Заглушка
                    Stage = 0,
                    RoleID = 2 // Обычный пользователь
                };

                _context.Authors.Add(user);
                await _context.SaveChangesAsync();

                string roleName = user.RoleID == 1 ? "admin" : "user";

                await Authenticate(model.Login, roleName);
                if (roleName == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Recipes");
                }
            }
            return View(model);
        }

        private async Task Authenticate(string userName, string role)
        {
            // Создаем claims (данные о пользователе)
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString())
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            // Устанавливаем куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
