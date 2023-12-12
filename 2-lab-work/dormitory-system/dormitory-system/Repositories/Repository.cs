using Npgsql;

namespace dormitory_system.Repositories;

public abstract class Repository<T> : IRepository<T>
{
    protected readonly NpgsqlDataSource DataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        DataSource = dataSource;
    }
    
    public abstract Task Add(T item);
    public abstract Task Delete(T item);
}