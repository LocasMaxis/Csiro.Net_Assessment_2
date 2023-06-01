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


	public IActionResult Register() //Applicant(view) -> Register.cshtml(razor view)
	{
		// Get courses from the database and populate courseList
		var courses = _db.courses.ToList();
		var courseList = courses.Select(c => new SelectListItem
		{
			Value = c.courseID.ToString(),
			Text = c.courseName
		}).ToList();

		// Get universities from the database and populate uniList
		var universities = _db.universities.ToList();
		var uniList = universities.Select(u => new SelectListItem
		{ 
			Value = u.uniID.ToString(),
			Text = u.uniName
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
