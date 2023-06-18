namespace EDA_Inventory.Data;

public class Products
{
    public int Id { get; set; }
    
    public Guid ProductId { get; set; }
    
    public string Name { get; set; }
    
    public int Quantity { get; set; }
}