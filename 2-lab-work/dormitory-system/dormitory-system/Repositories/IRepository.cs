using Npgsql;

namespace dormitory_system.Repositories;

public interface IRepository<T>
{
    public Task Add(T item);
    public Task Delete(T item);
}