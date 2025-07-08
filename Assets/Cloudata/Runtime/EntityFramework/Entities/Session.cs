using JetBrains.Annotations;

namespace SpecialNeeds.Cloudata.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Session
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Session()
        {
            GazeDefocusingPeriods = new HashSet<GazeDefocusingPeriod>();
            GazeFocusingSamples = new HashSet<GazeFocusingSample>();
            ThresholdBasedTransformSamples = new HashSet<ThresholdBasedTransformSample>();
            UserAnswersReports = new HashSet<UserAnswersReport>();
            VoiceSamples = new HashSet<VoiceSample>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public byte AvatarId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartedAt { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndedAt { get; set; }

        public int ModuleID { get; set; }

        public int WorldID { get; set; }
        
        public string AkivaVrVersion { get; set; }
        
        public string AkivaCompanionVersion { get; set; }

        public string AkivaVrName { get; set; }

        public virtual Avatar Avatar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GazeDefocusingPeriod> GazeDefocusingPeriods { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GazeFocusingSample> GazeFocusingSamples { get; set; }

        public virtual Module Module { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThresholdBasedTransformSample> ThresholdBasedTransformSamples { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAnswersReport> UserAnswersReports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoiceSample> VoiceSamples { get; set; }
    }
}
