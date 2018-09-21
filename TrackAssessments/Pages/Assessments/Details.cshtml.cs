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

namespace TrackAssessments.Pages.Assessments
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;

        public DetailsModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Assessment Assessment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assessment = await _context.Assessment
                .Include(s => s.AssessmentType)
                .Include(s => s.Customer)
                .Include(s => s.Destination)
                .Include(s => s.AcquiredItems)
                    .ThenInclude(ri => ri.Checkpoints)
                    .Include(s => s.AcquiredItems)
                    .ThenInclude(ri => ri.ItemType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Assessment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
