using FluentValidation;
using School.Services.Courses.Dtos.Author;

namespace School.Services.Courses.Services.Validators.Author
{
    public class UpdateAuthorValidator : AbstractValidator<AuthorDto>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(w => w.AuthorId).NotNull().NotEmpty();
            RuleFor(w => w.AuthorName).NotNull().NotEmpty();
        }
    }
}