using dormitory_system.Models;
using Npgsql;

namespace dormitory_system.Repositories.Interfaces;

public interface IFacultyRepository
{
    public Task<IEnumerable<Faculty>> GetAll();
    public Task Add(Faculty item);
    public Task Delete(Faculty item);
}