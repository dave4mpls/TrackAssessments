using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrackAssessments.Data;
using TrackAssessments.Model;

namespace TrackAssessments.Pages.AcquiredItems
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;

        public DeleteModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AcquiredItem AcquiredItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AcquiredItem = await _context.AcquiredItem
                .Include(r => r.ItemType).FirstOrDefaultAsync(m => m.ID == id);

            if (AcquiredItem == null)
            {
                return NotFound();
            }
            ViewData["AssessmentID"] = AcquiredItem.AssessmentID;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AcquiredItem = await _context.AcquiredItem.FindAsync(id);
            int parentID = AcquiredItem.AssessmentID;

            if (AcquiredItem != null)
            {
                _context.AcquiredItem.Remove(AcquiredItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Assessments/Details", new { id = parentID } );
        }
    }
}
