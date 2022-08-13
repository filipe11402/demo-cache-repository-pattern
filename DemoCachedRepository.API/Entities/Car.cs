namespace DemoCachedRepository.API.Entities
{
    public class Car
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
}
