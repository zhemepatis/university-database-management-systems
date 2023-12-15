namespace dormitory_system.DataTransferObjects;

public class CurrentResidentDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    
    public required string  StudentFacultyAbbreviation { get; set; }
    
    public required int DormitoryId { get; set; }
    public required string DormitoryFacultyAbbreviation { get; set; }
    public required string DormitoryAddress { get; set; }
    
    public required int RoomId { get; set; }
    public required string RoomNumber { get; set; }

    public override string ToString()
    {
        return $"{Id}, {Name}, {Surname}, {StudentFacultyAbbreviation}, " +
               $"{DormitoryId}, {DormitoryFacultyAbbreviation}, {DormitoryAddress}," +
               $"{RoomId}, {RoomNumber}";
    }
}