using dormitory_system.Models;

namespace dormitory_system.Repositories.Interfaces;

public interface IMaintenanceRepository
{
    public Task Add(Maintenance item);
    public Task Delete(Maintenance item);
}