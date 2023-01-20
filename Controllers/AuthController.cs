using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services;
using MagicVilla_Web.Services.IServices;
using MagicVilla_Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginRequest = new LoginRequestDTO();

            return View(loginRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            var apiResponse = await _authService.LoginAsync<APIResponse>(loginRequest);

            if (apiResponse != null && apiResponse.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<LoginResponseDTO>(apiResponse.Result.ToString());

                // The following Claims logic is required so the app (HttpContext) understands that the user has signed in.
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, result.User.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, result.User.Role));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString(StaticDetails.SessionToken, result.Token);

                return RedirectToAction(nameof(Index), "Home");
            }

            ModelState.AddModelError("CustomError", apiResponse.ErrorMessages.FirstOrDefault());

            return View(loginRequest);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequest)
        {
            var apiResponse = await _authService.RegisterAsync<APIResponse>(registrationRequest);

            if (apiResponse != null && apiResponse.IsSuccess)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            HttpContext.Session.SetString(StaticDetails.SessionToken, "");

            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
