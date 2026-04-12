using GymManagement.Application.Common.Interface;
using GymManagement.Domain.AdminAggregate;
using MediatR;
using SharedKernel.UserManagement;

namespace GymManagement.Application.Admins.IntegrationEvents;

public class AdminProfileCreatedIntegrationEventHandler: INotificationHandler<AdminProfileCreatedIntegrationEvent>
{
    private readonly IAdminsRepository _adminsRepository;
    
    public AdminProfileCreatedIntegrationEventHandler(IAdminsRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }

    public async Task Handle(AdminProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Admin admin = new Admin(userId: notification.UserId, id: notification.AdminId);
        
        await _adminsRepository.AddAsync(admin);
    }
}