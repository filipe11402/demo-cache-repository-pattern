## CachedRepository Pattern implementation Demonstration

---

A Demonstration project, to showcase how an implementation(with a twist) of the CachedRepository pattern, initially proposed by [Steve Ardalis](https://github.com/ardalis).

---

### Theory
The Repository Pattern, is in essence, to act as a facade, in between our system, and a Data source(Database etc...). And sometimes, we need caching for when we are in the need of retrieving data as fast as possible to our view.
So why not join both both?

### Context
We have a system, that will fetch:
- A list of cars
- A car, based on it's identifier

But, we need somehow to reduce the time that we fetch the data from the datasource, specially when fetching information for a specific car

Car Entity
```cs 
class Car
{
    public int Id { get; init; }

    public string Brand { get; init; }

    public string Model { get; init; }

    public DateTime DateReleased { get; init; }

    public Car(int id, string brand, string model, DateTime dateReleased)
    {
        Id = id;
        Brand = brand;
        Model = model;
        DateReleased = dateReleased;
    }
}
```

Repository contract
```cs
interface ICarRepository
{
    Task<(string dataSource, Car)> ListAsync();
    Task<(string dataSource, Car)> GetByIdAsync(int id);
}
```

Repository Implementation
```cs
interface CarRepository : ICarRepository
{
    Task<(string dataSource, List<Car>)> ListAsync()
    {
        var cacheKey = "Cars";

        var cachedCars = _cache.Get<List<Car>>(cacheKey);

        //If no cars are in cache
        //cache the cars that we fetch from the data source
        //and return them
        if(cachedCars is null)
        {
            _cache.Set(cacheKey, _carDatabase);

            return Task.FromResult(("Database", _carDatabase));
        }

        //otherwise return the cached data
        return Task.FromResult(("Cache", cachedCars));
    }

    Task<(string dataSource, Car)> GetByIdAsync(int id)
    {
        var cacheKey = $"Cars:{id}";

        Car cachedCar = _cache.Get<Car>(cacheKey);

        //if the car is not on cache
        if (cachedCar is null) 
        {
            Car dbCar = _carDatabase.FirstOrDefault(token => token.Id == id);

            //search for the car on the datasource
            if (dbCar is null)
            {
                
                //otherwise return null;
                return Task.FromResult<(string, Car)>(default);
            }

            //if we find it, cache it and return it
            _cache.Set(cacheKey, dbCar);
            return Task.FromResult(("Database", dbCar));
        }

        //otherwise return the cached car
        return Task.FromResult(("Cache", cachedCar));
    }
}
```

## References

- Repository pattern

  - [Steve Ardalis CachedRepository Pattern](https://ardalis.com/introducing-the-cachedrepository-pattern/)
  -  [Bruno Brito](https://www.youtube.com/watch?v=vC444683wHs)

- Documentation
  - [Nelson Nobre](https://github.com/NelsonBN)
