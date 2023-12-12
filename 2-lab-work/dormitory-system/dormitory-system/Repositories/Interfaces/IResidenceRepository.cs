using dormitory_system.Models;

namespace dormitory_system.Repositories.Interfaces;

public interface IResidenceRepository
{
    public Task Add(Residence item);
    public Task Delete(Residence item);
}