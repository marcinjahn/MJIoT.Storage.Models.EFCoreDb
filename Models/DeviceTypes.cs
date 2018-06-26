using System;
using System.Collections.Generic;

namespace MjIot.Storage.Models.EFCoreDb.Models
{
    public partial class DeviceTypes
    {
        public DeviceTypes()
        {
            Devices = new HashSet<Devices>();
            InverseBaseDeviceType = new HashSet<DeviceTypes>();
            PropertyTypes = new HashSet<PropertyTypes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? BaseDeviceTypeId { get; set; }
        public bool IsAbstract { get; set; }
        public bool OfflineMessagesEnabled { get; set; }

        public DeviceTypes BaseDeviceType { get; set; }
        public ICollection<Devices> Devices { get; set; }
        public ICollection<DeviceTypes> InverseBaseDeviceType { get; set; }
        public ICollection<PropertyTypes> PropertyTypes { get; set; }
    }
}
