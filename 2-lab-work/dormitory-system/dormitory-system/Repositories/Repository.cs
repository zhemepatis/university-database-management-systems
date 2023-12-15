using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Repositories;

public abstract class Repository<T> : IRepository<T>
{
    protected readonly NpgsqlDataSource DataSource;

    protected Repository(NpgsqlDataSource dataSource)
    {
        DataSource = dataSource;
    }

    public abstract T Map(NpgsqlDataReader reader);
    public abstract Task Add(T item);
    public abstract Task Delete(T item);

    protected async Task<IEnumerable<T>> MapAll(NpgsqlDataReader reader)
    {
        IEnumerable<T> list = new List<T>();
        while (await reader.ReadAsync())
        {
            T item = Map(reader);
            list = list.Append(item);
        }

        return list;
    }
}