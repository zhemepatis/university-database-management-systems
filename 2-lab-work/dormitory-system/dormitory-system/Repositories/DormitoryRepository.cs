﻿using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Repositories;

public class DormitoryRepository : Repository<Dormitory>, IDormitoryRepository
{
    public DormitoryRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {

    }

    public override async Task Add(Dormitory item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
    
        await using var cmd = new NpgsqlCommand("INSERT INTO gari9267.dormitories (address, manager_name, manager_surname, manager_phone_number) VALUES (@p1, @p2, @p3, @p4)", conn);
        cmd.Parameters.Add(new("p1", item.Address));
        cmd.Parameters.Add(new("p2", item.ManagerName));
        cmd.Parameters.Add(new("p3", item.ManagerSurname));
        cmd.Parameters.Add(new("p4", item.ManagerPhoneNumber));

        await cmd.ExecuteNonQueryAsync();
    }

    public override async Task Delete(Dormitory item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();

        await using var cmd = new NpgsqlCommand("DELETE FROM gari9267.dormitories WHERE id = (@p1)", conn);
        cmd.Parameters.Add(new ("p1", item.Id));

        await cmd.ExecuteNonQueryAsync();
    }
}