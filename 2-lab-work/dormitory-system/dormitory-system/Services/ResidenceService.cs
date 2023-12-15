using System.Data;
using dormitory_system.DataTransferObjects;
using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Services;

public class ResidenceService
{
    private readonly NpgsqlDataSource _dataSource;

    private readonly IStudentRepository _studentRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IResidenceRepository _residenceRepository;

    public ResidenceService(NpgsqlDataSource dataSource, IStudentRepository studentRepository, IRoomRepository roomRepository, IResidenceRepository residenceRepository)
    {
        _dataSource = dataSource;
        
        _studentRepository = studentRepository;
        _roomRepository = roomRepository;
        _residenceRepository = residenceRepository;
    }

    public async Task<IEnumerable<CurrentResidentDto>> GetCurrentResidents()
    {
        await using NpgsqlConnection conn = await _dataSource.OpenConnectionAsync();
        await using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM gari9267.current_residence_view" ,conn);

        NpgsqlDataReader reader =  await cmd.ExecuteReaderAsync();
        IEnumerable<CurrentResidentDto> list = await MapAll(reader);
        
        return list;
    }
    
    protected async Task<IEnumerable<CurrentResidentDto>> MapAll(NpgsqlDataReader reader)
    {
        IEnumerable<CurrentResidentDto> list = new List<CurrentResidentDto>();
        while (await reader.ReadAsync())
        {
            CurrentResidentDto item = Map(reader);
            list = list.Append(item);
        }

        return list;
    }

    public CurrentResidentDto Map(NpgsqlDataReader reader)
    {
        return new CurrentResidentDto
        {
            Id = reader.GetInt32("id"),
            Name = reader.GetString("name"),
            Surname = reader.GetString("surname"),
            
            StudentFacultyAbbreviation = reader.GetString("student_faculty_abbreviation"),
            
            DormitoryId = reader.GetInt32("dormitory_id"),
            DormitoryFacultyAbbreviation = reader.GetString("dormitory_faculty_abbreviation"),
            DormitoryAddress = reader.GetString("dormitory_address"),
            
            RoomId = reader.GetInt32("room_id"),
            RoomNumber = reader.GetString("room_number")
            
        };
    }

    public async Task<IEnumerable<Room>?> GetRoomsOfStudentDormitory(int studentId)
    {
        await using NpgsqlConnection conn = await _dataSource.OpenConnectionAsync();
        
        // getting student dormitory id
        await using NpgsqlCommand cmd1 =
            new NpgsqlCommand("SELECT dormitory_id FROM gari9267.current_residence_view WHERE id = (@p1)", conn);
        cmd1.Parameters.Add(new("p1", studentId));

        await using NpgsqlDataReader residenceReader = await cmd1.ExecuteReaderAsync();
        await residenceReader.ReadAsync();
        int dormitoryId = residenceReader.GetInt32("dormitory_id");

        // getting student dormitory rooms
        await using NpgsqlCommand cmd2 =
            new NpgsqlCommand("SELECT * FROM gari9267.rooms WHERE dormitory_id = (@p1)", conn);
        cmd2.Parameters.Add(new("p1", dormitoryId));

        await using NpgsqlDataReader roomReader = await cmd2.ExecuteReaderAsync();

        return await _roomRepository.MapAll(roomReader);
    }

    public async Task EvictResident(int studentId)
    {
        await using NpgsqlConnection conn = await _dataSource.OpenConnectionAsync();
        
        await using NpgsqlCommand cmd =
                new NpgsqlCommand("UPDATE gari9267.residence SET resided_until = (@p1) " +
                                  "WHERE student_id = (@p2) AND resided_until is NULL", conn);
            cmd.Parameters.Add(new("p1", DateTime.Now));
            cmd.Parameters.Add(new("p2", studentId));

            await cmd.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<CurrentResidentDto>> FindRoomResidents(int roomId)
    {
            await using var conn = await _dataSource.OpenConnectionAsync();
    
            await using var cmd = new NpgsqlCommand("SELECT * FROM gari9267.current_residence_view WHERE room_id = (@p1)", conn);
            cmd.Parameters.Add(new ("p1", roomId));
            
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            return await MapAll(reader);
    }
}