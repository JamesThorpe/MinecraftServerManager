using System.ComponentModel.DataAnnotations;

namespace MSM.Web.Models.Authentication {
    public class Credentials
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}