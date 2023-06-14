# nexar-token

Getting Nexar tokens, the tool and sample.

[UPDATE] Please note for those users who want to get going with the supply API, we recommend following the examples [here](https://github.com/NexarDeveloper/nexar-first-supply-query) which support multiple languages and include token caching.

## Usage

Change to the project source directory

    cd src

and invoke as

    dotnet run <clientId> <clientSecret> [<scope1> ...]

where scopes include:

- `user.access`
- `design.domain`
- `supply.domain`

### Supply token

To get a supply token, invoke

    dotnet run <clientId> <clientSecret> supply.domain

This token does not require signing with Altium Live credentials.
It is suitable for the Supply operations, i.e. queries with the prefix `sup`.

### Design token

To get a design token, invoke, for example

    dotnet run <clientId> <clientSecret> user.access design.domain supply.domain

> Note that exact scopes depend on your Nexar application.

## Login / Logout

The default system browser opens the Nexar identity login page.
To login, enter your credentials and click `Sign In`.

The browser does not necessarily asks your credentials each time, it may remember and use your last login.
If this is not desired, open <https://identity.nexar.com> and either logout or clean the site cookies.
Then login again.

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
