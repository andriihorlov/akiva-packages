namespace SpecialNeeds.Cloudata.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GazeFocusingSample
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public float FocusPercentage { get; set; }

        public virtual Session Session { get; set; }
    }
}
