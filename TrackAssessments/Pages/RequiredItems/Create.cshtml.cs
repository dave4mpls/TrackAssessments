using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrackAssessments.Data;
using TrackAssessments.Model;

namespace TrackAssessments.Pages.RequiredItems
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;

        public CreateModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RequiredItem RequiredItem { get; set; }

        public IActionResult OnGet(int? AssessmentTypeID)
        {
            if (AssessmentTypeID == null)
            {
                return NotFound();
            }
            ViewData["ItemTypeID"] = new SelectList(_context.ItemType, "ID", "Name");
            ViewData["AssessmentTypeID"] = (int)AssessmentTypeID;
            RequiredItem = new RequiredItem();
            RequiredItem.ItemTypeID = 1;
            RequiredItem.AssessmentTypeID = (int) AssessmentTypeID;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Request.Form.Files.Count > 0)
            {
                Stream s = Request.Form.Files[0].OpenReadStream();
                if (s.Length < 20 * 1024 * 1024)
                {
                    byte[] buffer = new byte[s.Length];
                    s.Read(buffer, 0, (int)s.Length);
                    s.Close();
                    RequiredItem.Attachment = buffer;
                    RequiredItem.AttachmentFileName = Request.Form.Files[0].FileName;
                    RequiredItem.AttachmentContentType = Request.Form.Files[0].ContentType;
                }
                else
                {
                    s.Close();
                    ModelState.AddModelError(string.Empty, "The attachment is too large.  The maximum size is 20MB.");
                    return Page();
                }
            }

            _context.RequiredItem.Add(RequiredItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("../AssessmentTypes/Details", new { id = RequiredItem.AssessmentTypeID });
        }
    }
}