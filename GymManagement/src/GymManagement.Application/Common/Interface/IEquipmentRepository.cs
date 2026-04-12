using GymManagement.Domain.Common.Entities;

namespace GymManagement.Application.Common.Interface;

public interface IEquipmentRepository
{
    Task AddEquipmentsAsync(List<Equipment> equipments);
}