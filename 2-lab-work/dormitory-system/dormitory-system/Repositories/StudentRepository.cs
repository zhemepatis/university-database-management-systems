using System.Data;
using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
        
    }
    
    public async Task<IEnumerable<Student>> GetAll()
    {
        await using var conn = await DataSource.OpenConnectionAsync();
    
        await using var cmd = new NpgsqlCommand("SELECT * FROM gari9267.students", conn);
        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        return await MapAll(reader);
    }

    public override Student Map(NpgsqlDataReader reader)
    {
        return new Student
        {
            Id = reader.GetInt32("id"),
            StudentId = reader.GetInt32("student_id"),
            Name = reader.GetString("name"),
            Surname = reader.GetString("surname"),
            Email = reader.GetString("email"),
            PhoneNumber = reader.IsDBNull("phone_number") ? null : reader.GetString("phone_number"),
            Address = reader.IsDBNull("address") ? null : reader.GetString("address"),
            FacultyId = reader.GetInt32("faculty_id")
        };
    }

    public override async Task Add(Student item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
    
        await using var cmd = new NpgsqlCommand("INSERT INTO gari9267.students (student_id, name, surname, email, phone_number, address, faculty_id) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7)", conn);
        cmd.Parameters.Add(new("p1", item.StudentId));
        cmd.Parameters.Add(new("p2", item.Name));
        cmd.Parameters.Add(new("p3", item.Surname));
        cmd.Parameters.Add(new("p4", item.Email));
        cmd.Parameters.Add(new("p5", item.PhoneNumber == null ? DBNull.Value : item.PhoneNumber));
        cmd.Parameters.Add(new("p6", item.Address == null ? DBNull.Value : item.Address));
        cmd.Parameters.Add(new("p7", item.FacultyId));

        await cmd.ExecuteNonQueryAsync();
    }

    public override async Task Delete(Student item)
    {
        await using var conn = await DataSource.OpenConnectionAsync();

        await using var cmd = new NpgsqlCommand("DELETE FROM gari9267.students WHERE id = (@p1)", conn);
        cmd.Parameters.Add(new ("p1", item.Id));

        await cmd.ExecuteNonQueryAsync();
    }
}