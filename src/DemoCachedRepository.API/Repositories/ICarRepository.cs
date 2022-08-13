using DemoCachedRepository.API.Entities;

namespace DemoCachedRepository.API.Repositories
{
    public interface ICarRepository
    {
        /// <summary>
        /// Lists all cars from the datasource
        /// </summary>
        /// <returns>
        /// </returns>
        Task<(string dataSource, List<Car>)> ListAsync();

        /// <summary>
        /// Returns a car based on it's id
        /// </summary>
        /// <param name="id">The identifier of the car</param>
        /// <returns></returns>
        Task<(string dataSource, Car)> GetByIdAsync(int id);
    }
}
