namespace MoneroCore.Bank
{
    public interface IBankService
    {
        bool ValidateClientEmail(string operationCode, string email);
        bool ValidateClientAccount(string operationCode, string email);
    }
}
