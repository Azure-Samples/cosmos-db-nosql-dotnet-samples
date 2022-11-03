// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

// <using_directives> 
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
Container container1 = await database.CreateContainerAsync(
    id: "products-1",
    partitionKeyPath: "/category",
    throughput: 400
);
// </create_container>

// <create_container_check>
// New instance of Container class referencing the server-side container
Container container2 = await database.CreateContainerIfNotExistsAsync(
    id: "products-2",
    partitionKeyPath: "/category",
    throughput: 400
);
// </create_container_check>

// <create_container_response>
// New instance of Container class referencing the server-side container
ContainerResponse response = await database.CreateContainerIfNotExistsAsync(
    id: "products-3",
    partitionKeyPath: "/category",
    throughput: 400
);
// Parse additional response properties
Container container3 = response.Container;
// </create_container_response>