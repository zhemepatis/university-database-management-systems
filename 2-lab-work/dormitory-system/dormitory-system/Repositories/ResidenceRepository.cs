using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Repositories;

public class ResidenceRepository : Repository<Residence>, IResidenceRepository
{
    public ResidenceRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public override async Task Add(Residence item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
    
        await using var cmd = new NpgsqlCommand("INSERT INTO gari9267.residence (student_id, room_id, resided_from) VALUES (@p1, @p2, @p3)", conn);
        cmd.Parameters.Add(new("p1", item.StudentId));
        cmd.Parameters.Add(new("p2", item.RoomId));
        cmd.Parameters.Add(new("p3", item.ResidedFrom));

        await cmd.ExecuteNonQueryAsync();
    }

    public override async Task Delete(Residence item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();

        await using var cmd = new NpgsqlCommand("DELETE FROM gari9267.residence WHERE id = (@p1)", conn);
        cmd.Parameters.Add(new ("p1", item.Id));

        await cmd.ExecuteNonQueryAsync();
    }
}