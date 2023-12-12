using dormitory_system.Models;

namespace dormitory_system.Repositories.Interfaces;

public interface IStudentRepository
{
    public Task Add(Student item);
    public Task Delete(Student item);
}