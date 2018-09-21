using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackAssessments.Model
{
    public class Assessment
    {
        public int ID { get; set; }

        [Display(Name ="Name")]
        [Required]
        [StringLength(50)]
        public string AssessmentName { get; set; }

        [Required]
        [Display(Name ="Destination")]
        public int DestinationID { get; set; }

        public Destination Destination { get; set; }

        [Required]
        [Display(Name ="Customer")]
        public int CustomerID { get; set; }

        public Customer Customer { get; set; }

        [Required]
        [Display(Name ="Assessment Type")]
        public int AssessmentTypeID { get; set; }

        [Display(Name = "Assessment Type")]
        public AssessmentType AssessmentType { get; set; }

        public string Description { get; set;  }

        public string Creator { get; set; }

        [Display(Name ="Created On")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [Display(Name ="Acquired Items")]
        public ICollection<AcquiredItem> AcquiredItems { get; set; }
    }
}
