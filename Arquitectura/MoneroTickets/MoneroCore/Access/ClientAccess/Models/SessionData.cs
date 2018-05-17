namespace MoneroCore.Access.ClientAccess.ViewModels
{
    public class SessionData
    {
        public string Email { get; }
        public string Token { get; }

        public SessionData(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}
