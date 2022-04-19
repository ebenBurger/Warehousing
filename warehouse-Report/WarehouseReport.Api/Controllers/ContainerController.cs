using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseReport.Api.Interface;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    
    public class ContainerController : Controller
    {
        private readonly IContainerManager _containerManager;
        
        public ContainerController(IContainerManager containerManager)
        {
            _containerManager = containerManager;
        }

        [HttpGet("GetAllContainer")]
        public IActionResult GetAllContainer()
        {
            try
            {
                var data = _containerManager.GetAllContainers();

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
        
        [HttpPost("CreateContainer")]
        public IActionResult SaveContainer(ContainerModel containerModel)
        {
            try
            {
                var data = _containerManager.SaveContainer(containerModel);
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

        [HttpPost("UpdateContainer")]
        public IActionResult UpdateContainer(ContainerModel containerModel)
        {
            try
            {
                var data = _containerManager.UpdateContainer(containerModel);
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