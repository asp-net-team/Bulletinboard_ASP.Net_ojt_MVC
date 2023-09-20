using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

namespace Bulletinboard.Web.Models
{
    public class UserViewModel
    {
        // Designing user view model with data annotation validation
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name can't be blank")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [Display(Name = "E-Mail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password  can't be blank")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password can't be blank")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match. 1")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Type  can't be blank")]
        public int Type { get; set; }

        [Required(ErrorMessage = "Phone can't be blank")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Birthday can't be blank")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Date of births")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Address  can't be blank")]
        [MinLength(5, ErrorMessage = "Address must be minimum length of '5'")]
        [MaxLength(255, ErrorMessage = "Address must be maximum length of '255'")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Profile can't be blank")]
        [FileSize(5 * 1024 * 1024, ErrorMessage = "File size must be up to 5MB.")]
        [AllowedFileTypes("jpg,jpeg,png", ErrorMessage = "Only JPG, JPEG, and PNG files are allowed.")]
        public HttpPostedFileBase Profile { get; set; }

        // File size validation using customize annotation
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class FileSizeAttribute : ValidationAttribute
        {
            private readonly int _maxSize;

            public FileSizeAttribute(int maxSize)
            {
                _maxSize = maxSize;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var file = value as HttpPostedFileBase;

                if (file == null)
                    return new ValidationResult("Invalid file.");

                if (file.ContentLength > _maxSize)
                    return new ValidationResult(ErrorMessage);

                return ValidationResult.Success;
            }
        }

        // File type validation using customize annotation
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class AllowedFileTypesAttribute : ValidationAttribute
        {
            private readonly string _allowedTypes;

            public AllowedFileTypesAttribute(string allowedTypes)
            {
                _allowedTypes = allowedTypes;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var file = value as HttpPostedFileBase;

                if (file == null)
                    return new ValidationResult("Invalid file.");

                var fileExtension = Path.GetExtension(file.FileName);

                if (string.IsNullOrEmpty(fileExtension) || !_allowedTypes.Contains(fileExtension.ToLower()))
                    return new ValidationResult(ErrorMessage);

                return ValidationResult.Success;
            }
        }
    }

    // Designing user edit model
    public class UserEditModel : UserViewModel
    {
        public HttpPostedFileBase ProfileEdit { get; set; }
        public string ProfilePath { get; set; }
    }

    // Designing actual user model
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Type { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Profile { get; set; }
    }
}
