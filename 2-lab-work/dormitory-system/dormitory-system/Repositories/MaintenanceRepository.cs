using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Repositories;

public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
{
    public MaintenanceRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
        
    }
    
    public override async Task Add(Maintenance item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
    
        await using var cmd = new NpgsqlCommand("INSERT INTO gari9267.maintenance (room_id, type, description) VALUES (@p1, @p2, @p3)", conn);
        cmd.Parameters.Add(new("p1", item.RoomId));
        cmd.Parameters.Add(new("p2", item.Type));
        cmd.Parameters.Add(new("p3", item.Description));

        await cmd.ExecuteNonQueryAsync();
    }

    public override async Task Delete(Maintenance item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();

        await using var cmd = new NpgsqlCommand("DELETE FROM gari9267.maintenance WHERE id = (@p1)", conn);
        cmd.Parameters.Add(new ("p1", item.Id));

        await cmd.ExecuteNonQueryAsync();
    }
}