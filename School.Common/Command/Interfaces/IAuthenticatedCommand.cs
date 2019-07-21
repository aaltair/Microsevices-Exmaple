namespace School.Common.Command.Interfaces
{
    public interface IAuthenticatedCommand :ICommand
    {
        string UserId { get; set; }
    }
}