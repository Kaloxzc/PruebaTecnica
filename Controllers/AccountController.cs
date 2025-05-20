using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;

namespace Prueba_Tecnica.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly FondoXyzContext _context;
        private readonly ILogger<EmailService> _logger;


        // Un solo constructor con todas las dependencias
        public AccountController(
            FondoXyzContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            EmailService emailService,
            ILogger<EmailService> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NombreCompleto = model.FullName // Asegúrate de mapear tus propiedades personalizadas
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Redirige a una página de confirmación
                    return RedirectToAction("RegisterConfirmation");
                }

                // Si hay errores, agrégualos al ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Si llegamos aquí, algo falló, muestra el formulario nuevamente
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl });
                }
                if (result.IsLockedOut)
                {
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("Email", "El correo electrónico es requerido");
                return View(model);
            }
            var usuario = await _userManager.FindByEmailAsync(model.Email);

            if (usuario == null)
            {
                // Por seguridad, no revelar que el email no existe
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            // Generar token de recuperación
            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            // O si prefieres usar código como en tu ejemplo:
            var codigo = GenerateRandomCode(6); // Método que genera código alfanumérico

            // Guardar en base de datos (si usas código)
            var recoveryCode = new RecoveryCode
            {
                UserId = usuario.Id,
                Code = codigo,
                Expiration = DateTime.Now.AddMinutes(30),
                IsUsed = false
            };

            _context.RecoveryCode.Add(recoveryCode);
            await _context.SaveChangesAsync();

            // Enviar email
            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { userId = usuario.Id, code = token }, protocol: Request.Scheme);

            var emailBody = $"Por favor restablezca su contraseña haciendo clic <a href='{callbackUrl}'>aquí</a>. " +
                           $"O use este código: {codigo}";
            var usuarioEmail = usuario.Email ?? throw new InvalidOperationException("El usuario no tiene email registrado");

            try
            {
                await _emailService.SendEmailAsync(model.Email, "Restablecer contraseña", emailBody);
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al enviar el correo: {ex.Message}");
                return View(model);
            }
        }

        // Método para generar código aleatorio
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View("~/Views/Account/ForgotPasswordConfirmation.cshtml"); // Buscará en /Views/Account/ForgotPasswordConfirmation.cshtml
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null, string userId = null)
        {
            try
            {
                if (code == null || userId == null)
                {
                    return RedirectToAction("Error", "Home");
                }

                var model = new ResetPassword
                {
                    Code = code,
                    UserId = userId
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ResetPassword GET");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                // Redirige a confirmación aunque el usuario no exista (por seguridad)
                return RedirectToAction("ResetPasswordConfirmation");
            }

            // Decodifica el código si fue codificado en la URL
            var code = model.Code;
            if (code.Contains(' '))
            {
                code = code.Replace(' ', '+');
            }

            var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
            if (result.Succeeded)
            {
                // Opcional: Iniciar sesión automáticamente después del cambio
                // await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

    }
}
