using ErrorOr;
using GymManagement.Application.Common.Interface;
using GymManagement.Application.Gyms.Queries.QueryGym;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.GetGym;

public class GetGymQueryHandler: IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public GetGymQueryHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
    {
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }
    
    public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymsRepository.GetGymByIdAsync(request.GymId);
        if (gym is null)
            return Error.NotFound(description: "Gym is not found");

        return gym;
    }
}