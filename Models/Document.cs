namespace ATM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Document
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        public DateTime Submit { get; set; }

        public DateTime? Expire { get; set; }

        public Guid PersonId { get; set; }

        public string Body { get; set; }

        [StringLength(3000)]
        public string Comment { get; set; }

        public virtual Person Person { get; set; }
    }
}
