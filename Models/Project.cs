namespace ATM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project
    {
        public Guid Id { get; set; }

        [StringLength(350)]
        public string Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Guid OwnerId { get; set; }
    }
}
