namespace dormitory_system.Models;

public class Faculty
{
    public int Id { get; init; }
    public required string Abbreviation { get; set; }
    public required string Name { get; set; }
}