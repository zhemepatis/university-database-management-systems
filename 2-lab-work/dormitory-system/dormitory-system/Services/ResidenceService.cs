using Npgsql;

namespace dormitory_system.Services;

public class ResidenceService
{
    private readonly NpgsqlDataSource _dataSource;

    public ResidenceService(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task GetCurrentResidents()
    {
        await using NpgsqlConnection conn = await _dataSource.OpenConnectionAsync();
        await using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM gari9267.current_residence_view" ,conn);

        NpgsqlDataReader reader =  await cmd.ExecuteReaderAsync();

    }

}