using ErrorOr;
using GymManagement.Application.Common.Interface;
using GymManagement.Domain.Common.Entities;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler: IRequestHandler<CreateGymCommand, ErrorOr<Guid>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    
    public CreateGymCommandHandler(ISubscriptionsRepository subscriptionsRepository, IEquipmentRepository equipmentRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _equipmentRepository = equipmentRepository;
    }
    
    public async Task<ErrorOr<Guid>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        Subscription? subscription = await _subscriptionsRepository.GetSubscriptionByIdAsync(request.SubscriptionId);

        if (subscription is null)
            return Error.NotFound();
        
        List<Equipment> equipments = request.Equipments.ConvertAll(equipmentName => new Equipment(equipmentName));
        
        await _equipmentRepository.AddEquipmentsAsync(equipments);

        Gym gym = new Gym(
            subscriptionId: request.SubscriptionId,
            equipments: equipments,
            maxRooms: subscription.GetMaxRooms(),
            name: request.Name);
        
        ErrorOr<Created> result = subscription.AddGym(gym);

        if (result.IsError)
            return result.Errors;
        
        await _subscriptionsRepository.UpdateAsync(subscription);

        return gym.Id;
    }
}