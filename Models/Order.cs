public class Order
{
    public int OrderID { get; set; }
    public int CustomerID { get; set; }
    public int EmployeeID { get; set; }
    public DateTime OrderDate { get; set; }
    public int ShipperID { get; set; }

    public Customer? Customer { get; set; }
    public Employee? Employee { get; set; }
    public Shipper? Shipper { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}