using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using School.Services.Courses.Dtos;
using School.Services.Courses.Dtos.Course;
using School.Services.Courses.Entities;
using School.Services.Courses.Infrastructure.Interfaces;
using School.Services.Courses.Services.Interfaces;
using School.Services.Courses.Services.Validators.Course;

namespace School.Services.Courses.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(ICourseUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public PagedResultsDto<CourseDto> GetAllCourse(int pageIndex, int pageSize)
        {
            return new PagedResultsDto<CourseDto>
            {
                Results = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(_unitOfWork.CourseRepository.GetAll(null, o => o.OrderByDescending(w => w.CreatedBy), "", pageIndex, pageSize)),
                TotalRecords = _unitOfWork.CourseRepository.Count()
            };
        }

        public PagedResultsDto<CourseAuthorDto> GetAllCourseWithAuthor(int pageIndex, int pageSize)
        {
            return new PagedResultsDto<CourseAuthorDto>
            {
                Results = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseAuthorDto>>(_unitOfWork.CourseRepository.GetAll(null, o => o.OrderByDescending(w => w.CreatedBy), "Author", pageIndex, pageSize)),
                TotalRecords = _unitOfWork.CourseRepository.Count()
            };
        }

        public CourseAuthorDto GetCourseById(int id) =>
            _mapper.Map<Course, CourseAuthorDto>(_unitOfWork.CourseRepository
                .GetAll(w => w.CourseId == id, null, "Author").SingleOrDefault()) ?? throw new Exception("Not_Found");

        public CourseDto CreateCourse(CreateCourseDto createCourseDto, string currentUserId)
        {
            CreateCourseValidator authorValidator = new CreateCourseValidator();
            if (!authorValidator.Validate(createCourseDto).IsValid) throw new Exception("Check_Your_Fileds");
            Course course = _mapper.Map<CreateCourseDto, Course>(createCourseDto);
            course.CreatedOn = DateTime.Now;
            course.CreatedBy = currentUserId;
            _unitOfWork.CourseRepository.Add(course);
            _unitOfWork.Save();
            return _mapper.Map<Course, CourseDto>(course);
        }

        public CourseDto UpdateCourse(CourseDto courseDto, string currentUserId)
        {
            UpdateCourseValidator authorValidator = new UpdateCourseValidator();
            if (!authorValidator.Validate(courseDto).IsValid) throw new Exception("Empty_Null");
            Course course = _unitOfWork.CourseRepository.GetById(courseDto.CourseId) ?? throw new Exception("Not_Found");
            course.CourseName = courseDto.CourseName;
            course.CourseNameEn = courseDto.CourseNameEn;
            course.AuthorId = courseDto.AuthorId;
            course.CourseCategory = courseDto.CourseCategory;
            course.CourseCategoryEn = courseDto.CourseCategoryEn;
            course.UpdateOn = DateTime.Now;
            course.UpdateBy = currentUserId;
            _unitOfWork.CourseRepository.Update(course);
            _unitOfWork.Save();
            return _mapper.Map<Course, CourseDto>(course);
        }

        public CourseDto DeleteCourse(int id, string currentUserId)
        {
            Course course = _unitOfWork.CourseRepository.GetById(id) ?? throw new Exception("Not_Found");
            course.IsDelete = true;
            course.UpdateOn = DateTime.Now;
            course.UpdateBy = currentUserId;
            _unitOfWork.CourseRepository.Update(course);
            _unitOfWork.Save();

            return _mapper.Map<Course, CourseDto>(course);
        }
    }
}