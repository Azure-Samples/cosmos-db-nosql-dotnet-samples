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