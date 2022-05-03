using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseReport.Api.Interface;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    
    public class TransporterController : Controller
    {
        private readonly ITransporterManager _transporterManager;

        public TransporterController(ITransporterManager transporterManager)
        {
            _transporterManager = transporterManager;
        }

        [HttpGet("GetAllTransporter")]
        public IActionResult GetAll()
        {
            try
            {
                var data = _transporterManager.GetAllTransporter();

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

        [HttpPost("CreateTransporter")]
        public IActionResult Create(TransporterModel transporterModel)
        {
            try
            {
                var data = _transporterManager.SaveTransporter(transporterModel);

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

        [HttpPost("UpdateTransporter")]
        public IActionResult Update(TransporterModel transporterModel)
        {
            try
            {
                var data = _transporterManager.UpdateTransporter(transporterModel);

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