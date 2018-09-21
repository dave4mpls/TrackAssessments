using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrackAssessments.Data;
using TrackAssessments.Model;

namespace TrackAssessments.Pages.Destinations
{
    public class IndexModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;

        public IndexModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Destination> Destination { get;set; }

        public async Task OnGetAsync()
        {
            Destination = await _context.Destination.ToListAsync();
        }
    }
}
