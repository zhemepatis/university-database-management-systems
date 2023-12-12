using dormitory_system.Models;

namespace dormitory_system.Repositories.Interfaces;

public interface IRoomRepository
{
    public Task Add(Room item);
    public Task Delete(Room item);
}