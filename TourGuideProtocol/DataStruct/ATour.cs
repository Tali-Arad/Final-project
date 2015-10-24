using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TourGuideProtocol.DataStruct
{
    public class ATour
    {
        public string TourID { get; set; }
        [Required(ErrorMessage = "Please enter tour name")]
        public string TourName { get; set; }
        [Required(ErrorMessage = "Please enter tour location")]
        public string TourLocation  { get; set; }
        [Required(ErrorMessage = "Please enter tour area")]
        public string TourArea { get; set; }
        [Required(ErrorMessage = "Please enter tour category")]
        public string TourCategory { get; set; }
        [Required(ErrorMessage = "Please enter tour duration")]
        [Range(1, 200, ErrorMessage="Tour duration must be between 1 and 200 hours" )]
        public int TourDuration { get; set; }
        [Required(ErrorMessage = "Please enter tour price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal TourPrice{ get; set; }
        [Required(ErrorMessage = "Please enter tour registration minimum")]
        [Range(0, byte.MaxValue)]
        public int MinReg { get; set; }
        [Required(ErrorMessage = "Please enter tour registration maximum")]
        [Range(0, 255)]
        [CustomAttributeMinMax("MinReg", "The MaxReg value must be equal to or greater than the MinReg value")]
        public int MaxReg { get; set; }
        [Required(ErrorMessage = "Please enter tour description")]
        public string TourDescription { get; set; }
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }

    public class CustomAttributeMinMax : ValidationAttribute
    {
        private readonly string _other;
        private readonly string _errorMessage;
        public CustomAttributeMinMax(string other, string errorMessage)
        {
            _other = other;
            _errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_other);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", _other)
                );
            }
            object otherValue = property.GetValue(validationContext.ObjectInstance, null);

            // at this stage you have "value" and "otherValue" pointing
            // to the value of the property on which this attribute
            // is applied and the value of the other property respectively
            // => you could do some checks
            if ((int)otherValue > (int)value)
            {
                // here we are verifying whether the 2 values are equal
                // but you could do any custom validation you like
                return new ValidationResult(_errorMessage);
            }
            return null;
        }
    }
}
