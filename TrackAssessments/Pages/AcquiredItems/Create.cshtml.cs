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

namespace TrackAssessments.Pages.AcquiredItems
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
        public AcquiredItem AcquiredItem { get; set; }

        public IActionResult OnGet(int? AssessmentID)
        {
            if (AssessmentID == null)
            {
                return NotFound();
            }
            ViewData["ItemTypeID"] = new SelectList(_context.ItemType, "ID", "Name");
            ViewData["AssessmentID"] = (int)AssessmentID;
            AcquiredItem = new AcquiredItem();
            AcquiredItem.ItemTypeID = 1;
            AcquiredItem.AssessmentID = (int) AssessmentID;
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
                    AcquiredItem.Attachment = buffer;
                    AcquiredItem.AttachmentFileName = Request.Form.Files[0].FileName;
                    AcquiredItem.AttachmentContentType = Request.Form.Files[0].ContentType;
                }
                else
                {
                    s.Close();
                    ModelState.AddModelError(string.Empty, "The attachment is too large.  The maximum size is 20MB.");
                    return Page();
                }
            }

            _context.AcquiredItem.Add(AcquiredItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Assessments/Details", new { id = AcquiredItem.AssessmentID });
        }
    }
}