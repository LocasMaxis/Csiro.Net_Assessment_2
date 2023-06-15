using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Csiro.ViewModels;
using Csiro.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class ApplicantController : Controller
{
	private readonly ApplicantDbContext _db;
    private UserManager<ApplicationUser> userManager { get; }
    private SignInManager<ApplicationUser> signInManager { get; }

    public ApplicantController(ApplicantDbContext db,UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
	{
		_db = db;
        this.userManager = _userManager;
        this.signInManager = _signInManager;
    }

	public IActionResult Index()
	{
		return View();
	}

    //Drop down 
    public IActionResult Register() //Applicant(view) -> Register.cshtml(razor view)
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

		// Create instance of RegisterViewModel and  populate lists
		var viewModel = new ApplicantsViewModel
        {
			courseList = courseList,
			uniList = uniList
		};

		return View(viewModel);
	}

    //create a new application
    [HttpPost]
    public async Task<IActionResult> Register(ApplicantsViewModel m)
    {
        if (ModelState.IsValid)
        {
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

    public IActionResult Success()
    {
        return View();

    }
    //Get Applicant by ID
    public async Task<IActionResult> Edit(int id)
    {
        var applicant = await _db.applicants.FindAsync(id);
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

    //Edit user's application by ID: Applicant/display/id
    [HttpPost]
    public async Task<IActionResult> Edit(int id, ApplicantsViewModel m)
    {
        if (id != m.ApplicantID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var applicant = await _db.applicants.FindAsync(id);
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
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
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
   
    //Display User's application
    public IActionResult Display(int id)
    {
        var applicant = _db.applicants.FirstOrDefault(a => a.applicantID == id);
        if (applicant == null)
        {
            return NotFound();
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
    
    //Delete application 
    public async Task<IActionResult> Delete(int id)
    {
        var applicant = await _db.applicants.FindAsync(id);
        if (applicant == null)
        {
            return NotFound();
        }

        _db.applicants.Remove(applicant);
        await _db.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}


