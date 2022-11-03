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
Database database1 = await client.CreateDatabaseAsync(
    id: "adventureworks-1"
);
// </create_database>

// <create_database_check>
// New instance of Database class referencing the server-side database
Database database2 = await client.CreateDatabaseIfNotExistsAsync(
    id: "adventureworks-2"
);
// </create_database_check>

// <create_database_response>
// New instance of Database response class referencing the server-side database
DatabaseResponse response = await client.CreateDatabaseIfNotExistsAsync(
    id: "adventureworks-3"
);
// Parse additional response properties
Database database3 = response.Database;
// </create_database_response>