// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

// <using_directives> 
using System.Net;
using Microsoft.Azure.Cosmos;
// </using_directives>

// <endpoint_key> 
// New instance of CosmosClient class using an endpoint and key string
using CosmosClient client = new(
    accountEndpoint: Environment.GetEnvironmentVariable("COSMOS_ENDPOINT")!,
    authKeyOrResourceToken: Environment.GetEnvironmentVariable("COSMOS_KEY")!
);
// </endpoint_key>

// <create_database>
// New instance of Database class referencing the server-side database
Database database = await client.CreateDatabaseIfNotExistsAsync(
    id: "adventureworks"
);
// </create_database>

// <create_container>
// New instance of Container class referencing the server-side container
Container container = await database.CreateContainerIfNotExistsAsync(
    id: "products",
    partitionKeyPath: "/category",
    throughput: 400
);
// </create_container>

// <create_objects> 
// Create new item and add to container
Product firstItem = new(
    id: "68719518388",
    category: "gear-surf-surfboards",
    name: "Sunnox Surfboard",
    quantity: 8,
    sale: true
);

Product secondItem = new(
    id: "68719518381",
    category: "gear-surf-surfboards",
    name: "Kalbar Surfboard",
    quantity: 4,
    sale: false
);
// </create_objects>

// <create_items>
await container.CreateItemAsync<Product>(
    item: firstItem,
    partitionKey: new PartitionKey("gear-surf-surfboards")
);

await container.CreateItemAsync<Product>(
    item: secondItem,
    partitionKey: new PartitionKey("gear-surf-surfboards")
);
// </create_items> 

// <read_item>
// Read existing item from container
Product readItem = await container.ReadItemAsync<Product>(
    id: "68719518388",
    partitionKey: new PartitionKey("gear-surf-surfboards")
);
// </read_item> 

// <read_item_expanded>
// Read existing item from container
ItemResponse<Product> readResponse = await container.ReadItemAsync<Product>(
    id: "68719518388",
    partitionKey: new PartitionKey("gear-surf-surfboards")
);

// Get response metadata
double requestUnits = readResponse.RequestCharge;
HttpStatusCode statusCode = readResponse.StatusCode;

// Explicitly get item
Product readItemExplicit = readResponse.Resource;
// </read_item_expanded>


// <read_item_stream>
// Read existing item from container
using ResponseMessage readItemStreamResponse = await container.ReadItemStreamAsync(
    id: "68719518388",
    partitionKey: new PartitionKey("gear-surf-surfboards")
);

// Get stream from response
using StreamReader readItemStreamReader = new(readItemStreamResponse.Content);

// (optional) Get stream content
string content = await readItemStreamReader.ReadToEndAsync();
// </read_item_stream>

// <read_multiple_items>
// Create partition key object
PartitionKey partitionKey = new("gear-surf-surfboards");

// Create list of tuples for each item
List<(string, PartitionKey)> itemsToFind = new()
{
    ("68719518388", partitionKey),
    ("68719518381", partitionKey)
};

// Read multiple items
FeedResponse<Product> feedResponse = await container.ReadManyItemsAsync<Product>(
    items: itemsToFind
);

foreach (Product item in feedResponse)
{
    Console.WriteLine($"Found item:\t{item.name}");
}
// </read_multiple_items>