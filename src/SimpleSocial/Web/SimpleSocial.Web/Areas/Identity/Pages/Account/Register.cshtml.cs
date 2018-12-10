using System;
using System.Collections.Generic;
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
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<SimpleSocialUser> _signInManager;
        private readonly UserManager<SimpleSocialUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly SimpleSocialContext dbContext;
        private readonly IRepository<ProfilePicture> profilePicturesRepository;

        public RegisterModel(
            UserManager<SimpleSocialUser> userManager,
            SignInManager<SimpleSocialUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            SimpleSocialContext dbContext,
            IRepository<ProfilePicture> profilePicturesRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.dbContext = dbContext;
            this.profilePicturesRepository = profilePicturesRepository;
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
            [StringLength(40, ErrorMessage = "The {0} must be max {1} characters long.")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(40, ErrorMessage = "The {0} must be max {1} characters long.")]
            public string LastName { get; set; }

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

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new SimpleSocialUser() { UserName = Input.Username, FirstName = Input.FirstName, LastName = Input.LastName, Email = Input.Email};

                var result = await _userManager.CreateAsync(user, Input.Password);
                var wall = new Wall()
                {
                    UserId = dbContext.Users.FirstOrDefault(x => x.UserName == user.UserName)?.Id
                };

                dbContext.Walls.Add(wall);
                dbContext.SaveChanges();
                user.WallId = wall.Id;
                dbContext.SaveChanges();
                if (result.Succeeded)
                {
                    
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
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
