namespace dormitory_system.Models;

public class Student
{
    public int Id { get; init; }
    public required int StudentId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public required int FacultyId { get; set; }

    public override string ToString()
    {
        return $"{Id}, {StudentId}, {Name}, {Surname}, {Email}, {Address ?? "NULL"}, {PhoneNumber ?? "NULL"}, {FacultyId}";
    }
}