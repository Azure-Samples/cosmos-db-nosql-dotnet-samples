// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

// <using_directives> 
using Microsoft.Azure.Cosmos;
// </using_directives>

// <connection_string> 
// New instance of CosmosClient class using a connection string
using CosmosClient client = new(
    connectionString: Environment.GetEnvironmentVariable("COSMOS_CONNECTION_STRING")!
);
// </connection_string>