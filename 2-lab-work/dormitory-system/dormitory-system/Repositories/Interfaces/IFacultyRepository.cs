using dormitory_system.Models;

namespace dormitory_system.Repositories.Interfaces;

public interface IFacultyRepository
{
    public Task Add(Faculty item);
    public Task Delete(Faculty item);
}