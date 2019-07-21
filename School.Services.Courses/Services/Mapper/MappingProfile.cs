using AutoMapper;
using School.Common.Command;
using School.Common.Event;
using School.Services.Courses.Dtos.Author;
using School.Services.Courses.Dtos.Course;
using School.Services.Courses.Entities;

namespace School.Services.Courses.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AuthorCreatedEvent, AuthorDto>().ReverseMap();
            CreateMap<Author, CreateAuthorDto>().ReverseMap();
            CreateMap<CreateAuthorCommand, CreateAuthorDto>().ReverseMap();
            CreateMap<Author, AuthorCourseDto>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, CourseAuthorDto>().ReverseMap();
        }
    }
}