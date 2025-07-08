using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SpecialNeeds.Cloudata.Data
{
    public class SpecialNeedsContextInitializer : CreateDatabaseIfNotExists<SpecialNeedsContext>
    {
        protected override void Seed(SpecialNeedsContext context)
        {
            IList<Gender> defaultGenders = new List<Gender>
            {
                new Gender {Name = "Male"},
                new Gender {Name = "Female"},
            };

            context.Genders.AddRange(defaultGenders);

            IList<User> defaultUsers = new List<User>
            {
                new User
                {
                    Gender = defaultGenders[0],
                    ChildName = "Bob",
                    ChildAge = 8,
                    ParentPhotoUri = "https://link-to-blob-stroge",
                    CreatedAt = DateTime.Now
                }
            };

            context.Users.AddRange(defaultUsers);

            IList<TransformSource> defaultTransformSources = new List<TransformSource>
            {
                new TransformSource {Id = 0, Name = "Right Hand"},
                new TransformSource {Id = 1, Name = "Left Hand"},
                new TransformSource {Id = 2, Name = "Head"}
            };

            context.TransformSources.AddRange(defaultTransformSources);

            base.Seed(context);
        }
    }
}