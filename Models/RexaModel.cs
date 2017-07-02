using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public partial class Person
    {
        private Guid id;
        public Guid Id
        {
            get { return id; }
            set
            {
                if (id == null)
                    id = Guid.NewGuid();
                id = value;
            }
        }

        [StringLength(50)]
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage ="این فیلد الزامیست")]
        [RexaUniqueUsername]
        public string Username { get; set; }

        [Required(ErrorMessage = "انتخاب کلمه ی عبور الزامیست")]
        [StringLength(4000)]
        [Display(Name ="کلمه ی عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(150)]
        [Display(Name = "نام")]
        public string Firstname { get; set; }

        [StringLength(150)]
        [Display(Name = "نام خانوادگی")]
        public string Lastname { get; set; }

        [Display(Name ="شناسه ی تصویر")]
        public Guid? PictureFileId { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membership> Memberships { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }
    }

    internal class RexaUniqueUsername : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var owner = validationContext.ObjectInstance as Person;
            if (owner == null) return new ValidationResult("این فیلد الزامیست");
            MainModel db = new MainModel();
            var user = db.People.FirstOrDefault(u => u.Username == (string)value && u.Id != owner.Id);

            if (user == null)
                return ValidationResult.Success;
            else
                return new ValidationResult("این نام کاربری پیشتر ثبت شده است");
        }
    }
}