using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneroCore.Bank;
using MoneroTickets.Json;
using MoneroTickets.ViewModels;

namespace MoneroTickets.Controllers
{
    [Route("api/[controller]")]
    public class AccessController : Controller
    {
        private readonly ILogger<AccessController> _logger;
        private readonly IBankService _bankService;

        public AccessController(ILogger<AccessController> logger, IBankService bankService)
        {
            _logger = logger;
            _bankService = bankService;
        }

        //// GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost("validate-client-account")]
        public IActionResult ValidateClientAccount([FromBody]ValidateClientAcountRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Something wasn't valid on the model
                return BadRequest(new JsonErrors(ModelState));
            }
            _logger.LogTrace("Received request: {0}", request);

            if (!_bankService.ValidateClientAccount(request.OperatorCode, request.Account))
                return BadRequest("InvalidData");

            return NoContent();
        }

		[HttpPost("create-client-access")]
		public IActionResult CreateClientAccess([FromBody]CreateClientAccessRequest request)
		{
			if (!ModelState.IsValid)
			{
				// Something wasn't valid on the model
				return BadRequest(new JsonErrors(ModelState));
			}
			_logger.LogTrace("Received request: {0}", request);

			//if (!_bankService.ValidateClientAccount(request.OperatorCode, request.Account))
			//	return BadRequest("InvalidData");

			return NoContent();
		}
	}
}
