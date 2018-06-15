using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MSM.Core.GameData;

namespace MSM.Web.Models.Admin
{
    public class FindUser
    {
        [Required]
        public string Username { get; set; }

        public MojangUserData FoundUser { get; set; }
    }
}
