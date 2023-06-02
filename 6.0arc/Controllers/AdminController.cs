using Csiro.Models;
using Csiro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Csiro.Controllers
{

    [Authorize(Roles = "admin")]
	public class AdminController: Controller
    {

        private readonly ApplicantDataContext _db;
        private UserManager<IdentityUser> userManager { get; }
        private RoleManager<IdentityRole> roleManager { get; }

        private List<string> userList;

        public AdminController(UserManager<IdentityUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {
            this.userManager = _userManager;
            this.roleManager = _roleManager;

            /*userList = new List<string>();
            var users = userManager.Users;

            foreach (var user in users) userList.Add(user.Email);*/

        }
        [HttpGet]
        //[Authorize(Roles = "admin1, user")] //or
		
		public IActionResult CreateRole()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        public async Task <IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded) return View("Display", roleManager.Roles);

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                }
            return View("Display", roleManager.Roles);
            
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string roleId)
        { 
            var role = await roleManager.FindByIdAsync(roleId);
            IdentityResult result = await roleManager.DeleteAsync(role);
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("Display", roleManager.Roles);

        }

        [HttpGet]
        public IActionResult ManageRole()
        {
            ManageRole mrole = new ManageRole();

            FillArray(mrole);
            return View(mrole);
        }

		[HttpPost]
		public async Task<IActionResult> ManageRole( ManageRole mrole)
		{
            var role = await roleManager.FindByIdAsync(mrole.roleID);
            var user = await userManager.FindByIdAsync(mrole.userID);

            if(!(await userManager.IsInRoleAsync(user, role.Name)))
            {
                await userManager.AddToRoleAsync(user, role.Name);
            }
			return View("Display", roleManager.Roles);
		}


		private void FillArray(ManageRole mrole)
        {
            var users = userManager.Users;
            mrole.userList = new List<SelectListItem>();

            foreach (var user in users)
            {
                mrole.userList.Add(new SelectListItem { Text = user.UserName, Value = user.Id });
            }

            var roles = roleManager.Roles;
            mrole.roleList = new List<SelectListItem>();

            foreach (var role in roles)
            {
                mrole.roleList.Add(new SelectListItem { Text = role.Name, Value = role.Id });
            }
        }

        public IActionResult DisplayApps()
        {
            var p = from a in _db.applicants
                    join c in _db.courses
                    on a.courseID equals c.courseID into anObject
                    join u in _db.universities
                    on a.uniID equals u.uniID
                    from c in anObject.DefaultIfEmpty()
                    select new
                    {
                        applicantID = a.applicantID,
                        firstName = a.firstName,
                        lastName = a.lastName,
                        email = a.email,
                        courseName = c.courseName,
                        uniName = u.uniName,
                        gpa = a.gpa,
                        resume = a.resume
                    };
            List<ApplicationCombined> aList = new List<ApplicationCombined>();
            foreach (var applicant in p) {
                Applicants app = new Applicants { applicantID = applicant.applicantID, firstName = applicant.firstName, lastName = applicant.lastName, email = applicant.email, gpa = applicant.gpa, resume = applicant.resume};
                Courses cor = new Courses { courseName = applicant.courseName };
                Universities uni = new Universities { uniName = applicant.uniName };
                ApplicationCombined fullApp = new ApplicationCombined { a10 = app, c10 = cor, u10 = uni };
                aList.Add(fullApp);
            }

            return View(aList);
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
