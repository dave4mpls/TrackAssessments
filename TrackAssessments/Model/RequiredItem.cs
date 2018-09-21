using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackAssessments.Model
{
    public class RequiredItem
    {
        public int ID { get; set; }
        public int AssessmentTypeID { get; set; }

        [Display(Name = "Item Name")]
        [Required]
        [StringLength(50)]
        public string ItemName { get; set; }

        [Required]
        [Display(Name ="Item Type")]
        public int ItemTypeID { get; set; }

        [Display(Name = "Item Type")]
        public ItemType ItemType { get; set; }        

        public string Description { get; set; }

        public byte[] Attachment { get; set; }
        public string AttachmentContentType { get; set; }
        public string AttachmentFileName { get; set; }
    }
}
