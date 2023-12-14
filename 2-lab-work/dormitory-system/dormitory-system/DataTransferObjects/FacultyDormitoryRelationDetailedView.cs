using dormitory_system.Models;

namespace dormitory_system.DataTransferObjects;

public class FacultyDormitoryRelationDetailedView
{
    public required Faculty Faculty { get; set; }
    public Dormitory? Dormitory { get; set; }

    public override string ToString()
    {
        return $"{Faculty}, {(Dormitory == null ? "NULL" : Dormitory)}";
    }
}