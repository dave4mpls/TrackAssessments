using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrackAssessments.Model
{
    public class Checkpoint
    {
        public int ID { get; set; }
        public int AcquiredItemID { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string User { get; set; }

        [Display(Name = "Timestamp")]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        public string Notes { get; set; }
    }
}
