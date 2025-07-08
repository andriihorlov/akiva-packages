namespace SpecialNeeds.Cloudata.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class World
    {
        public World()
        {
            Sessions = new HashSet<Session>();
        }
        
        public World(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
