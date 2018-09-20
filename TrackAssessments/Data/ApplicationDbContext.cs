using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackAssessments.Model;

namespace TrackAssessments.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TrackAssessments.Model.AssessmentType> AssessmentType { get; set; }
        public DbSet<ItemType> ItemType { get; set; }
        public DbSet<ItemType> RequiredItem { get; set; }
    }
}
