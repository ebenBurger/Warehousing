using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseReport.Api.Interface;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CargoController : Controller
    {
        private readonly ICargoManager _cargoManager;

        public CargoController(ICargoManager cargoManager)
        {
            _cargoManager = cargoManager;
        }

        [HttpGet("GetAllCargo")]
        public IActionResult GetAllCargo()
        {
            try
            {
                var data = _cargoManager.GetAllCargo();
                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddCargo")]
        public IActionResult SaveCargo(CargoModel cargoModel)
        {
            try
            {
                var data = _cargoManager.SaveCargo(cargoModel);
                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateCargo")]
        public IActionResult UpdateCargo(CargoModel cargoModel)
        {
            try
            {
                var data = _cargoManager.UpdateCargo(cargoModel);
                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

    }
}