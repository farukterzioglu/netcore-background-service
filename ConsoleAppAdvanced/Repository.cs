using System;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
    public interface IRepository { }

    public class Repository : IRepository
    {
        private readonly AuthenticationSettings _authenticationSettings;

        // public Repository(IOptions<AuthenticationSettings> authenticationSettings)
        // {
        //     _authenticationSettings = (authenticationSettings ?? throw new ArgumentNullException(nameof(authenticationSettings))).Value;
        // }

        public Repository(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings ?? throw new ArgumentNullException(nameof(authenticationSettings));
        }
    }
}