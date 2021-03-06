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
        public string ContainerNumber { get; set; }
        public string ContainerType { get; set; }
        public string SealOne { get; set; }
        public string SealTwo { get; set; }
        public DateTime? PackingDate { get; set; }
        public string PackingLocation { get; set; }
        public string FileReference { get; set; }
        public string BillOfLading { get; set; }
        public string Vesel { get; set; }
        public string Voyage { get; set; }
        public string Status { get; set; }
        public string LoadingPort { get; set; }
        public string DestinationPort { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public bool IsActive { get; set; }

        public ICollection<CargoModel> Cargo { get; set; }
        public ICollection<PackageModel> Package { get; set; }
    }
}