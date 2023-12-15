using dormitory_system.Models;
using Npgsql;

namespace dormitory_system.Repositories.Interfaces;

public interface IFacultyDormitoryRelationRepository
{
    public Task Add(FacultyDormitoryRelation item);
    public Task Add(FacultyDormitoryRelation item, NpgsqlConnection conn);
    public Task Delete(FacultyDormitoryRelation item);
}