// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

// <using_directives> 
using Microsoft.Azure.Cosmos;
// </using_directives>

// <client_credentials> 
// New instance of CosmosClient class
using CosmosClient client = new(
    accountEndpoint: Environment.GetEnvironmentVariable("COSMOS_ENDPOINT")!,
    authKeyOrResourceToken: Environment.GetEnvironmentVariable("COSMOS_KEY")!
);
// </client_credentials>

// <new_database> 
// Database reference with creation if it does not already exist
Database database = await client.CreateDatabaseIfNotExistsAsync(
    id: "adventureworks"
);

Console.WriteLine($"New database:\t{database.Id}");
// </new_database>

// <new_container> 
// Container reference with creation if it does not alredy exist
Container container = await database.CreateContainerIfNotExistsAsync(
    id: "products",
    partitionKeyPath: "/category",
    throughput: 400
);

Console.WriteLine($"New container:\t{container.Id}");
// </new_container>

// <new_item> 
// Create new object and upsert (create or replace) to container
Product newItem = new(
    id: "68719518391",
    category: "gear-surf-surfboards",
    name: "Yamba Surfboard",
    quantity: 12,
    sale: false
);

Product createdItem = await container.CreateItemAsync<Product>(
    item: newItem,
    partitionKey: new PartitionKey("gear-surf-surfboards")
);

Console.WriteLine($"Created item:\t{createdItem.id}\t[{createdItem.category}]");
// </new_item>

// <read_item> 
// Point read item from container using the id and partitionKey
Product readItem = await container.ReadItemAsync<Product>(
    id: "68719518391",
    partitionKey: new PartitionKey("gear-surf-surfboards")
);
// </read_item>

// <query_items> 
// Create query using a SQL string and parameters
var query = new QueryDefinition(
    query: "SELECT * FROM products p WHERE p.category = @key"
)
    .WithParameter("@key", "gear-surf-surfboards");

using FeedIterator<Product> feed = container.GetItemQueryIterator<Product>(
    queryDefinition: query
);

while (feed.HasMoreResults)
{
    FeedResponse<Product> response = await feed.ReadNextAsync();
    foreach (Product item in response)
    {
        Console.WriteLine($"Found item:\t{item.name}");
    }
}
// </query_items>
