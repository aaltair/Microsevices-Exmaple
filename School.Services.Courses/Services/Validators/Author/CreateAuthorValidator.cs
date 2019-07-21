using FluentValidation;
using School.Services.Courses.Dtos.Author;

namespace School.Services.Courses.Services.Validators.Author
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorValidator()
        {
            RuleFor(w => w.AuthorName).NotNull().NotEmpty();
        }
    }
}