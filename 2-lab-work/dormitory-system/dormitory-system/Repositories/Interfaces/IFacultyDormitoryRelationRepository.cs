using dormitory_system.Models;

namespace dormitory_system.Repositories.Interfaces;

public interface IFacultyDormitoryRelationRepository
{
    public Task Add(FacultyDormitoryRelation item);
    public Task Delete(FacultyDormitoryRelation item);
}