using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Dto
{
    [CourseTitleAndDescription]
    public class CourseForCreationDto //:IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if(Title == Description)
        //    {
        //        yield return new ValidationResult(
        //            "the title and description must be diffrent",
        //            new[] { "CourseForCreationDto" }
        //           );
              
        //    }
       // }
    }
}
