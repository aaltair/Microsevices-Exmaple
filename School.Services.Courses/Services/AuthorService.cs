using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using School.Services.Courses.Dtos;
using School.Services.Courses.Dtos.Author;
using School.Services.Courses.Entities;
using School.Services.Courses.Infrastructure.Interfaces;
using School.Services.Courses.Services.Interfaces;
using School.Services.Courses.Services.Validators.Author;

namespace School.Services.Courses.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ICourseUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(ICourseUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public PagedResultsDto<AuthorDto> GetAllAuthors(int pageIndex, int pageSize)
        {
            return new PagedResultsDto<AuthorDto>
            {
                TotalRecords = _unitOfWork.AuthorRepository.Count(),
                Results = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(_unitOfWork.AuthorRepository.GetAll(null, o => o.OrderByDescending(w => w.CreatedBy), "", pageIndex, pageSize))
            };
        }
        public PagedResultsDto<AuthorCourseDto> GetAllAuthorsWithCourses(int pageIndex, int pageSize)
        {
            return new PagedResultsDto<AuthorCourseDto>
            {
                TotalRecords = _unitOfWork.AuthorRepository.Count(),
                Results = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorCourseDto>>(_unitOfWork.AuthorRepository.GetAll(null, o => o.OrderByDescending(w => w.CreatedBy), "Courses", pageIndex, pageSize))
            };
        }
        public AuthorCourseDto GetAuthorById(int id) => _mapper.Map<Author, AuthorCourseDto>(_unitOfWork.AuthorRepository.GetAll(w => w.AuthorId == id, null, "Courses").SingleOrDefault()) ?? throw new Exception("Not_Found");
        public AuthorDto CreateAuthor(CreateAuthorDto createAuthor, string currentUserId)
        {
            CreateAuthorValidator authorValidator = new CreateAuthorValidator();
            if (!authorValidator.Validate(createAuthor).IsValid) throw new Exception("Empty_Null");
            Author author = _mapper.Map<CreateAuthorDto, Author>(createAuthor);
            author.CreatedOn = DateTime.Now;
            author.CreatedBy = currentUserId;
            _unitOfWork.AuthorRepository.Add(author);
            _unitOfWork.Save();
            return _mapper.Map<Author, AuthorDto>(author);
        }
        public AuthorDto UpdateAuthor(AuthorDto authorDto, string currentUserId)
        {
            UpdateAuthorValidator authorValidator = new UpdateAuthorValidator();
            if (!authorValidator.Validate(authorDto).IsValid) throw new Exception("Empty_Null");
            Author author = _unitOfWork.AuthorRepository.GetById(authorDto.AuthorId);
            if (author == null) throw new Exception("Not_Found");
            author.AuthorName = authorDto.AuthorName;
            author.UpdateOn = DateTime.Now;
            author.UpdateBy = currentUserId;
            _unitOfWork.AuthorRepository.Update(author);
            _unitOfWork.Save();
            return _mapper.Map<Author, AuthorDto>(author);
        }
        public AuthorDto DeleteAuthor(int id, string currentUserId)
        {
            Author author = _unitOfWork.AuthorRepository.GetById(id);
            if (author == null) throw new Exception("Not_Found");
            author.UpdateOn = DateTime.Now;
            author.UpdateBy = currentUserId;
            author.IsDelete = true;
            _unitOfWork.AuthorRepository.Update(author);
            _unitOfWork.Save();
            return _mapper.Map<Author, AuthorDto>(author);
        }

    }
}