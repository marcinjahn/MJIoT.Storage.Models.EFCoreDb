using System;
using System.Collections.Generic;

namespace MJIoT_EFCoreModel.Models
{
    public partial class PropertyTypes
    {
        public PropertyTypes()
        {
            ConnectionsListenerProperty = new HashSet<Connections>();
            ConnectionsSenderProperty = new HashSet<Connections>();
            DeviceProperties = new HashSet<DeviceProperties>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? DeviceTypeId { get; set; }
        public int Format { get; set; }
        public bool Uiconfigurable { get; set; }
        public bool IsListenerProperty { get; set; }
        public bool IsSenderProperty { get; set; }

        public DeviceTypes DeviceType { get; set; }
        public ICollection<Connections> ConnectionsListenerProperty { get; set; }
        public ICollection<Connections> ConnectionsSenderProperty { get; set; }
        public ICollection<DeviceProperties> DeviceProperties { get; set; }
    }
}
