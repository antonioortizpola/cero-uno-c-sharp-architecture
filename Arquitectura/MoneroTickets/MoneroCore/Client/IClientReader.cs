namespace MoneroCore.Client
{
    public interface IClientReader
    {
        string GetPasswordHash(string email);
    }
}
