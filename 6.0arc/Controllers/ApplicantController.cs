using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Csiro.ViewModels;
using Csiro.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

public class ApplicantController : Controller
{
	private readonly ApplicantDbContext _db;

	public ApplicantController(ApplicantDbContext db)
	{
		_db = db;
    }

	/*public IActionResult Register()
	{
		return View();
	}*/

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
		var viewModel = new RegisterViewModel
		{
			courseList = courseList,
			uniList = uniList
		};

		return View(viewModel);
	}
    public IActionResult Success() //Applicant(view) --> Success.cshtml(razor view)
    {
        return View();
    }


}
