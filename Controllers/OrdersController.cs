using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Définition de la route pour ce contrôleur. Toutes les actions de ce contrôleur peuvent être atteintes via 'api/Orders'.
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    // Injection de dépendance de notre DbContext.
    private readonly NorthwindContext _context;

    public OrdersController(NorthwindContext context)
    {
        _context = context;
    }

    // GET: api/Orders
    // Cette action renvoie tous les enregistrements de la table Orders.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    // GET: api/Orders/5
    // Cette action renvoie un enregistrement spécifique de la table Orders en fonction de l'ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    // GET: api/Orders/5/OrderDetails
    // Cette action renvoie les détails d'une commande spécifique de la table OrderDetails en fonction de l'ID de la commande.
    [HttpGet("{id}/OrderDetails")]
    public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails(int id)
    {
        var orderDetails = await _context.OrderDetails
            .Where(od => od.OrderID == id)
            .ToListAsync();

        if (orderDetails == null)
        {
            return NotFound();
        }

        return orderDetails;
    }

    // PUT: api/Orders/5
    // Cette action met à jour un enregistrement spécifique de la table Orders.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, Order order)
    {
        if (id != order.OrderID)
        {
            return BadRequest();
        }

        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Orders
    // Cette action crée un nouvel enregistrement dans la table Orders.
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order);
    }

    // DELETE: api/Orders/5
    // Cette action supprime un enregistrement spécifique de la table Orders.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
