using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackAssessments.Model
{
    public class ItemType
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
