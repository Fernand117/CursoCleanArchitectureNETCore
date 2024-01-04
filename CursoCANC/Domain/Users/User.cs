using Domain.Primitives;

namespace Domain.Users
{
    public class User : AgregateRoot
    {
        public User() { }

        public User(UserId id, string username, string email, string password)
        {
            Id = id;
            UserName = username;
            Email = email;
            Password = password;
        }

        public UserId Id { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public bool Active { get; private set; }

        public static User UpdateUser(Guid id,  string username, string email, string password)
        {
            return new User(new UserId(id), username, email, password);
        }
    }
}
