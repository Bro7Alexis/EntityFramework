using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Définition de la route pour ce contrôleur. Toutes les actions de ce contrôleur peuvent être atteintes via 'api/OrderDetails'.
[Route("api/[controller]")]
[ApiController]
public class OrderDetailsController : ControllerBase
{
    // Injection de dépendance de notre DbContext.
    private readonly NorthwindContext _context;

    public OrderDetailsController(NorthwindContext context)
    {
        _context = context;
    }

    // GET: api/OrderDetails
    // Cette action renvoie tous les enregistrements de la table OrderDetails.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
    {
        return await _context.OrderDetails.ToListAsync();
    }

    // GET: api/OrderDetails/5
    // Cette action renvoie un enregistrement spécifique de la table OrderDetails en fonction de l'ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
    {
        var orderDetail = await _context.OrderDetails.FindAsync(id);

        if (orderDetail == null)
        {
            return NotFound();
        }

        return orderDetail;
    }

    // PUT: api/OrderDetails/5
    // Cette action met à jour un enregistrement spécifique de la table OrderDetails.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetail orderDetail)
    {
        if (id != orderDetail.OrderDetailID)
        {
            return BadRequest();
        }

        _context.Entry(orderDetail).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/OrderDetails
    // Cette action crée un nouvel enregistrement dans la table OrderDetails.
    [HttpPost]
    public async Task<ActionResult<OrderDetail>> CreateOrderDetail(OrderDetail orderDetail)
    {
        _context.OrderDetails.Add(orderDetail);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.OrderDetailID }, orderDetail);
    }

    // DELETE: api/OrderDetails/5
    // Cette action supprime un enregistrement spécifique de la table OrderDetails.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        var orderDetail = await _context.OrderDetails.FindAsync(id);
        if (orderDetail == null)
        {
            return NotFound();
        }

        _context.OrderDetails.Remove(orderDetail);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
