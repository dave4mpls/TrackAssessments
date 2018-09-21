using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TrackAssessments.Model
{
    public class AcquiredItem
    {
        public int ID { get; set; }
        public int AssessmentID { get; set; }

        public bool Acquired { get; set; }

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

        [StringLength(50)]
        public string Barcode { get; set; }

        public byte[] Attachment { get; set; }
        public string AttachmentContentType { get; set; }
        public string AttachmentFileName { get; set; }

        [Display(Name = "Checkpoints")]
        public ICollection<Checkpoint> Checkpoints { get; set; }
    }
}
