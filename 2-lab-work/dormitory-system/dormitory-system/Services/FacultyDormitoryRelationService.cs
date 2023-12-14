using dormitory_system.Models;
using dormitory_system.Repositories;
using dormitory_system.Repositories.Interfaces;
using Npgsql;

namespace dormitory_system.Services;

public class FacultyDormitoryRelationService
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly IDormitoryRepository _dormitoryRepository;
    private readonly IFacultyDormitoryRelationRepository _facultyDormitoryRelationRepository;
    
    public FacultyDormitoryRelationService(NpgsqlDataSource dataSource,
        IDormitoryRepository dormitoryRepository,
        IFacultyDormitoryRelationRepository facultyDormitoryRelationRepository)
    {
        _dataSource = dataSource;
        
        _dormitoryRepository = new DormitoryRepository(dataSource);
        _facultyDormitoryRelationRepository = new FacultyDormitoryRelationRepository(dataSource);
    }

    public async Task AddDormitory(Dormitory dormitory, int facultyId)
    {
        await using NpgsqlConnection conn = await _dataSource.OpenConnectionAsync();
        NpgsqlTransaction transaction = await conn.BeginTransactionAsync();

        try
        {
            int dormitoryId = await _dormitoryRepository.Add(dormitory);

            FacultyDormitoryRelation facultyDormitoryRelation = new FacultyDormitoryRelation
            {
                FacultyId = facultyId,
                DormitoryId = dormitoryId
            };
            await _facultyDormitoryRelationRepository.Add(facultyDormitoryRelation);

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }
}