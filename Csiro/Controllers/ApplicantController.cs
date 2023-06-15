using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Csiro.ViewModels;
using Csiro.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ApplicantController : Controller
{
    private readonly ApplicantDbContext _db;
    private UserManager<ApplicationUser> userManager { get; }
    private SignInManager<ApplicationUser> signInManager { get; }

    public ApplicantController(ApplicantDbContext db, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
    {
        _db = db;
        userManager = _userManager;
        signInManager = _signInManager;
    }

    public IActionResult Index()
    {
        //return RedirectToAction("Display");
        return View();
    }

    public IActionResult Register() // Applicants(view) -> Register.cshtml(razor view)
    {
        // Get courses from the database and populate courseList
        var courses = _db.Courses.ToList();
        var courseList = courses.Select(c => new SelectListItem
        {
            Value = c.CourseID.ToString(),
            Text = c.CourseName
        }).ToList();

        // Get universities from the database and populate uniList
        var universities = _db.Universities.ToList();
        var uniList = universities.Select(u => new SelectListItem
        {
            Value = u.UniID.ToString(),
            Text = u.UniName
        }).ToList();

        // Create instance of ApplicantsViewModel and populate lists
        var viewModel = new ApplicantsViewModel
        {
            courseList = courseList,
            uniList = uniList
        };

        return View(viewModel);
    }

    //User creates application
    [HttpPost]
    public async Task<IActionResult> Register(ApplicantsViewModel m)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.GetUserAsync(User);
            var applicant = new Applicants
            {
                firstName = m.FirstName,
                lastName = m.LastName,
                email = m.Email,
                gpa = (float)m.Gpa,
                resume = m.Resume,
                coverLetter = m.CoverLetter,
                courseID = m.CourseID,
                uniID = m.UniID,
                userId = user.Id
            };

            _db.applicants.Add(applicant);
            await _db.SaveChangesAsync();

            return RedirectToAction("Success");
        }

        var courses = _db.Courses.ToList();
        var courseList = courses.Select(c => new SelectListItem
        {
            Value = c.CourseID.ToString(),
            Text = c.CourseName
        }).ToList();

        var universities = _db.Universities.ToList();
        var uniList = universities.Select(u => new SelectListItem
        {
            Value = u.UniID.ToString(),
            Text = u.UniName
        }).ToList();

        m.courseList = courseList;
        m.uniList = uniList;

        return View(m);
    }


    //Get application data for edit
    public async Task<IActionResult> Edit()
    {
        var user = await userManager.GetUserAsync(User);
        var applicant = await _db.applicants.FirstOrDefaultAsync(a => a.userId == user.Id); //retrieves the first element from applicant. 

        if (applicant == null)
        {
            return NotFound();
        }

        var courses = _db.Courses.ToList();
        var courseList = courses.Select(c => new SelectListItem
        {
            Value = c.CourseID.ToString(),
            Text = c.CourseName,
            Selected = c.CourseID == applicant.courseID
        }).ToList();

        var universities = _db.Universities.ToList();
        var uniList = universities.Select(u => new SelectListItem
        {
            Value = u.UniID.ToString(),
            Text = u.UniName,
            Selected = u.UniID == applicant.uniID
        }).ToList();

        var viewModel = new ApplicantsViewModel
        {
            ApplicantID = applicant.applicantID,
            FirstName = applicant.firstName,
            LastName = applicant.lastName,
            Email = applicant.email,
            Gpa = applicant.gpa,
            Resume = applicant.resume,
            CoverLetter = applicant.coverLetter,
            CourseID = applicant.courseID,
            UniID = applicant.uniID,
            courseList = courseList,
            uniList = uniList
        };

        return View(viewModel);
    }

    //user edit application
    [HttpPost]
    public async Task<IActionResult> Edit(ApplicantsViewModel m)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.GetUserAsync(User);
            var applicant = await _db.applicants.FirstOrDefaultAsync(a => a.userId == user.Id);

            if (applicant == null)
            {
                return NotFound();
            }

            applicant.firstName = m.FirstName;
            applicant.lastName = m.LastName;
            applicant.email = m.Email;
            applicant.gpa = (float)m.Gpa;
            applicant.resume = m.Resume;
            applicant.coverLetter = m.CoverLetter;
            applicant.courseID = m.CourseID;
            applicant.uniID = m.UniID;

            _db.applicants.Update(applicant);
            await _db.SaveChangesAsync();

            return RedirectToAction("Success");
        }

        var courses = _db.Courses.ToList();
        var courseList = courses.Select(c => new SelectListItem
        {
            Value = c.CourseID.ToString(),
            Text = c.CourseName
        }).ToList();

        var universities = _db.Universities.ToList();
        var uniList = universities.Select(u => new SelectListItem
        {
            Value = u.UniID.ToString(),
            Text = u.UniName
        }).ToList();

        m.courseList = courseList;
        m.uniList = uniList;

        return View(m);
    }

    public async Task<IActionResult> Display()
    {
        var user = await userManager.GetUserAsync(User);
        var applicant = await _db.applicants.FirstOrDefaultAsync(a => a.userId == user.Id);

        if (applicant == null)
        {
            return RedirectToAction("NoApp");
        }

        var viewModel = new ApplicantsViewModel
        {
            ApplicantID = applicant.applicantID,
            FirstName = applicant.firstName,
            LastName = applicant.lastName,
            Email = applicant.email,
            Gpa = applicant.gpa,
            Resume = applicant.resume,
            CoverLetter = applicant.coverLetter,
            CourseID = applicant.courseID,
            UniID = applicant.uniID
        };

        return View(viewModel);
    }

    public IActionResult Success()
    {
        return View();
    }
    //Delete application 
    public async Task<IActionResult> Delete(int id)
    {
        var applicant = await _db.applicants.FindAsync(id);
        if (applicant == null)
        {
            return NotFound();
        }

        // Check if the logged-in user is the owner of the application
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (applicant.userId != userId)
        {
            return Unauthorized();
        }

        _db.applicants.Remove(applicant);
        await _db.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    //when no application is found
    public IActionResult NoApp()
    {
        return View();
    }

}
