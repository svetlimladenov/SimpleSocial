using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.SignUpDetails;

namespace SimpleSocial.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<SimpleSocialUser> _userManager;
        private readonly SignInManager<SimpleSocialUser> _signInManager;
        private readonly IProfileDetailsServices profileDetailsServices;
        private readonly IRepository<SimpleSocialUser> userRepository;

        public IndexModel(
            UserManager<SimpleSocialUser> userManager,
            SignInManager<SimpleSocialUser> signInManager,
            IProfileDetailsServices profileDetailsServices,
            IRepository<SimpleSocialUser> userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.profileDetailsServices = profileDetailsServices;
            this.userRepository = userRepository;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public List<string> Countries { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime? BirthDay { get; set; }

            public string Description { get; set; }

            public Gender? Gender { get; set; }

            public string City { get; set; }

            public string Country { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var birthday = user.BirthDay;
            var description = user.Description;
            var gender = user.Gender;
            var city = user.City;
            var country = user.Country;
            this.Username = userName;

            Input = new InputModel
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDay = birthday,
                Description = description,
                Gender = gender,
                City = city,
                Country = country,
                Email = email,
                PhoneNumber = phoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            this.Countries = profileDetailsServices.GetCounties();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            if (!string.IsNullOrEmpty(Input.FirstName))
            {
                user.FirstName = Input.FirstName;
            }

            if (!string.IsNullOrEmpty(Input.LastName))
            {
                user.LastName = Input.LastName;
            }

            if (Input.Gender == null)
            {
                user.Gender = Input.Gender;
            }

            if (!string.IsNullOrEmpty(Input.Description))
            {
                user.Description = Input.Description;
            }

            if (!string.IsNullOrEmpty(Input.City))
            {
                user.City = Input.City;
            }

            if (!string.IsNullOrEmpty(Input.Country))
            {
                user.Country = Input.Country;
            }

            if (Input.BirthDay != null && Input.BirthDay < DateTime.Now)
            {
                user.BirthDay = Input.BirthDay;
            }


            userRepository.SaveChangesAsync().GetAwaiter().GetResult();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToAction("SuccessfullAction", "Profiles");
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
