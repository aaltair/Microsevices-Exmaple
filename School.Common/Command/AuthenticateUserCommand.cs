using School.Common.Command.Interfaces;

namespace School.Common.Command
{
    public class AuthenticateUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}