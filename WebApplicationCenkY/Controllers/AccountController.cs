using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebApplicationCenkY.Entities;
using WebApplicationCenkY.Models;

namespace WebApplicationCenkY.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;

        public AccountController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Users.Any(x => x.UserName == model.Username))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username already");
                    return View(model);
                }

                string dummyMD5 = _configuration.GetValue<string>("AppSettings:DummyMD");
                string pwdCombin = model.Password + dummyMD5;
                string hashPass = pwdCombin.MD5();

                User user = new()
                {
                    UserName = model.Username,
                    Password = hashPass
                };
                _databaseContext.Users.Add(user);
                int status = _databaseContext.SaveChanges();
                if(status == 0)
                {
                    ModelState.AddModelError("", "Not added");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string dummyMD5 = _configuration.GetValue<string>("AppSettings:DummyMD");
                string pwdCombin = model.Password + dummyMD5;
                string hashPass = pwdCombin.MD5();

                User user = _databaseContext.Users.SingleOrDefault(x => x.UserName == model.Username && x.Password == hashPass);
                if(user != null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim("Username", user.UserName.ToString()));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Pass incorrect.");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
