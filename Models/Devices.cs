using System;
using System.Collections.Generic;

namespace MjIot.Storage.Models.EFCoreDb.Models
{
    public partial class Devices
    {
        public Devices()
        {
            ConnectionsListenerDevice = new HashSet<Connections>();
            ConnectionsSenderDevice = new HashSet<Connections>();
            DeviceProperties = new HashSet<DeviceProperties>();
        }

        public int Id { get; set; }
        public int? DeviceTypeId { get; set; }
        public string IoThubKey { get; set; }
        public int? UserId { get; set; }

        public DeviceTypes DeviceType { get; set; }
        public Users User { get; set; }
        public ICollection<Connections> ConnectionsListenerDevice { get; set; }
        public ICollection<Connections> ConnectionsSenderDevice { get; set; }
        public ICollection<DeviceProperties> DeviceProperties { get; set; }
    }
}
