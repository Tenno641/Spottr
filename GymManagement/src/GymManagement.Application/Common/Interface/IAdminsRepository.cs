using GymManagement.Domain.AdminAggregate;

namespace GymManagement.Application.Common.Interface;

public interface IAdminsRepository
{
    Task<Admin?> GetByIdAsync(Guid id);
    Task UpdateAsync(Admin admin);
    Task AddAsync(Admin admin);
}