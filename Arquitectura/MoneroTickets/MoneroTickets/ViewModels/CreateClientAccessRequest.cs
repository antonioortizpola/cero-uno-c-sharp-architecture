using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MoneroTickets.ViewModels
{
    public class CreateClientAccessRequest
	{
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
