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

namespace TrackAssessments.Pages.RequiredItems
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
        public RequiredItem RequiredItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RequiredItem = await _context.RequiredItem
                .Include(r => r.ItemType).FirstOrDefaultAsync(m => m.ID == id);

            if (RequiredItem == null)
            {
                return NotFound();
            }
           ViewData["ItemTypeID"] = new SelectList(_context.ItemType, "ID", "Name");
            ViewData["AssessmentTypeID"] = RequiredItem.AssessmentTypeID;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RequiredItem originalItem = await _context.RequiredItem.FirstOrDefaultAsync(m => m.ID == RequiredItem.ID);
            if (originalItem != null && Request.Form.Files.Count == 0)
            {
                RequiredItem.Attachment = originalItem.Attachment;
                RequiredItem.AttachmentFileName = originalItem.AttachmentFileName;
                RequiredItem.AttachmentContentType = originalItem.AttachmentContentType;
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

            _context.Attach(RequiredItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequiredItemExists(RequiredItem.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../AssessmentTypes/Details", new { id = RequiredItem.AssessmentTypeID } );
        }

        private bool RequiredItemExists(int id)
        {
            return _context.RequiredItem.Any(e => e.ID == id);
        }
    }
}
