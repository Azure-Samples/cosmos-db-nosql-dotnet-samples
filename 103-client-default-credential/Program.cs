// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

// <using_identity_directives> 
using Azure.Core;
using Azure.Identity;
// </using_identity_directives>
// <using_directives> 
using Microsoft.Azure.Cosmos;
// </using_directives>

// <credential>
// Credential class for testing on a local machine or Azure services
TokenCredential credential = new DefaultAzureCredential();
// </credential>

// <default_credential> 
// New instance of CosmosClient class using a connection string
using CosmosClient client = new(
    accountEndpoint: Environment.GetEnvironmentVariable("COSMOS_ENDPOINT")!,
    tokenCredential: credential
);
// </default_credential>