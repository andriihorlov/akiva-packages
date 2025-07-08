namespace SpecialNeeds.Cloudata.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserAnswersReport
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        [StringLength(256)]
        public string ReportUri { get; set; }

        public virtual Session Session { get; set; }
    }
}
