using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Dto;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [Route("api/authorcollections")]
    [ApiController]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseLibraryRepository _courseLibraryRepository;

        public AuthorCollectionsController(IMapper mapper,ICourseLibraryRepository courseLibraryRepository)
        {
            _mapper = mapper;
            _courseLibraryRepository = courseLibraryRepository;
        }


        [HttpGet("{ids}", Name = "GetAuthorCollection")]
        public ActionResult GetAuthorCollection( [FromRoute]
                [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if(ids == null)
            {
                return BadRequest();
            }

            var authorEntities = _courseLibraryRepository.GetAuthors(ids);

            if(ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorEntities));

        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(IEnumerable<AuthorForCreationDto>authorsDto)
        {
            var authorEntities = _mapper.Map <IEnumerable<Author>>(authorsDto);
            foreach (var author in authorEntities)
            {
                _courseLibraryRepository.AddAuthor(author);               
            }

            _courseLibraryRepository.Save();

            var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            var stringIds = string.Join(',', authorCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorCollection",
                new { ids = stringIds},authorCollectionToReturn);
        }
    }
}
