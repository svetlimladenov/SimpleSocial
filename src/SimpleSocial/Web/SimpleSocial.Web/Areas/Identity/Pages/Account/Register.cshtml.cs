using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using SimpleSocial.Data;
using SimpleSocial.Data.Models;
using SimpleSocial.Data.Common.Constants;

namespace SimpleSocial.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<SimpleSocialUser> _signInManager;
        private readonly UserManager<SimpleSocialUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly SimpleSocialContext dbContext;

        public RegisterModel(
            UserManager<SimpleSocialUser> userManager,
            SignInManager<SimpleSocialUser> signInManager,
            ILogger<RegisterModel> logger,
            SimpleSocialContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {

            [Required]
            [StringLength(40, ErrorMessage = "The {0} must be max {1} characters long.")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "NewsFeed");
            }
            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new SimpleSocialUser() { UserName = Input.Username, Email = Input.Email};

                var result = await _userManager.CreateAsync(user, Input.Password);

                user.ProfilePictureURL = "https://res.cloudinary.com/svetlinmld/image/upload/v1546050240/default.jpg";

                await dbContext.SaveChangesAsync();

                if (result.Succeeded)
                {
                    await this._userManager.AddToRoleAsync(user, GlobalConstants.NormalUserRoleName);
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect("/Identity/Account/Manage");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
