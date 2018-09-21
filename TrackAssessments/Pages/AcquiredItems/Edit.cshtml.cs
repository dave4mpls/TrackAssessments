using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrackAssessments.Data;
using TrackAssessments.Model;

namespace TrackAssessments.Pages.AcquiredItems
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
        public AcquiredItem AcquiredItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AcquiredItem = await _context.AcquiredItem
                .Include(r => r.ItemType).FirstOrDefaultAsync(m => m.ID == id);

            if (AcquiredItem == null)
            {
                return NotFound();
            }
           ViewData["ItemTypeID"] = new SelectList(_context.ItemType, "ID", "Name");
            ViewData["AssessmentID"] = AcquiredItem.AssessmentID;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            AcquiredItem originalItem = await _context.AcquiredItem.FirstOrDefaultAsync(m => m.ID == AcquiredItem.ID);
            if (originalItem != null && Request.Form.Files.Count == 0)
            {
                AcquiredItem.Attachment = originalItem.Attachment;
                AcquiredItem.AttachmentFileName = originalItem.AttachmentFileName;
                AcquiredItem.AttachmentContentType = originalItem.AttachmentContentType;
            }
            _context.Entry(originalItem).State = EntityState.Detached;  // detach from original item so you can attach to new one
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

            _context.Attach(AcquiredItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcquiredItemExists(AcquiredItem.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Assessments/Details", new { id = AcquiredItem.AssessmentID } );
        }

        private bool AcquiredItemExists(int id)
        {
            return _context.AcquiredItem.Any(e => e.ID == id);
        }
    }
}
