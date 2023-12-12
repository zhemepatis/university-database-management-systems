namespace dormitory_system.Models;

public class Dormitory
{
    public int Id { get; init; }
    public required string Address { get; set; }
    public required string ManagerName { get; set; }
    public required string ManagerSurname { get; set; }
    public required string ManagerPhoneNumber { get; set; }
    
    public override string ToString()
    {
        return $"{Id}, {Address}, {ManagerName}, {ManagerSurname}, {ManagerPhoneNumber}";
    }
}