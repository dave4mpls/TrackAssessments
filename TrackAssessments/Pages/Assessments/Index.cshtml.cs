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
    public class IndexModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;

        public IndexModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Assessment> Assessment { get;set; }

        public async Task OnGetAsync()
        {
            Assessment = await _context.Assessment
                .Include(s => s.AssessmentType)
                .Include(s => s.Customer) 
                .Include(S => S.Destination)
                .ToListAsync();
        }
    }
}
