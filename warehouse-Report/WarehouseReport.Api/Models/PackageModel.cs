using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WarehouseReport.Api.Models
{
    public class PackageModel
    {
        [Key]
        public int PackageId { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double KgCBMConversion { get; set; }
        public double VolumeCbm { get; set; }
        public double VolumeMetric { get; set; }
        public double ChargeableWeight { get; set; }
        public bool IsActive { get; set; }
        
        public DateTime DateCreated { get; set; }

        public int CargoId { get; set; }
        public CargoModel Cargo { get; set; }
    }
}