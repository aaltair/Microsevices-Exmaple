using School.Common.Command.Interfaces;

namespace School.Common.Command
{
    public class CreateAuthorCommand : IAuthenticatedCommand
    {
        public string UserId { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}