using dormitory_system.DataTransferObjects;
using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using System.Data;
using Npgsql;

namespace dormitory_system.Services;

public class FacultyDormitoryRelationService
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly IFacultyRepository _facultyRepository;
    private readonly IDormitoryRepository _dormitoryRepository;
    private readonly IFacultyDormitoryRelationRepository _facultyDormitoryRelationRepository;
    
    public FacultyDormitoryRelationService(NpgsqlDataSource dataSource,
        IFacultyRepository facultyRepository,
        IDormitoryRepository dormitoryRepository,
        IFacultyDormitoryRelationRepository facultyDormitoryRelationRepository)
    {
        _dataSource = dataSource;

        _facultyRepository = facultyRepository;
        _dormitoryRepository = dormitoryRepository;
        _facultyDormitoryRelationRepository = facultyDormitoryRelationRepository;
    }
    
    public async Task<IEnumerable<FacultyDormitoryRelationDto>> FacultyDormitoryList()
    {
        await using NpgsqlConnection conn = await _dataSource.OpenConnectionAsync();
        await using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM gari9267.faculty_dormitory_relations_view", conn);
        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        return await MapAll(reader);
    }
    
    public async Task<IEnumerable<FacultyDormitoryRelationDto>> MapAll(NpgsqlDataReader reader)
    {
        IEnumerable<FacultyDormitoryRelationDto> list = new List<FacultyDormitoryRelationDto>();
        
        while (await reader.ReadAsync())
        {
            FacultyDormitoryRelationDto entry = await Map(reader);
            list = list.Append(entry);
        }

        return list;
    }
    
    public async Task<FacultyDormitoryRelationDto> Map(NpgsqlDataReader reader)
    {
        Faculty faculty = new Faculty
        {
            Id = reader.GetInt32("faculty_id"),
            Abbreviation = reader.GetString("abbreviation"),
            Name = reader.GetString("faculty_name")
        };;

        Dormitory? dormitory = await reader.IsDBNullAsync("dormitory_id")
            ? null
            : new Dormitory
            {
                Id = reader.GetInt32("dormitory_id"),
                Address = reader.GetString("dormitory_address"),
                ManagerName = reader.GetString("manager_name"),
                ManagerSurname = reader.GetString("manager_surname"),
                ManagerPhoneNumber = reader.GetString("manager_phone_number")
            };

        return new FacultyDormitoryRelationDto
        {
            Faculty = faculty,
            Dormitory = dormitory
        };
    }

    public async Task AddDormitory(Dormitory dormitory, int facultyId)
    {
        await using NpgsqlConnection conn = await _dataSource.OpenConnectionAsync();
        await using NpgsqlTransaction transaction = await conn.BeginTransactionAsync();

        try
        {
            int dormitoryId = await _dormitoryRepository.Add(dormitory, conn);

            FacultyDormitoryRelation facultyDormitoryRelation = new FacultyDormitoryRelation
            {
                FacultyId = facultyId,
                DormitoryId = dormitoryId
            };
            await _facultyDormitoryRelationRepository.Add(facultyDormitoryRelation, conn);

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }
}