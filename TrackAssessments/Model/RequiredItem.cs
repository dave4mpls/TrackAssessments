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
        public int ItemTypeID { get; set; }

        public ItemType ItemType { get; set; }        

        public string Description { get; set; }

    }
}
