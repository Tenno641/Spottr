using GymManagement.Application.Common.Interface;
using GymManagement.Domain.AdminAggregate;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class AdminsRepository: IAdminsRepository
{
    private readonly GymManagementDbContext _dbContext;
    
    public AdminsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Admin?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Admins.FirstOrDefaultAsync(admin => admin.Id == id);
    }
    
    public Task UpdateAsync(Admin admin)
    {
        _dbContext.Admins.Update(admin);
        return _dbContext.SaveChangesAsync();
    }
}