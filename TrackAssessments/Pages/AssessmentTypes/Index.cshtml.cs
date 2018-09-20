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

namespace TrackAssessments.Pages.AssessmentTypes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;

        public IndexModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AssessmentType> AssessmentType { get;set; }

        public async Task OnGetAsync()
        {
            AssessmentType = await _context.AssessmentType.ToListAsync();
        }
    }
}
