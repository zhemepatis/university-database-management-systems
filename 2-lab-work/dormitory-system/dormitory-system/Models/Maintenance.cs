
namespace dormitory_system.Models;

public class Maintenance
{
    public int Id { get; set; }
    public required int RoomId { get; set; }
    public required DateOnly EntryDate { get; set; }
    public DateOnly FixedDate { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }
}