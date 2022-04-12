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
    
    public class ClientController : Controller
    {
        private readonly IClientManager _clientManager;

        public ClientController(IClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        [HttpGet("GetAllClients")]
        public IActionResult GetAllClients()
        {
            try
            {
                var data = _clientManager.GetAllClients();
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

        [HttpPost("AddClient")]
        public IActionResult AddClient(ClientModel clientModel)
        {
            try
            {
                var data = _clientManager.SaveClient(clientModel);
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