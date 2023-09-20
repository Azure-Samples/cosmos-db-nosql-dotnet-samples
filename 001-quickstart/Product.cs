// <entity>
// C# record representing an item in the container
public record Product(
    string id,
    string categoryId,
    string categoryName,
    string name,
    int quantity,
    bool sale
);
// </entity>