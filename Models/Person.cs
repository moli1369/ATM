namespace ATM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Person
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(4000)]
        public string Password { get; set; }

        [StringLength(150)]
        public string Firstname { get; set; }

        [StringLength(150)]
        public string Lastname { get; set; }

        public Guid? PictureFileId { get; set; }
    }
}
