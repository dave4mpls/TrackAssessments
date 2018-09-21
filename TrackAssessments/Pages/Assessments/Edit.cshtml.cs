using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrackAssessments.Data;
using TrackAssessments.Model;

namespace TrackAssessments.Pages.Assessments
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;

        public EditModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Assessment Assessment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assessment = await _context.Assessment.FirstOrDefaultAsync(m => m.ID == id);

            if (Assessment == null)
            {
                return NotFound();
            }
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

            _context.Attach(Assessment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssessmentExists(Assessment.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssessmentExists(int id)
        {
            return _context.Assessment.Any(e => e.ID == id);
        }
    }
}
