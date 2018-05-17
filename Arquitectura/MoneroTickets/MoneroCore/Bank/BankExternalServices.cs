using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MoneroCore.Bank
{
    public class BankExternalServices
    {
        private readonly BankExternalServiceCredentials _bankExternalServiceCredentials;
        private readonly ILogger<BankExternalServices> _logger;

        public BankExternalServices(IConfiguration configuration, ILogger<BankExternalServices> logger)
        {
	        _bankExternalServiceCredentials = configuration.GetSection("BankExternalServiceCredentials").Get<BankExternalServiceCredentials>();
            _logger = logger;
        }

        public bool ValidateClientEmail(string operationCode, string email)
        {
            _logger.LogTrace("Calling banks service fake with username '{0}' and password '{1}' to endpoint {2}",
                _bankExternalServiceCredentials.Username, _bankExternalServiceCredentials.Password, _bankExternalServiceCredentials.Endpoint);
            //TODO: Replace with real service call when the bank confirms our access credentials
            return operationCode.Equals("12345678") && email.Equals("test@test.com");
        }

        public bool ValidateClientAccount(string operationCode, string account)
        {
            _logger.LogTrace("Calling banks service fake with username '{0}' and password '{1}' to endpoint {2}",
                _bankExternalServiceCredentials.Username, _bankExternalServiceCredentials.Password, _bankExternalServiceCredentials.Endpoint);
            //TODO: Replace with real service call when the bank confirms our access credentials
            return operationCode.Equals("12345678") && account.Equals("12345678");
        }
    }
}