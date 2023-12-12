﻿using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Repositories;

public class FacultyDormitoryRelationRepository : Repository<FacultyDormitoryRelation>, IFacultyDormitoryRelationRepository
{
    public FacultyDormitoryRelationRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public override async Task Add(FacultyDormitoryRelation item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
    
        await using var cmd = new NpgsqlCommand("INSERT INTO gari9267.faculty_dormitory_relations (dormitory_id, faculty_id) VALUES (@p1, @p2)", conn);
        cmd.Parameters.Add(new("p1", item.DormitoryId));
        cmd.Parameters.Add(new("p2", item.FacultyId));

        await cmd.ExecuteNonQueryAsync();
    }

    public override async Task Delete(FacultyDormitoryRelation item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        
        await using var cmd = new NpgsqlCommand("DELETE FROM gari9267.faculty_dormitory_relations WHERE faculty_id = (@p1) AND dormitory_id = (@p2)", conn);
        cmd.Parameters.Add(new ("p1", item.FacultyId));
        cmd.Parameters.Add(new ("p2", item.DormitoryId));

        await cmd.ExecuteNonQueryAsync();
    }
}