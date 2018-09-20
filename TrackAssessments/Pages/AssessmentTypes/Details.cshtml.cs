﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;

        public DetailsModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public AssessmentType AssessmentType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssessmentType = await _context.AssessmentType.FirstOrDefaultAsync(m => m.ID == id);

            if (AssessmentType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
