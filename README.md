# GitHub Webhooks Challenge

## Summary

The project is a simple Web service that listens to a GitHub Organization `repository` events, and creates basic protection rules for the default (main) branch.
After the protection rules have been applied, a new *Issue* is created, mentioning the *User* that created the rules, as well as a brief summary of the protections applied.

## Running the project

The project is written in **C#**, using **ASP Core**, running on the **.NET 6** platform.

> Note: This project connects to the GitHub API using the .NET client, [Octokit.net](https://github.com/octokit/octokit.net), and the credentials are generated using the **OAuth flow**. This choice was made to have a better control over the user tokens, and to remove any potential information leak. In a real scenario, where the Web Service needs to be constantly connected to the GitHub API (self-refreshing tokens) without any user interaction, a `basic authentication` approach makes more sense, and the GitHub Client will be initialized using a `username` and `password`.

### Setup:
- install [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) from the official website
- install [Visual Studio](https://visualstudio.microsoft.com/) from the official website
- clone the Repository, and open the `GitHub.Webhooks.sln` solution in Visual Studio
- *because we use OAuth to authenticate against the GitHub API* - create a OAuth application in your GitHub account [following the Octokit.net docs](https://github.com/octokit/octokit.net/blob/main/docs/oauth-flow.md).
- replace the `ClientId` and `ClientSecret` values in the `GitHub.Webhooks.Web/appsettings.json` file

### Authorizing the GitHub Client: 
- `Build` and `Run` the project in Visual Studio
- a Swagger API page should be displayed, with 3 endpoints visible

![Swagger Dashboard](./docs/swagger.png)

- navigate to the `https://localhost:[port]/github/login` page in your browser, and a GitHub authorization page should show up. Grant the required permissions.
- after the Grant was given, the GitHub Client has been authorized. (*Note: the Authorization step is required every time you run the project*)
- your project is now ready to receive GitHub Webhooks!

### Receiving Repository events from GitHub:
- if you don't already have a GitHub Organization, create one ([follow official docs](https://docs.github.com/en/organizations/collaborating-with-groups-in-organizations/creating-a-new-organization-from-scratch))
- in your Organization Settings, go to the `Webhooks` page and create a new Webhook ([follow official docs](https://docs.github.com/en/developers/webhooks-and-events/webhooks/creating-webhooks))
- the new Webhook should only contain the `repository` events, the rest we don't need for the scope of this project
- in the Webhook URL, set the `https://[example.com]/github/webhooks/repository` (*Note: the Webhooks cannot be configured to send events to `localhost`, but I recommend using a tool like [NGROK](https://ngrok.com/) to route Internet requests to localhost for testing purposes*)

Creating a new Repository in the Organization, should send a Webhook event to the Web Service, which should then create protection rules for the default branch, as well as a new Issue with the summary of the protection applied.