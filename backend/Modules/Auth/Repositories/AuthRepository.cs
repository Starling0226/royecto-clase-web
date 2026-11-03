using BackEnd.Data;
using BackEnd.Modules.Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Modules.Auth.Repositories;

public class AuthRepository(AppDbContext context) {
  private readonly AppDbContext _context = context;

  public async Task<User?> GetByEmailAsync(string email) {
    return await _context.Users
      .AsNoTracking()
      .FirstOrDefaultAsync(u => u.Email == email);
  }

  public async Task<User> CreateAsync(User user) {
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return user;
  }
}
