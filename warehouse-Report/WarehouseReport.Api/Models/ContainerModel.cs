using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WarehouseReport.Api.Models
{
    public class ContainerModel
    {
        [Key]
        public int ContainerId { get; set; }
        public string ContainerName { get; set; }
        
        public bool GP20ft { get; set; }
        public bool FRIG20ft { get; set; }
        public bool FROOG20ft { get; set; }
        public bool TOIG20ft { get; set; }
        public bool OTOOG20ft { get; set; }
        public bool GP40ft { get; set; }
        public bool HQ40ft { get; set; }
        public bool FRIG40ft { get; set; }
        public bool FROOG40ft { get; set; }
        public bool OTIG40ft { get; set; }
        public bool OTOOG40ft { get; set; }

        public string SealOne { get; set; }
        public string SealTwo { get; set; }
        public DateTime PackingDate { get; set; }
        public string PackingLocation { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public bool IsActive { get; set; }

        public ICollection<CargoModel> Cargo { get; set; }
    }
}