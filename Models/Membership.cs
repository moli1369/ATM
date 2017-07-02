namespace ATM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Membership
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }

        public virtual Task Task { get; set; }
    }
}
