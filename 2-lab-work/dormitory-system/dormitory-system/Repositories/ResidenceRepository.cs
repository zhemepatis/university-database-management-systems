using System.Data;
using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Repositories;

public class ResidenceRepository : Repository<Residence>, IResidenceRepository
{
    public ResidenceRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public override Residence Map(NpgsqlDataReader reader)
    {
        return new Residence
        {
            Id = reader.GetInt32("id"),
            StudentId = reader.GetInt32("student_id"),
            RoomId = reader.GetInt32("room_id"),
            ResidedFrom = DateOnly.FromDateTime(reader.GetDateTime("resided_from")),
            ResidedUntil = reader.IsDBNull("resided_until") ? null : DateOnly.FromDateTime(reader.GetDateTime("resided_until"))
        };
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