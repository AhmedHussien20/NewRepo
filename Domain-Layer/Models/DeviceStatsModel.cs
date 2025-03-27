using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class DeviceStatsModel
    {
        public int DeviceCount { get; set; }
        public int ActiveDevices { get; set; }
        public int DeactiveDevices { get; set; }
        public int DevicesOnHold { get; set; }  
    }
}
