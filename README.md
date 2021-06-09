# TrustworthyACW

ASP.NET Core Razor Pages web app providing a catering bill system for the university, coupled with PayPal for processing payment. Assessed coursework for the Trustworthy Computing module at the University of Hull.

## Setup

In order to build the application, you must ensure the .NET Core 5.0 SDK is installed. This can be downloaded here, https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.301-windows-x64-installer.

Alternatively, you may use the pre-built binaries. The .NET Core 5.0 runtime can be downloaded here, https://dotnet.microsoft.com/download/dotnet/5.0/runtime.

In order to setup and use the database, you must ensure SQL Server LocalDB is installed. Then navigate to the CateringSystemWeb directory and enter the following into the Windows terminal.

```bat
dotnet tool install --global dotnet-ef 
dotnet tool update --global dotnet-ef
dotnet ef database update
```

You may then use the provided SQL queries to add seed data to the database; alternatively, you may populate the database yourself.

## Notes

Postman scripts are provided to demonstrate PayPal API endpoints that are exposed by the web app.
