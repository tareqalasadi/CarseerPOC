using CarseerPOC.DomainClasses;
using CarseerPOC.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarseerPOC.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class CarModelsController : ControllerBase
    {
        #region Const
        private readonly ICarsDetailsRepo _ICarsDetailsRepo;

        public CarModelsController(ICarsDetailsRepo ICarsDetailsRepo)
        {
            _ICarsDetailsRepo = ICarsDetailsRepo;
        }
        #endregion

        #region Get Cars Models 

        /// <summary>
        /// API to retrive all models for a specific car by car name and model year
        /// </summary>
        /// <param name="Make"></param>
        /// <param name="ModelYear"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCarsModels([FromQuery] string Make, [FromQuery] long ModelYear)
        {
            if (!string.IsNullOrEmpty(Make) && ModelYear > 0)
            {
                // to get Car Id by Car Name
                long MakeId = _ICarsDetailsRepo.GetMakeId(Make);

                if (MakeId == 0)
                    return BadRequest("Car Make Not Found.");

                // To get all models of the selected car type
                var CarModels = await _ICarsDetailsRepo.GetCarModels(MakeId, ModelYear);

                if (CarModels == null || CarModels.Count == 0)
                    return NotFound("No Car Models Found For The Given Make And Year.");

                return Ok(new CarsModelsResponse
                {
                    Make = Make,
                    Year = ModelYear,
                    Models = CarModels.Results
                });
            }
            return BadRequest();
        }
        #endregion
    }
}
