# nexar-token

Getting Nexar tokens, the tool and sample.

## Usage

Change to the project source directory

    cd src

and invoke with arguments for an application token or without arguments for a credentials token

    dotnet run <clientId> <clientSecret> [<scope>]
    dotnet run

### Application token

To get an application token, use your Nexar application client ID and secret

    dotnet run <clientId> <clientSecret> [<scope>]

The scope argument is optional, e.g. "supply.domain".

### Credentials token

To get your credentials token, invoke

    dotnet run

On the first run it starts the browser with the Nexar identity login page:

![](images/login.png)

Enter your credentials and click `Sing In`.

The browser redirects to the page with this message:

> You can now return to the application.

and the Nexar token is printed to the console.

On next runs you may be redirected to this page right away.

You may clean the cookies in order to ensure the login page. E.g. in Chrome,
click the `(i)` icon in the address bar, then `Cookies` and remove cookies:

![](images/cookies.png)

## Notes

### nexar-token executable

Instead of `dotnet run` which builds and runs the tool you may use the already built `nexar-token` directly

    <path>/nexar-token [arguments]

### Output to clipboard

In PowerShell

    dotnet run | Set-Clipboard
    nexar-token | Set-Clipboard

In cmd

    dotnet run | clip
    nexar-token | clip
