namespace School.Services.Identity.Dtos
{
    public class SignUpDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}