using System.Data;
using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Repositories;

public class FacultyRepository : Repository<Faculty>, IFacultyRepository
{
    public FacultyRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public async Task<IEnumerable<Faculty>> GetAll()
    {
        await using NpgsqlConnection conn = await DataSource.OpenConnectionAsync();
        await using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM gari9267.faculties", conn);
        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        return await MapAll(reader);
    }
    
    public override Faculty Map(NpgsqlDataReader reader)
    {
        return new Faculty
        {
            Id = reader.GetInt32("id"),
            Abbreviation = reader.GetString("abbreviation"),
            Name = reader.GetString("name")
        };
    }

    public override async Task Add(Faculty item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
    
        await using var cmd = new NpgsqlCommand("INSERT INTO gari9267.faculties (abbreviation, name) VALUES (@p1, @p2)", conn);
        cmd.Parameters.Add(new("p1", item.Abbreviation));
        cmd.Parameters.Add(new("p2", item.Name));

        await cmd.ExecuteNonQueryAsync();
    }

    public override async Task Delete(Faculty item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();

        await using var cmd = new NpgsqlCommand("DELETE FROM gari9267.faculties WHERE id = (@p1)", conn);
        cmd.Parameters.Add(new ("p1", item.Id));

        await cmd.ExecuteNonQueryAsync();
    }
}