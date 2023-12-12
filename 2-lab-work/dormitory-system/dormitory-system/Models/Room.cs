namespace dormitory_system.Models;

public class Room
{
    public int Id { get; set; }
    public required int DormitoryId { get; set; }
    public required string Number { get; set; }
    public required int Capacity { get; set; }
    public required bool Availability { get; set; }
}