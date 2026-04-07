using GymManagement.Domain.Common;

namespace GymManagement.Domain.Trainers;

public class Trainer : Entity
{
    public Guid GymId { get; }

    public Trainer(Guid gymId, Guid? id = null) : base(id)
    {
        GymId = gymId;
    }
}