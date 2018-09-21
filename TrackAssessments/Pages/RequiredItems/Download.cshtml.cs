using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrackAssessments.Data;
using TrackAssessments.Model;

namespace TrackAssessments.Pages.RequiredItems
{
    [Authorize]
    public class DownloadModel : PageModel
    {
        private readonly TrackAssessments.Data.ApplicationDbContext _context;

        public DownloadModel(TrackAssessments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            if (RequiredItem.Attachment == null)
            {
                return NotFound();
            }
            ContentDisposition cd = new ContentDisposition
            {
                FileName = RequiredItem.AttachmentFileName,
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(RequiredItem.Attachment, RequiredItem.AttachmentContentType);
        }
    }
}
