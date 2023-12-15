using dormitory_system.Models;
using Npgsql;

namespace dormitory_system.Repositories.Interfaces;

public interface IDormitoryRepository
{
    public Dormitory Map(NpgsqlDataReader reader);
    public Task<int> Add(Dormitory item);
    public Task<int> Add(Dormitory item, NpgsqlConnection conn);
    public Task Delete(Dormitory item);
}