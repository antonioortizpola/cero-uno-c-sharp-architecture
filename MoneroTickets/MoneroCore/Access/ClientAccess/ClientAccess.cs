using MoneroCore.Access.ClientAccess.Exceptions;
using MoneroCore.Access.ClientAccess.ViewModels;
using MoneroCore.Access.Token;
using MoneroCore.Bank;
using MoneroCore.Client;

namespace MoneroCore.Access.ClientAccess
{
    public class ClientAccess : IClientAccess
    {
        private readonly IClientService _clientService;
        private readonly ITokenService _tokenService;
        private readonly IBankService _bankService;

        public ClientAccess(
            IClientService clientService,
            ITokenService tokenService,
            IBankService bankService
        )
        {
            _clientService = clientService;
            _tokenService = tokenService;
            _bankService = bankService;
        }


        public SessionData SignIn(string email, string operatorCode, string password)
        {
            var clientId = _clientService.ValidateClientSignIn(email, password);
            if(!clientId.HasValue)
                throw new InvalidSignInData();

            if(!_bankService.ValidateClientEmail(email, operatorCode))
                throw new InvalidSignInData();

            var token = _tokenService.SignInUser(clientId.Value);
            return new SessionData(email, token);
        }

        public SessionData CreateClientAccess(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public void Exit(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
