using CourseLibrary.API.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ValidationAttributes
{
    public class CourseTitleAndDescription:ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CourseForCreationDto)validationContext.ObjectInstance;
            if(course.Title == course.Description)
            {
                return new ValidationResult("Title and Discription must be diffrent",
                    new[] { "CourseForCreationDto"});
            }

            return ValidationResult.Success;
        }
    }
}
