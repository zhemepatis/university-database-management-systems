using Npgsql;

namespace dormitory_system.Repositories;

public interface IRepository<T>
{
    // TODO: add Map function?
    public Task Add(T item);
    public Task Delete(T item);
}