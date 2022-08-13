using DemoCachedRepository.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoCachedRepository.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListCars() 
        {
            var carList = await _carRepository.ListAsync();

            return Ok(
                new 
                {
                    DataSource = carList.dataSource,
                    Cars = carList.Item2
                });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var fetchCar = await _carRepository.GetByIdAsync(id);


            if (fetchCar.Item2 is null) { return NotFound(); }

            return Ok(
                new 
                {
                    DataSource = fetchCar.dataSource,
                    Car = fetchCar.Item2
                });
        }
    }
}
