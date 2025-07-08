
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CloudataMigrations.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SpecialNeeds.Cloudata.Data.SpecialNeedsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}