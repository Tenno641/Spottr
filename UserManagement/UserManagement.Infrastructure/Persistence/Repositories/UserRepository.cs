using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.UserAggregate;
using UserManagement.Infrastructure.Identity;

namespace UserManagement.Infrastructure.Persistence.Repositories;

public class UserRepository: IUserRepository
{
    private readonly UserManagementDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UserRepository(UserManagementDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }
    
    public async Task CreateAsync(User user, string password)
    {
        ApplicationUser applicationUser = new ApplicationUser(fullName: user.Name)
        {
            Email = user.Email,
            UserName = user.Email
        };

        await _userManager.CreateAsync(applicationUser, password);
        await _dbContext.SaveChangesAsync();
    }
}