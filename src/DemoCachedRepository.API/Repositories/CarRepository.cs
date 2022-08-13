using DemoCachedRepository.API.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace DemoCachedRepository.API.Repositories
{
    public class CarRepository : ICarRepository
    {
        //This could be your ApplicationDbContext
        //Showcase purposes
        private List<Car> _carDatabase;

        private IMemoryCache _cache;

        public CarRepository(IMemoryCache cache)
        {
            _carDatabase = new List<Car>
            {
                new Car(1, "Ferrari", "458", DateTime.Now.AddYears(5)),
                new Car(2, "Porsche", "911 GT3RS", DateTime.Now.AddYears(2)),
                new Car(3, "Bugatti", "Chiron", DateTime.Now.AddYears(1))
            };

            _cache = cache;
        }

        public Task<(string dataSource, Car)> GetByIdAsync(int id)
        {
            var cacheKey = $"Cars:{id}";

            Car cachedCar = _cache.Get<Car>(cacheKey);

            if (cachedCar is null) 
            {
                Car dbCar = _carDatabase.FirstOrDefault(token => token.Id == id);

                if (dbCar is null)
                {
                    return Task.FromResult<(string, Car)>(default);
                }

                _cache.Set(cacheKey, dbCar);

                return Task.FromResult(("Database", dbCar));
            }

            return Task.FromResult(("Cache", cachedCar));
        }

        public Task<(string dataSource, List<Car>)> ListAsync()
        {
            var cacheKey = "Cars";

            var cachedCars = _cache.Get<List<Car>>(cacheKey);

            if(cachedCars is null)
            {
                _cache.Set(cacheKey, _carDatabase);

                return Task.FromResult(("Database", _carDatabase));
            }

            return Task.FromResult(("Cache", cachedCars));
        }
    }
}
