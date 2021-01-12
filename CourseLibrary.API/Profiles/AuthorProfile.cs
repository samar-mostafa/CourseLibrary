using AutoMapper;
using CourseLibrary.API.Dto;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Profiles
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>().
                ForMember(dest =>
                            dest.Name,
                            op => op.MapFrom(src => $"{src.FirstName}{src.LastName}")).
                ForMember(dest =>
                            dest.age,
                            op => op.MapFrom(src => src.DateOfBirth.GetCurrentAge()));

            CreateMap<AuthorForCreationDto, Author>();
                
        }
    }
}
