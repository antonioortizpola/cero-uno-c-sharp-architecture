using MoneroCore.Access.ClientAccess.ViewModels;

namespace MoneroCore.Access.ClientAccess
{
    interface IClientAccess
    {
        SessionData SignIn(string email, string operatorCode, string password);

        SessionData CreateClientAccess(string email, string password);

        void Exit(string token);
    }

    
}
