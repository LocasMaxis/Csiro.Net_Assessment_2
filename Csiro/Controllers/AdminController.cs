using Csiro.Models;
using Csiro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Csiro.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly ApplicantDbContext _db;
        private UserManager<ApplicationUser> userManager { get; }
        private RoleManager<IdentityRole> roleManager { get; }

        private List<string> userList;
        private readonly IConfiguration _configuration;

        public AdminController(ApplicantDbContext db, UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager, IConfiguration configuration)
        {
            this.userManager = _userManager;
            this.roleManager = _roleManager;
            _db = db;
            _configuration = configuration;

            /*userList = new List<string>();
            var users = userManager.Users;

            foreach (var user in users) userList.Add(user.Email);*/

        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded) return View("Display", roleManager.Roles);

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View("Display", roleManager.Roles);

        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string roleId)
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
        public async Task<IActionResult> ManageRole(ManageRole mrole)
        {
            var role = await roleManager.FindByIdAsync(mrole.roleID);
            var user = await userManager.FindByIdAsync(mrole.userID);

            if (!(await userManager.IsInRoleAsync(user, role.Name)))
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

        [HttpGet]
        public IActionResult DisplayApps()
        {
            int? sort = HttpContext.Session.GetInt32("sort");
            dynamic p;
            if (sort == 0 || sort == null)
            {
                p = from a in _db.applicants.Where(a => a.deleted == false && a.gpa >= _configuration.GetValue<float>("GPA cutoff"))
                    join c in _db.Courses
                    on a.courseID equals c.CourseID into anObject
                    join u in _db.Universities
                    on a.uniID equals u.UniID
                    from c in anObject.DefaultIfEmpty()
                    orderby a.firstName
                    select new
                    {
                        a.applicantID,
                        a.firstName,
                        a.lastName,
                        a.email,
                        courseName = c.CourseName,
                        uniName = u.UniName,
                        a.gpa,
                        a.resume,
                        a.sent
                    };
            }
            else if (sort == 1)
            {
                p = from a in _db.applicants.Where(a => a.deleted == false && a.gpa >= _configuration.GetValue<float>("GPA cutoff"))
                    join c in _db.Courses
                    on a.courseID equals c.CourseID into anObject
                    join u in _db.Universities
                    on a.uniID equals u.UniID
                    from c in anObject.DefaultIfEmpty()
                    orderby a.gpa descending
                    select new
                    {
                        a.applicantID,
                        a.firstName,
                        a.lastName,
                        a.email,
                        courseName = c.CourseName,
                        uniName = u.UniName,
                        a.gpa,
                        a.resume,
                        a.sent
                    };
            }
            else
            {
                p = from a in _db.applicants.Where(a => a.deleted == false && a.gpa >= _configuration.GetValue<float>("GPA cutoff"))
                    join c in _db.Courses
                    on a.courseID equals c.CourseID into anObject
                    join u in _db.Universities
                    on a.uniID equals u.UniID
                    from c in anObject.DefaultIfEmpty()
                    orderby a.gpa
                    select new
                    {
                        a.applicantID,
                        a.firstName,
                        a.lastName,
                        a.email,
                        courseName = c.CourseName,
                        uniName = u.UniName,
                        a.gpa,
                        a.resume,
                        a.sent
                    };
            }
            List<ApplicationCombined> aList = new List<ApplicationCombined>();
            foreach (var applicant in p)
            {
                Applicants app = new Applicants { applicantID = applicant.applicantID, firstName = applicant.firstName, lastName = applicant.lastName, email = applicant.email, gpa = applicant.gpa, resume = applicant.resume, sent = applicant.sent };
                Courses cor = new Courses { CourseName = applicant.courseName };
                Universities uni = new Universities { UniName = applicant.uniName };
                ApplicationCombined fullApp = new ApplicationCombined { a10 = app, c10 = cor, u10 = uni };
                aList.Add(fullApp);
            }

            return View(aList);
        }

        [HttpGet]
        public IActionResult SortGpa()
        {
            int? sort = HttpContext.Session.GetInt32("sort");
            if (sort == 0)
            {
                HttpContext.Session.SetInt32("sort", 1);
            }
            else if (sort == 1)
            {

                HttpContext.Session.SetInt32("sort", -1);
            }
            else
            {
                HttpContext.Session.SetInt32("sort", 0);
            }

            return RedirectToAction("DisplayApps", "Admin");
        }

        [HttpGet]
        public IActionResult DeleteApp(int applicantID)
        {
            Applicants application = _db.applicants.Find(applicantID);
            application.deleted = true;
            _db.Update(application);
            _db.SaveChanges();
            return Redirect("DisplayApps");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int applicationID)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var senderName = $"{currentUser.FirstName} {currentUser.LastName}";
            Applicants applicant = await _db.applicants.FindAsync(applicationID);
            string msg = $"Dear {applicant.firstName} {applicant.lastName}\n\n";
            msg += "We are pleased to inform you that you have moved onto the next phase of our hiring process and invite you to an interview with us on the 15th of June at 3:00PM at our Head Office at 10 Wayfard St, Melbourne Vic.\n";
            msg += $"Please arrive 15 minutes before your appointment to allow processing at the front desk.\n\nKind Regards,\n{senderName},\nCsiro HR Department";

            var message = new MailMessage("CsiroHR@outlook.com", applicant.email, "Application Progress", msg);


            using (var smtp = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = "csrioassignmentdummy@outlook.com",
                    Password = "qbcolmiersnpgosl"
                };
                smtp.Credentials = credentials;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }

            applicant.sent = true;
            _db.Update(applicant);
            _db.SaveChanges();

            return View("DisplayApps");
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
