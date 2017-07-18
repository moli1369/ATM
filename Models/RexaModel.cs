using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class Login
    {
        [DataType(DataType.Text)]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا نام کاربری خود را وارد نمائید")]
        public string User { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا کلمه ی عبور خود را وارد نمائید")]
        [Display(Name = "کلمه ی عبور")]
        public string Pass { get; set; }

        //private string captcha;
        //public string Captcha
        //{
        //    get { return captcha; }
        //    set { captcha = value; }
        //}
    }
    public partial class Document
    {
        [Display(Name = "شناسه")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "وارد کردن عنوان الزامیست")]
        [StringLength(500)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "ثبت")]
        public int Submit { get; set; }

        [Display(Name = "اتمام")]
        [DataType(DataType.DateTime)]
        public int? Expire { get; set; }

        public Guid PersonId { get; set; }

        [Display(Name = "متن")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "وارد کردن توضیحات الزامی است")]
        [StringLength(3000)]
        public string Comment { get; set; }
    }
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
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "این فیلد الزامیست")]
        [RexaUniqueUsername]
        [RegularExpression("([a-z0-9]|_)*", ErrorMessage = "نام کاربری تنها شامل حروف، عدد و زیر خط است")]
        public string Username { get; set; }

        [Required(ErrorMessage = "انتخاب کلمه ی عبور الزامیست")]
        [StringLength(4000)]
        [Display(Name = "کلمه ی عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(150)]
        [Display(Name = "نام")]
        public string Firstname { get; set; }

        [StringLength(150)]
        [Display(Name = "نام خانوادگی")]
        public string Lastname { get; set; }

        [Display(Name = "تصویر")]
        public Guid? PictureFileId { get; set; }

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