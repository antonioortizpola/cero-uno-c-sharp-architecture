namespace MoneroCore.Bank
{
    public class BankService : IBankService
    {
        private readonly BankExternalServices _bankExternalServices;

        public BankService(BankExternalServices bankExternalServices)
        {
            _bankExternalServices = bankExternalServices;
        }

        public bool ValidateClientEmail(string operationCode, string email)
        {
            return _bankExternalServices.ValidateClientEmail(operationCode, email);
        }

        public bool ValidateClientAccount(string operationCode, string account)
        {
            return _bankExternalServices.ValidateClientAccount(operationCode, account);
        }
    }
}