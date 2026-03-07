using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.UserAggregate;
using UserManagement.Infrastructure.Identity;

namespace UserManagement.Infrastructure.Persistence;

public class UserManagementDbContext: IdentityDbContext<ApplicationUser>
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options): base(options) { }
    
    public DbSet<ApplicationUser> Users { get; set; }
    
    
    
    
}