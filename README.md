# Test application ASP .NET Core

Application exposant une API Rest avec .NET 8 permettant de retourner les statistiques des joueurs de tennis.

L'application est réalisée dans le cadre d'un test technique ([voir les consignes](https://tenisu.latelier.co/backend)).

## Requirements

For building and running the application you need:

- [.NET SDK 8.0](https://dotnet.microsoft.com/fr-fr/download)

## Setup

Build :

```shell
dotnet build
```

Run :

```shell
dotnet run --project src/WebApplication1/WebApplication1.csproj
```

You can find the swagger spec at : http://localhost:5164/swagger/index.html (Development only)

## Test

Run xUnit tests  :

```shell
dotnet test -l "console;verbosity=normal"
```

## Deploy

The app is automatically deployed on Azure using GitHub Actions, after every push on Main branch.

Deployed app is hosted as Azure Web App.
