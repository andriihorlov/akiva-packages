namespace SpecialNeeds.Cloudata.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GazeDefocusingPeriod
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public TimeSpan StartedAt { get; set; }

        public TimeSpan EndedAt { get; set; }

        public virtual Session Session { get; set; }
    }
}
