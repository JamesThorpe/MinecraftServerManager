using System;
using System.Collections.Generic;
using System.Text;

namespace MSM.Core
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsServerAdmin { get; set; }
        public int AllowedServers { get; set; }
    }
}
