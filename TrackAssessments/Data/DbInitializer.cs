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
            // First, initialize drop-down index tables; this is done even in production.
            var itemTypes = new[]
            {
            new ItemType{Name = "Online Form" },
            new ItemType{Name = "Paper Document" },
            new ItemType{Name = "Photograph" },
            new ItemType{Name = "Attached File" },
            new ItemType{Name = "Physical Object" }
            };

            //--- Note that this method shown below will add any items that aren't already there, while leaving the rest alone.
            foreach (ItemType it in itemTypes)
            {
                var q = from i in context.ItemType
                        where i.Name == it.Name
                        select i;
                if (!q.Any()) 
                    context.ItemType.Add(it);
            }
            context.SaveChanges();

            // Look for any assessments.  If there aren't any, we can add our initial development data.
            if (context.AssessmentType.Any())
            {
                return;   // DB has been seeded
            }

        }
    }
}
