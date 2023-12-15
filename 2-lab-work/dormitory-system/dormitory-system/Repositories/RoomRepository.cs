using System.Data;
using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Repositories;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }
    
    public async Task<IEnumerable<Room>> GetAll()
    {
        await using NpgsqlConnection conn = await DataSource.OpenConnectionAsync();
        await using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM gari9267.rooms", conn);
        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        return await MapAll(reader);
    }

    public override Room Map(NpgsqlDataReader reader)
    {
        return new Room
        {
            Id = reader.GetInt32("id"),
            DormitoryId = reader.GetInt32("dormitory_id"),
            Number = reader.GetString("number"),
            Capacity = reader.GetInt32("capacity"),
            Availability = reader.GetBoolean("availability")
        };
    }

    public override async Task Add(Room item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
    
        await using var cmd = new NpgsqlCommand("INSERT INTO gari9267.rooms (dormitory_id, number, capacity, availability) VALUES (@p1, @p2, @p3, @p4)", conn);
        cmd.Parameters.Add(new("p1", item.DormitoryId));
        cmd.Parameters.Add(new("p2", item.Number));
        cmd.Parameters.Add(new("p3", item.Capacity));
        cmd.Parameters.Add(new("p4", item.Availability));

        await cmd.ExecuteNonQueryAsync();
    }

    public override async Task Delete(Room item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();

        await using var cmd = new NpgsqlCommand("DELETE FROM gari9267.rooms WHERE id = (@p1)", conn);
        cmd.Parameters.Add(new ("p1", item.Id));

        await cmd.ExecuteNonQueryAsync();
    }
}