using Nexar.Client.Login;
using Nexar.Client.Token;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nexar.Token
{
    public class Program
    {
        const string Usage = @"
Usage:
    nexar-token <clientId> <clientSecret> [<scope1> ...]
";

        public static async Task<int> Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine(Usage);
                return 1;
            }

            var authority = Environment.GetEnvironmentVariable("NEXAR_AUTHORITY_URL") ?? "https://identity.nexar.com";

            var clientId = args[0];
            var clientSecret = args[1];
            var scopes = args.Skip(2).ToArray();

            string token;
            if (scopes.Length == 1 && scopes[0] == "supply.domain")
            {
                // Supply token without credentials.
                using var client = new HttpClient();
                token = await client.GetNexarTokenAsync(clientId, clientSecret, authority);
            }
            else
            {
                // Credentials token, using interactive browser login.
                // Call the client login helper to start the browser.
                var result = await LoginHelper.LoginAsync(clientId, clientSecret, scopes, authority);
                token = result.AccessToken;
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
