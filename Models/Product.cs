public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int SupplierID { get; set; }
    public int CategoryID { get; set; }
    public string Unit { get; set; }
    public decimal Price { get; set; }

    public Category Category { get; set; }
    public Supplier Supplier { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}