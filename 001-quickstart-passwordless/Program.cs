// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

// <using_directives> 
using Microsoft.Azure.Cosmos;
using Azure.Identity;
// </using_directives>

// <client_credentials> 
// New instance of CosmosClient class
using CosmosClient client = new(
    "https://<your-cosmosdb-account-name>.documents.azure.com:443/",
    new DefaultAzureCredential()
);
// </client_credentials>

// <new_database> 
// Database reference with creation if it does not already exist
Database database = client.GetDatabase("<your-database-name>");

Console.WriteLine($"New database:\t{database.Id}");
// </new_database>

// <new_container> 
// Container reference with creation if it does not alredy exist
Container container = database.GetContainer("<your-container-name>");

Console.WriteLine($"New container:\t{container.Id}");
// </new_container>

// <new_item> 
// Create new object and upsert (create or replace) to container
Product newItem = new(
    id: "70b63682-b93a-4c77-aad2-65501347265f",
    categoryId: "61dba35b-4f02-45c5-b648-c6badc0cbd79",
    categoryName: "gear-surf-surfboards",
    name: "Yamba Surfboard",
    quantity: 12,
    sale: false
);

Product createdItem = await container.CreateItemAsync<Product>(
    item: newItem,
    partitionKey: new PartitionKey("61dba35b-4f02-45c5-b648-c6badc0cbd79")
);

Console.WriteLine($"Created item:\t{createdItem.id}\t[{createdItem.categoryName}]");
// </new_item>

// <read_item> 
// Point read item from container using the id and partitionKey
Product readItem = await container.ReadItemAsync<Product>(
    id: "70b63682-b93a-4c77-aad2-65501347265f",
    partitionKey: new PartitionKey("61dba35b-4f02-45c5-b648-c6badc0cbd79")
);
// </read_item>

// <query_items> 
// Create query using a SQL string and parameters
var query = new QueryDefinition(
    query: "SELECT * FROM products p WHERE p.categoryId = @categoryId"
)
    .WithParameter("@categoryId", "61dba35b-4f02-45c5-b648-c6badc0cbd79");

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
