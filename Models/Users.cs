using System;
using System.Collections.Generic;

namespace MjIot.Storage.Models.EFCoreDb.Models
{
    public partial class Users
    {
        public Users()
        {
            Devices = new HashSet<Devices>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public ICollection<Devices> Devices { get; set; }
    }
}
