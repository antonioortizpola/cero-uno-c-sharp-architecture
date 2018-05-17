using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MoneroTickets.ViewModels
{
    public class ValidateClientAcountRequest
    {
        [Required]
        public string Account { get; set; }
        [Required]
        public string OperatorCode { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
