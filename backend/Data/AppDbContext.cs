using BackEnd.Modules.Auth.Entities;
using Microsoft.EntityFrameworkCore;
namespace BackEnd.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {

  public DbSet<User> Users => Set<User>();

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);
  }


}