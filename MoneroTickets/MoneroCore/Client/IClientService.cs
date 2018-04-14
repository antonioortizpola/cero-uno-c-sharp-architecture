namespace MoneroCore.Client
{
    public interface IClientService
    {
        int? ValidateClientSignIn(string email, string password);
    }
}
