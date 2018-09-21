using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrackAssessments.Model
{
    public class Destination
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Destination Name")]
        public string DestinationName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Street Address")]
        [StringLength(80)]
        public string StreetAddress { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "State")]
        [StringLength(20)]
        public string State { get; set; }

        [Display(Name = "Zip")]
        [StringLength(10)]
        public string Zip { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(30)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(30)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public string Description { get; set; }

    }
}
