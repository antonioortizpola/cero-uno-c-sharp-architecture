namespace MoneroCore.Client
{
    public interface IClientService
	{
		int? ValidateClientSignIn(string email, string password);

		int CreateClient(string email, string password);
	}
}
