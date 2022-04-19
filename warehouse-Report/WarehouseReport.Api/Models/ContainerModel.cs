using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WarehouseReport.Api.Models
{
    public class ContainerModel
    {
        [Key]
        public int ContainerId { get; set; }
        public string ContainerName { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public bool IsActive { get; set; }

        public ICollection<CargoModel> CargoModels { get; set; }
    }
}