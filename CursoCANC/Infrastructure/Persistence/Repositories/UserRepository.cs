using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDBContext _context;

    public UserRepository(ApplicationDBContext context)
    {
        _context = context ?? throw new ArgumentException(nameof(context));
    }

    public async Task<List<User>> GetAll() => await _context.Users.Where(u => u.Active == true).ToListAsync();

    public async Task<User?> GetByIdAsync(UserId id) => await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

    public async Task<bool> ExistAsync(UserId id) => await _context.Users.AnyAsync(u => u.Active == true);

    public void Add(User user) => _context.Users.Add(user);

    public void Update(User user) => _context.Users.Update(user);

    public void Delete(User user) => _context.Users.Remove(user);
}