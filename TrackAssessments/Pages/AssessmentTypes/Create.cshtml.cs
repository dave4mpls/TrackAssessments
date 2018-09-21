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

namespace TrackAssessments.Pages.AssessmentTypes
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
        public AssessmentType AssessmentType { get; set; }

        public IActionResult OnGet()
        {
            AssessmentType = new AssessmentType();
            AssessmentType.Creator = _UserManager.GetUserName(User);
            AssessmentType.CreateDate = DateTime.Now;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AssessmentType.Add(AssessmentType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = AssessmentType.ID });
        }
    }
}