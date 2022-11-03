// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

// <entity>
// C# record representing an item in the container
public record Product(
    string id,
    string category,
    string name,
    int quantity,
    bool sale
);
// </entity>