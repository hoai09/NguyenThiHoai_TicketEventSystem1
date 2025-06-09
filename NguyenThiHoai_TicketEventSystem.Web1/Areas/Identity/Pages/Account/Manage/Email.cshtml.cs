﻿using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using NguyenThiHoai_TicketEventSystem.Core.Models;
namespace NguyenThiHoai_TicketEventSystem.Web.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public EmailModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager; _signInManager = signInManager; _emailSender = emailSender;
        }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        [TempData] public string StatusMessage { get; set; }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel { [Required][EmailAddress][Display(Name = "Email mới")] public string NewEmail { get; set; } }
        private async Task LoadAsync(ApplicationUser user)
        {
            var email = await _userManager.GetEmailAsync(user); Email = email;
            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User); if (user == null) return NotFound();
            await LoadAsync(user); return Page();
        }
        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User); if (user == null) return NotFound();
            if (!ModelState.IsValid) { await LoadAsync(user); return Page(); }
            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                var callbackUrl = Url.Page("/Account/ConfirmEmailChange", pageHandler: null, values: new { area = "Identity", userId, email = Input.NewEmail, code }, protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(Input.NewEmail, "Xác nhận email của bạn", $"Vui lòng xác nhận tài khoản của bạn bằng cách <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>nhấn vào đây</a>.");
                StatusMessage = "Link xác nhận đổi email đã được gửi. Vui lòng kiểm tra email của bạn.";
                return RedirectToPage();
            }
            StatusMessage = "Email của bạn không thay đổi.";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User); if (user == null) return NotFound();
            if (!ModelState.IsValid) { await LoadAsync(user); return Page(); }
            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { area = "Identity", userId, code }, protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(email, "Xác nhận email của bạn", $"Vui lòng xác nhận tài khoản của bạn bằng cách <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>nhấn vào đây</a>.");
            StatusMessage = "Email xác thực đã được gửi. Vui lòng kiểm tra email của bạn.";
            return RedirectToPage();
        }
    }
}