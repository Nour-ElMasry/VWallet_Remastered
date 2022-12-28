using Domain;

namespace Application.Abstract;

public interface IUserRepository
{
    Task Save();
    Task AddUser(User u);
    Task UpdateUser(User u);
    Task DeleteUser(User u);
    Task<User> GetUserById(long id);
    Task<List<User>> GetAllUsers();
}
