using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain_Layer.Models
{
    public class DevicesModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string DeviceId { get; set; }
        public string Prefix { get; set; }
        public string LastPrcnNumber { get; set; }
        public string LastGrnNumber { get; set; }
        public string LastGinNumber { get; set; }
        public string LastIdtNumber { get; set; }
        public string Location { get; set; }
        public string SerialNumber { get; set; }
        public string Username { get; set; }
        public int DeviceStatusId { get; set; }
        public int DeviceSupervisorId { get; set; }
        public float SyncPeriod { get; set; }
        public int DeviceBearerId { get; set; }
        public DateTime LastSync { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SateliteId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public UsersModel DeviceBearerData { get; set; }
        public UsersModel DeviceSuperevisorData { get; set; }
        public netPurLocationModel netPurLocationModel { get; set; }
        public DeviceStatusModel DeviceStatusData { get; set; }
    }
}
