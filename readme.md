# Azure Cosmos DB for NoSQL client library samples for .NET

[![Validate .NET code samples](https://github.com/Azure-Samples/cosmos-db-nosql-dotnet-samples/actions/workflows/validate.yml/badge.svg)](https://github.com/Azure-Samples/cosmos-db-nosql-dotnet-samples/actions/workflows/validate.yml)

## Getting started

This repo has a [devcontainer](https://containers.dev) environment making it easy to get started.

[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/Azure-Samples/cosmos-db-nosql-dotnet-samples?quickstart=1)

### Run the app

Configure your Azure Cosmos DB credentials as environment variables.

```bash
export COSMOS_ENDPOINT="<cosmos-account-URI>"
export COSMOS_KEY="<cosmos-account-PRIMARY-KEY>"
```

> **💡 TIP**: If you don't have an Azure Cosmos DB account, [create a free account](https://cosmos.azure.com/try/).

Run the quickstart sample app using the [`Microsoft.Azure.Cosmos`](https://www.nuget.org/packages/Microsoft.Azure.Cosmos) package from NuGet.

```bash
cd 001-quickstart/
dotnet add package Microsoft.Azure.Cosmos
dotnet run
```

### Validate any changes you make

If you change the code, run the formatter and test the build.

```bash
dotnet format

dotnet build
```

### Troubleshooting certificate errors

If you use a self-signed certificate, you may need to disable the client's validation:

```javascript
CosmosClientOptions options = new ()
{
    HttpClientFactory = () => new HttpClient(new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    }),
    ConnectionMode = ConnectionMode.Gateway
};
    >
using CosmosClient client = new(
  ...,
  ...,
  clientOptions: options
);
```
