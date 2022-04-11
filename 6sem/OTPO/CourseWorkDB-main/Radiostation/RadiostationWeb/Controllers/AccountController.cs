using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadiostationWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RadiostationWebDbContext _dbContext;
        public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RadiostationWebDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;

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

        [AuthorizeRoles(RoleType.Admin, RoleType.HR_Employee)]
        public ActionResult ManageUsers(string nameFilter, string surnameFilter, int page = 1)
        {
            var pageSize = 10;
            var users = FilterUsers(nameFilter, surnameFilter);
            var pageUsers = users.OrderBy(o => o.UserName).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(users.Count(), page, pageSize);
            var viewUsers = pageUsers.ToList();
            var pageItemsModel = new PageItemsModel<ApplicationUser> { Items = viewUsers, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        private IQueryable<ApplicationUser> FilterUsers(string nameFilter, string surnameFilter)
        {
            IQueryable<ApplicationUser> users = _userManager.Users;
            nameFilter = nameFilter ?? HttpContext.Request.Cookies["nameFilter"];
            if (!string.IsNullOrEmpty(nameFilter))
            {
                users = users.Where(e => e.Name.Contains(nameFilter));
                HttpContext.Response.Cookies.Append("nameFilter", nameFilter);
            }

            surnameFilter = surnameFilter ?? HttpContext.Request.Cookies["surnameFilter"];
            if (!string.IsNullOrEmpty(surnameFilter))
            {
                users = users.Where(e => e.Surname.Contains(surnameFilter));
                HttpContext.Response.Cookies.Append("surnameFilter", surnameFilter);
            }
            return users;
        }

        public IActionResult ResetManageFilter()
        {
            HttpContext.Response.Cookies.Delete("nameFilter");
            HttpContext.Response.Cookies.Delete("surnameFilter");
            return RedirectToAction(nameof(ManageUsers));
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.HR_Employee)]
        public async Task<ActionResult> AddRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && roleName != null)
            {
                await _userManager.AddToRoleAsync(user, roleName);
                if (roleName.Equals(RoleType.Employeе))
                {
                    Employee employee = new Employee { AspNetUserId = userId };
                    if (!_dbContext.Employees.ToList().Any(o => o.AspNetUserId == userId))
                    {
                        _dbContext.Employees.Add(employee);
                        _dbContext.SaveChanges();

                    }
                }
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.HR_Employee)]
        public async Task<ActionResult> DeleteRole(string userId, string roleName)
        {
            var currentUserId = _userManager.GetUserId(User);
            if (currentUserId != userId || roleName != RoleType.Admin || roleName != RoleType.HR_Employee)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var employee = _dbContext.Employees.FirstOrDefault(o => o.AspNetUserId.Equals(userId));
                if (user != null && roleName != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                    if (roleName.Equals(RoleType.Employeе))
                    {
                        if (!_dbContext.Broadcasts.ToList().Any(o => o.EmployeeId == employee.Id))
                        {
                            _dbContext.Employees.Remove(employee);
                            _dbContext.SaveChanges();
                            return RedirectToAction(nameof(ManageUsers));
                        }
                    }
                }
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.HR_Employee)]

        public async Task<ActionResult> Delete(string userId)
        {
            var currentUserId = _userManager.GetUserId(User);
            if (userId != null && userId != currentUserId)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var userRoles = await _userManager.GetRolesAsync(user);
                var emplRole = userRoles.Contains("Employee");
                if (emplRole)
                {
                    var employee = _dbContext.Employees.FirstOrDefault(o => o.AspNetUserId.Equals(userId));

                    if ((!_dbContext.Broadcasts.ToList().Any(o => o.EmployeeId == employee.Id)))
                    {
                        await _userManager.DeleteAsync(user);
                    }
                    else
                    {
                        return BadRequest("can not delete user, delete existing links");
                    }
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            else
            {
                return BadRequest("can no delete user");
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.HR_Employee)]
        public ActionResult Create()
        {
            return View(new RegistrationViewModel());
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.HR_Employee)]
        [HttpPost]
        public async Task<ActionResult> Create(RegistrationViewModel registrationModel, string userRole = RoleType.User)
        {
            if (registrationModel.Username != null && registrationModel.Email != null)
            {
                if (await _userManager.FindByNameAsync(registrationModel.Username) != null)
                {
                    ModelState.AddModelError("", "Username already exists");
                }

                if (await _userManager.FindByEmailAsync(registrationModel.Email) != null)
                {
                    ModelState.AddModelError("", "Email already exists");
                }
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registrationModel.Username,
                    Email = registrationModel.Email,
                    Name = registrationModel.Name,
                    Surname = registrationModel.Surname,
                    MiddleName = registrationModel.MiddleName
                };
                var creatingReuslt = await _userManager.CreateAsync(user, registrationModel.Password);

                if (creatingReuslt.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, userRole);
                    if (userRole.Equals(RoleType.Employeе))
                    {
                        _dbContext.Employees.Add(new Employee { AspNetUserId = user.Id, Education = "", PositionId = null, WorkTime = null });
                        _dbContext.SaveChanges();
                    }
                    return RedirectToAction(nameof(ManageUsers));
                }
            }

            return View(registrationModel);
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.HR_Employee)]
        public async Task<ActionResult> Edit(string userId)
        {

            var currentUser = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var emplRole = userRoles.Contains("Employee");
            var positions = _dbContext.Positions.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();

            IList<SelectListItem> educations = new List<SelectListItem>
            {
                new SelectListItem{Text = "higher", Value = "ehigher"},
                new SelectListItem{Text = "secondary", Value = "secondary"},
                new SelectListItem{Text = "without education", Value = "without education"},
            };
            if (currentUser != null)
            {
                var userEditViewModel = new UserEmployeeEditViewModel
                {
                    Id = currentUser.Id,
                    UserName = currentUser.UserName,
                    Email = currentUser.Email,
                    Name = currentUser.Name,
                    Surname = currentUser.Surname,
                    MiddleName = currentUser.MiddleName,
                    EmployeeRole = emplRole,
                    PositionList = positions,
                    EducationList = educations,

                };
                if (userEditViewModel.EmployeeRole)
                {
                    var currentEmployee = _dbContext.Employees.FirstOrDefault(o => o.AspNetUserId == currentUser.Id);
                    userEditViewModel.PositionId = currentEmployee.PositionId;
                    userEditViewModel.Education = currentEmployee.Education;
                    userEditViewModel.EmployeeId = currentEmployee.Id;
                    userEditViewModel.WorkTime = currentEmployee.WorkTime;
                }

                return View(userEditViewModel);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.HR_Employee)]
        [HttpPost]
        public async Task<ActionResult> Edit(UserEmployeeEditViewModel user)
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
                ApplicationUser currentUser = await _userManager.FindByIdAsync(user.Id);
                currentUser.UserName = user.UserName;
                currentUser.Email = user.Email;
                currentUser.Name = user.Name;
                currentUser.Surname = user.Surname;
                currentUser.MiddleName = user.MiddleName;
                await _userManager.UpdateAsync(currentUser);
                var userRoles = await _userManager.GetRolesAsync(currentUser);
                var emplRole = userRoles.Contains("Employee");

                if (emplRole)
                {
                    var currentEmployee = _dbContext.Employees.FirstOrDefault(o => o.AspNetUserId.Equals(currentUser.Id));
                    currentEmployee.Education = user.Education;
                    currentEmployee.PositionId = user.PositionId;
                    currentEmployee.WorkTime = user.WorkTime;
                    _dbContext.Employees.Update(currentEmployee);
                    _dbContext.SaveChanges();
                }

                ViewData["SuccessMessage"] = "Successfully edited";
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        public ActionResult Registration(string returnUrl = "/")
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
                var user = new ApplicationUser
                {
                    UserName = registrationModel.Username,
                    Email = registrationModel.Email,
                    Name = registrationModel.Name,
                    Surname = registrationModel.Surname,
                    MiddleName = registrationModel.MiddleName
                };
                var creatingReuslt = await _userManager.CreateAsync(user, registrationModel.Password);
                if (creatingReuslt.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleType.User);
                    await _signInManager.PasswordSignInAsync(user, registrationModel.Password, true, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(registrationModel);
        }
    }
}
