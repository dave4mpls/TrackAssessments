using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            ViewData["AssessmentTypeID"] = new SelectList(_context.AssessmentType.OrderBy(s => s.AssessmentTypeName), "ID", "AssessmentTypeName");
            ViewData["DestinationID"] = new SelectList(_context.Destination.OrderBy(s => s.DestinationName), "ID", "DestinationName");
            ViewData["CustomerID"] = new SelectList(_context.Customer.OrderBy(s => s.DisplayName), "ID", "DisplayName");
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
            int id = Assessment.ID;

            // now get all the related records to the assessment type,
            // so that we can add the required items to the 
            // acquired items (with the "Acquired" checkbox false, of course).
            Assessment = await _context.Assessment
                .Include(s => s.AssessmentType)
                    .ThenInclude(s => s.RequiredItems)
                .Include(s => s.AcquiredItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            foreach (RequiredItem ri in Assessment.AssessmentType.RequiredItems)
            {
                AcquiredItem ai = new AcquiredItem();
                ai.Description = ri.Description;
                ai.Attachment = ri.Attachment;
                ai.AttachmentFileName = ri.AttachmentFileName;
                ai.AttachmentContentType = ri.AttachmentContentType;
                ai.AssessmentID = id;
                ai.Acquired = false;
                ai.ItemName = ri.ItemName;
                ai.ItemType = ri.ItemType;
                ai.ItemTypeID = ri.ItemTypeID;
                _context.AcquiredItem.Add(ai);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Assessment.ID });
        }
    }
}