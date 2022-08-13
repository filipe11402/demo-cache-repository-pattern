using DemoCachedRepository.API.Entities;

namespace DemoCachedRepository.API.Repositories
{
    public interface ICarRepository
    {
        Task<(string dataSource, List<Car>)> ListAsync();

        Task<(string dataSource, Car)> GetByIdAsync(int id);
    }
}
