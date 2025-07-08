using JetBrains.Annotations;

namespace SpecialNeeds.Cloudata.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mechanic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mechanic()
        {
            VoiceSamples = new HashSet<VoiceSample>();
            MechanicBasedTransformSample = new HashSet<MechanicsBasedTransformSample>();
        }

        public int Id { get; set; }

        //[Required]
        //[StringLength(50)]
        [CanBeNull]
        public string Name { get; set; }

        public int ModuleId { get; set; }

        public byte OrderNumber { get; set; }

        //[Required]
        //[StringLength(50)]
        [CanBeNull]
        public string Type { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime EndedAt { get; set; }

        public virtual Module Module { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoiceSample> VoiceSamples { get; set; }
        
        public virtual ICollection<MechanicsBasedTransformSample> MechanicBasedTransformSample { get; set; }

    }
}
