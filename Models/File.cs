namespace ATM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class File
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public File()
        {
            People = new HashSet<Person>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(600)]
        public string Name { get; set; }

        public int Lenght { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        public byte[] Bytes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> People { get; set; }
    }
}
