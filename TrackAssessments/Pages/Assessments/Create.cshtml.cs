using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrackAssessments.Data;
using TrackAssessments.Model;

namespace TrackAssessments.Pages.Assessments
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _UserManager;

        public CreateModel(TrackAssessments.Data.ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _SignInManager = signInManager;
            _UserManager = userManager;
        }

        [BindProperty]
        public Assessment Assessment { get; set; }

        public IActionResult OnGet()
        {
            Assessment = new Assessment();
            Assessment.Creator = _UserManager.GetUserName(User);
            Assessment.CreateDate = DateTime.Now;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Assessment.Add(Assessment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Assessment.ID });
        }
    }
}