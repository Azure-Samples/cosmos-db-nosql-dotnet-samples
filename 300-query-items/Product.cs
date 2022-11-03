// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

// <type>
// C# record type for items in the container
public record Product(
    string id,
    string category,
    string name,
    int quantity,
    bool sale
);
// </type>