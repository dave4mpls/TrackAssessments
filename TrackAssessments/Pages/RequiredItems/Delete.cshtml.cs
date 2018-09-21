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

namespace TrackAssessments.Pages.RequiredItems
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
        public RequiredItem RequiredItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RequiredItem = await _context.RequiredItem
                .Include(r => r.ItemType).FirstOrDefaultAsync(m => m.ID == id);

            if (RequiredItem == null)
            {
                return NotFound();
            }
            ViewData["AssessmentTypeID"] = RequiredItem.AssessmentTypeID;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RequiredItem = await _context.RequiredItem.FindAsync(id);
            int parentID = RequiredItem.AssessmentTypeID;

            if (RequiredItem != null)
            {
                _context.RequiredItem.Remove(RequiredItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../AssessmentTypes/Details", new { id = parentID } );
        }
    }
}
