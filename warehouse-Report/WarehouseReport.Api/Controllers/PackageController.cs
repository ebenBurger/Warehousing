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
    
    public class PackageController : Controller
    {
        private readonly IPackageManager _packageManager;

        public PackageController(IPackageManager packageManager)
        {
            _packageManager = packageManager;
        }

        [HttpGet("GetAllPackages")]
        public IActionResult GetAll()
        {
            try
            {
                var data = _packageManager.GetAllPackages();

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

        [HttpPost("CreatePackage")]
        public IActionResult CreatePackage(PackageModel packageModel)
        {
            try
            {
                var data = _packageManager.SavePackage(packageModel);

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

        [HttpPost("UpdatePackage")]
        public IActionResult Update(PackageModel packageModel)
        {
            try
            {
                var data = _packageManager.UpdatePackage(packageModel);

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