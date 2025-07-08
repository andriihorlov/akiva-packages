namespace SpecialNeeds.Cloudata.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ThresholdBasedTransformSample
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public byte SourceId { get; set; }

        [StringLength(256)]
        public string SamplesDataUri { get; set; }

        public virtual Session Session { get; set; }

        public virtual TransformSource TransformSource { get; set; }
    }
}
