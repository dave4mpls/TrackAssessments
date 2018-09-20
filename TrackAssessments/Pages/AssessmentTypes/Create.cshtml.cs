﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public CreateModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AssessmentType AssessmentType { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AssessmentType.Add(AssessmentType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}