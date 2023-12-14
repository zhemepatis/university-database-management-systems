namespace dormitory_system.Models;

public class Residence
{
    public int Id { get; set; }
    public required int StudentId { get; set; }
    public required int RoomId { get; set; }
    public required DateOnly ResidedFrom { get; set; }
    public DateOnly? ResidedUntil { get; set; }
}