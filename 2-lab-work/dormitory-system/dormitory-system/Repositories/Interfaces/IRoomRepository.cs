using dormitory_system.Models;
using Npgsql;

namespace dormitory_system.Repositories.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    public Task<IEnumerable<Room>> GetAll();
}