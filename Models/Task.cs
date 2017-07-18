namespace ATM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            Memberships = new HashSet<Membership>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(450)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int Start { get; set; }

        public int End { get; set; }

        public Guid ProjectId { get; set; }

        public virtual DateDimension DateDimension { get; set; }

        public virtual DateDimension DateDimension1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membership> Memberships { get; set; }

        public virtual Project Project { get; set; }
    }
}
