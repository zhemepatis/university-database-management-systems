using dormitory_system.Models;
using Npgsql;

namespace dormitory_system.Repositories.Interfaces;

public interface IDormitoryRepository
{
    public Task<int> Add(Dormitory item);
    public Task Delete(Dormitory item);
}