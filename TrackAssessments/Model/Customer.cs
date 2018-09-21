using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrackAssessments.Model
{
    public class Customer
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        [StringLength(80)]
        public string Company { get; set; }

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

        [Display(Name ="Full Name")]
        public string DisplayName
        {
            get
            {
                string s = FirstName + " " + LastName;
                if (Company != null) s = Company + ": " + s;
                return s;
            }
        }

        //[Display(Name = "Assessments")]
        //public ICollection<Assessments> Assessments { get; set; }
    }
}
