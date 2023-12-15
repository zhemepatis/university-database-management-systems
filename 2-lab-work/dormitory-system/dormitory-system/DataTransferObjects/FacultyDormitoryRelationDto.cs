using dormitory_system.Models;
using dormitory_system.Repositories;
using Npgsql;

namespace dormitory_system.DataTransferObjects;

public class FacultyDormitoryRelationDto
{
    public required Faculty Faculty { get; set; }
    public Dormitory? Dormitory { get; set; }
    
    public override string ToString()
    {
        return $"{Faculty}, {(Dormitory == null ? "NULL" : Dormitory)}";
    }
}