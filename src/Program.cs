using IdentityModel.Client;
using Nexar.Client.Login;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nexar.Token
{
    public class Program
    {
        const string Usage = @"
Usage:
    nexar-token
        - get credentials token

    nexar-token <clientId> <clientSecret> [<scope>]
        - get application token
";

        public static async Task<int> Main(string[] args)
        {
            // The identity server endpoint, usually https://identity.nexar.com/
            var authority = Config.Authority;

            string token;
            if (args.Length == 0)
            {
                // Credentials token, using interactive browser login.
                // Call the client login helper to start the browser.
                var result = await LoginHelper.LoginAsync(Config.Authority);
                token = result.AccessToken;
            }
            else if (args.Length == 2 || args.Length == 3)
            {
                // Application token, using client ID, secret, scope.
                // Call the identity server with these credentials.
                var clientId = args[0];
                var clientSecret = args[1];
                var scope = args.Length > 2 ? args[2] : null;
                var result = await LoginWithClientCredentialsAsync(authority, clientId, clientSecret, scope);
                token = result.AccessToken;
            }
            else
            {
                Console.Error.WriteLine(Usage);
                return 1;
            }

            if (token == null)
            {
                Console.Error.WriteLine("Cannot get token.");
                return 1;
            }
            else
            {
                Console.Write(token);
                return 0;
            }
        }

        /// <summary>
        /// Gets the token responce using client ID, secret, scope.
        /// </summary>
        /// <param name="authority">The identity server endpoint, usually https://identity.nexar.com/ </param>
        /// <param name="clientId">The client ID.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="scope">The application scope.</param>
        /// <returns>The token response with <see cref="TokenResponse.AccessToken"/>.</returns>
        static async Task<TokenResponse> LoginWithClientCredentialsAsync(string authority, string clientId, string clientSecret, string scope)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(authority);
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scope
            });
            return tokenResponse;
        }
    }
}
