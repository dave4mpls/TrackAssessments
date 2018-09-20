using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackAssessments.Model;

namespace TrackAssessments.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any assessments.
            if (context.AssessmentType.Any())
            {
                return;   // DB has been seeded
            }

            // First, initialize drop-down index tables; this is done even in production.
            var itemTypes = new[]
            {
            new ItemType{Name = "Online Form" },
            new ItemType{Name = "Paper Document" },
            new ItemType{Name = "Physical Object" }
            };

            foreach (ItemType it in itemTypes)
            {
                context.ItemType.Add(it);
            }
            context.SaveChanges();
        }
    }
}
