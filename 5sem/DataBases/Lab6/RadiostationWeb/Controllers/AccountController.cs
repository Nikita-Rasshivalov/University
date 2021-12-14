using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RadiostationWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
            });
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginModel.Username);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var sigInResult = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if (sigInResult.Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }




        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageUsers()
        {
            var users = _userManager.Users.ToList();

            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && roleName != null)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }

            return RedirectToAction("ManageUsers");
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteRole(string userId, string roleName)
        {
            var currentUserId = _userManager.GetUserId(User);
            if (currentUserId != userId || roleName != "Admin")
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null && roleName != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }

            return RedirectToAction("ManageUsers");
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string userId)
        {
            var currentUserId = _userManager.GetUserId(User);
            if (userId != null && userId != currentUserId)
            {
                var user = await _userManager.FindByIdAsync(userId);
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("ManageUsers");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new RegistrationViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(RegistrationViewModel registrationModel, string userRole = "User")
        {
            if (await _userManager.FindByNameAsync(registrationModel.Username) != null)
            {
                ModelState.AddModelError("", "Username already exists");
            }

            if (await _userManager.FindByEmailAsync(registrationModel.Email) != null)
            {
                ModelState.AddModelError("", "Email already exists");
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registrationModel.Username,
                    Email = registrationModel.Email,
                };
                var creatingReuslt = await _userManager.CreateAsync(user, registrationModel.Password);
                if (creatingReuslt.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, userRole);
                    return RedirectToAction("ManageUsers");
                }
            }

            return View(registrationModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(IdentityUser user)
        {
            var userByUsername = await _userManager.FindByNameAsync(user.UserName);
            if (userByUsername != null && userByUsername.Id != user.Id)
            {
                ModelState.AddModelError("", "Username already exists");
            }

            var userByEmail = await _userManager.FindByEmailAsync(user.Email);
            if (userByEmail != null && userByEmail.Id != user.Id)
            {
                ModelState.AddModelError("", "Email already exists");
            }

            if (ModelState.IsValid)
            {
                var foundUser = await _userManager.FindByIdAsync(user.Id);
                foundUser.UserName = user.UserName;
                foundUser.Email = user.Email;
                await _userManager.UpdateAsync(foundUser);
                ViewData["SuccessMessage"] = "Successfully edited";
            }

            return View(user);
        }

        public ActionResult Registration(string returnUrl="/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationViewModel registrationModel)
        {
            if (await _userManager.FindByNameAsync(registrationModel.Username) != null)
            {
                ModelState.AddModelError("", "Username already exists");
            }

            if (await _userManager.FindByEmailAsync(registrationModel.Email) != null)
            {
                ModelState.AddModelError("", "Email already exists");
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registrationModel.Username,
                    Email = registrationModel.Email,
                };
                var creatingReuslt = await _userManager.CreateAsync(user, registrationModel.Password);
                if (creatingReuslt.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.PasswordSignInAsync(user, registrationModel.Password, false, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(registrationModel);
        }
    }
}
