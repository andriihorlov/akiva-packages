namespace SpecialNeeds.Cloudata.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Module
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Module()
        {
            Mechanics = new HashSet<Mechanic>();
            Sessions = new HashSet<Session>();
        }

        public Module(int id, string name, string description, byte localization)
        {
            Id = id;
            Name = name;
            Description = description;
            LocalizationID = localization;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public byte LocalizationID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Localization Localization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mechanic> Mechanics { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
