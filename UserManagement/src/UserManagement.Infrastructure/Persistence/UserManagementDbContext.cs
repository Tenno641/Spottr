using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.UserAggregate;

namespace UserManagement.Infrastructure.Persistence;

public class UserManagementDbContext: DbContext
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options): base(options) { }
    
    public DbSet<User> Users { get; set; }
    
}