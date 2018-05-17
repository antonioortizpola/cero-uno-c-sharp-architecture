using System;
using System.Collections.Generic;
using System.Text;

namespace MoneroCore.Access.Token
{
    public interface ITokenService
    {
        string SignInUser(int userId);
    }
}
