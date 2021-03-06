using System;
using System.Collections.Generic;

namespace MjIot.Storage.Models.EFCoreDb.Models
{
    public partial class DeviceProperties
    {
        public int Id { get; set; }
        public int? DeviceId { get; set; }
        public int? PropertyTypeId { get; set; }
        public string PropertyValue { get; set; }

        public Devices Device { get; set; }
        public PropertyTypes PropertyType { get; set; }
    }
}
