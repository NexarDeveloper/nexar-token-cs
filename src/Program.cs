using Nexar.Client.Login;
using Nexar.Client.Token;
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

    nexar-token <clientId> <clientSecret>
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
                var result = await LoginHelper.LoginAsync(authority);
                token = result.AccessToken;
            }
            else if (args.Length == 2)
            {
                // Application token, using client ID, secret, scope.
                // Call the identity server with these credentials.
                var clientId = args[0];
                var clientSecret = args[1];
                using var client = new HttpClient();
                token = await client.GetNexarTokenAsync(clientId, clientSecret, authority);
            }
            else
            {
                Console.Error.WriteLine(Usage);
                return 1;
            }

            if (token == null)
            {
                Console.Error.WriteLine("Cannot get Nexar token.");
                return 1;
            }
            else
            {
                Console.Write(token);
                return 0;
            }
        }
    }
}
