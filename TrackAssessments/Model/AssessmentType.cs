using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackAssessments.Model
{
    public class AssessmentType
    {
        public int ID { get; set; }

        [Display(Name ="Name")]
        [Required]
        [StringLength(50)]
        public string AssessmentTypeName { get; set; }

        public string Description { get; set;  }

        public string Creator { get; set; }

        [Display(Name ="Created On")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        public ICollection<RequiredItem> RequiredItems { get; set; }
    }
}
