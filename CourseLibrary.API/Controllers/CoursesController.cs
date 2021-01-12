using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Dto;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [Route("api/author/{authorId}/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseLibraryRepository _courseLibraryRepository;

        public CoursesController(IMapper mapper, ICourseLibraryRepository courseLibraryRepository)
        {
            _mapper = mapper;
            _courseLibraryRepository = courseLibraryRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
                return NotFound();

            var courses = _courseLibraryRepository.GetCourses(authorId);

            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId}",Name = "GetCourse")]
        public IActionResult GetCourseForAuthor(Guid authorId,Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId)) 
            return NotFound();

            var course = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (course == null)
                return NotFound();

            return Ok(_mapper.Map<CourseDto>(course));
        }

        [HttpPost]
        public ActionResult<CourseDto> AddCourseForAuthor(Guid authorId,CourseForCreationDto course)
        {
            var courseEntity = _mapper.Map<Course>(course);

            _courseLibraryRepository.AddCourse(authorId, courseEntity);
            _courseLibraryRepository.Save();

            var courseForReturn = _mapper.Map<CourseDto>(courseEntity);

            return CreatedAtRoute("GetCourse",
                                   new { authorId = authorId , courseId=courseForReturn.Id}, courseForReturn);

        }
             
    }
}
