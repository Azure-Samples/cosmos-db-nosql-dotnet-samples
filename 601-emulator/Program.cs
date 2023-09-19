// <imports>
using Microsoft.Azure.Cosmos;
// </imports>

// <client>
using CosmosClient client = new(
    accountEndpoint: "https://localhost:8081/",
    authKeyOrResourceToken: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
);
// </client>

// <resources>
Database database = await client.CreateDatabaseIfNotExistsAsync(
    id: "cosmicworks",
    throughput: 400
);

Container container = await database.CreateContainerIfNotExistsAsync(
    id: "products",
    partitionKeyPath: "/id"
);
// </resources>

// <upsert>
var item = new
{
    id = "68719518371",
    name = "Kiama classic surfboard"
};

await container.UpsertItemAsync(item);
// </upsert>
