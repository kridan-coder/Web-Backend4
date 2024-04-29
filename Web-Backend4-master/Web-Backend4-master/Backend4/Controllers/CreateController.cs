using System;
using System.Globalization;
using System.Linq;
using Backend4.Models;
using Backend4.Models.Controls;
using Backend4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend4.Controllers
{
    public class CreateController : Controller
    {
        private readonly IUserSignUpService userSignUpService;

        public CreateController(IUserSignUpService userSignUpService)
        {
            this.userSignUpService = userSignUpService;
        }

        public IActionResult Index()
        {
            loadViewBag();
            var model = new FirstStepCreateViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Boolean signUp, FirstStepCreateViewModel model)
        {
            loadViewBag();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (signUp)
            {
                var allGenders = GetAllGenders();
                var gendersMap = allGenders.ToDictionary(x => x.Id);
                if (userSignUpService.UserExists(model.FirstName,model.LastName,model.Day,model.Month,model.Year,gendersMap[(int)model.Gender].Name))
                {
                    return this.View("SignUpAlreadyExists", model);
                }
                else
                {
                    return this.View("SignUpCredentials", new SecondStepCreateViewModel 
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Day = model.Day,
                        Month = model.Month,
                        Year = model.Year,
                        Gender = model.Gender,
                        Existed = false
                    });
                }
            }
            else
            {
                this.ModelState.AddModelError("", "Invalid Code");
                return this.View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpAlreadyExists(Boolean signUpAnyway, FirstStepCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (signUpAnyway)
            {
                this.ViewBag.AllMonths = GetAllMonths();
                this.ViewBag.AllGenders = GetAllGenders();
                return this.View("SignUpCredentials", new SecondStepCreateViewModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Day = model.Day,
                    Month = model.Month,
                    Year = model.Year,
                    Gender = model.Gender,
                    Existed = true
                });
            }
            else
            {
                this.ModelState.AddModelError("", "Invalid Code");
                return this.View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpCredentials(Boolean completeSignUp, SecondStepCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (completeSignUp)
            {
                loadViewBag();
                var allGenders = GetAllGenders();
                var gendersMap = allGenders.ToDictionary(x => x.Id);
                userSignUpService.CreateUser(model.FirstName, model.LastName, model.Day, model.Month, model.Year, gendersMap[(int)model.Gender].Name, model.Email, model.Password);
                return this.View("SignUpResult", new SecondStepCreateViewModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Day = model.Day,
                    Month = model.Month,
                    Year = model.Year,
                    Gender = model.Gender,
                    Email = model.Email,
                    Password = model.Password,
                    Existed = model.Existed,
                    IsRemembered = model.IsRemembered
                });
            }
            else
            {
                this.ModelState.AddModelError("", "Invalid Code");
                return this.View(model);
            }
        }


        private void loadViewBag()
        {
            this.ViewBag.AllDays = GetAllDays();
            this.ViewBag.AllMonths = GetAllMonths();
            this.ViewBag.AllYears = GetAllYears();
            this.ViewBag.AllGenders = GetAllGenders();
        }

        // ))
        private Gender[] GetAllGenders()
        {
            return new Gender[] { new Gender { Id = 0, Name = "Male" }, new Gender { Id = 1, Name = "Female" } };
        }

        private Month[] GetAllMonths()
        {
            return CultureInfo.InvariantCulture.DateTimeFormat.MonthNames
                .Select((x, i) => new Month { Id = i + 1, Name = x })
                .Where(x => !String.IsNullOrEmpty(x.Name))
                .ToArray();
        }

        private int[] GetAllYears()
        {
            return Enumerable.Range(1930, 90).Reverse().ToArray();
        }

        private int[] GetAllDays()
        {
            return Enumerable.Range(1, 31).ToArray();
        }
    }
}
