namespace SpecialNeeds.Cloudata.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VoiceSample
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        [StringLength(256)]
        public string VoiceDataUri { get; set; }

        public int MechanicId { get; set; }

        public virtual Mechanic Mechanic { get; set; }

        public virtual Session Session { get; set; }
    }
}
