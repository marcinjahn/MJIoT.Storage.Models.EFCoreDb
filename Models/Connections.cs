using System;
using System.Collections.Generic;

namespace MJIoT_EFCoreModel.Models
{
    public partial class Connections
    {
        public int Id { get; set; }
        public int Condition { get; set; }
        public int? ListenerDeviceId { get; set; }
        public int? ListenerPropertyId { get; set; }
        public int? SenderDeviceId { get; set; }
        public int? SenderPropertyId { get; set; }
        public string ConditionValue { get; set; }

        public Devices ListenerDevice { get; set; }
        public PropertyTypes ListenerProperty { get; set; }
        public Devices SenderDevice { get; set; }
        public PropertyTypes SenderProperty { get; set; }
    }
}
