using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Définition de la route pour ce contrôleur. Toutes les actions de ce contrôleur peuvent être atteintes via 'api/Shippers'.
[Route("api/[controller]")]
[ApiController]
public class ShippersController : ControllerBase
{
    // Injection de dépendance de notre DbContext.
    private readonly NorthwindContext _context;

    public ShippersController(NorthwindContext context)
    {
        _context = context;
    }

    // GET: api/Shippers
    // Cette action renvoie tous les enregistrements de la table Shippers.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shipper>>> GetShippers()
    {
        return await _context.Shippers.ToListAsync();
    }

    // GET: api/Shippers/5
    // Cette action renvoie un enregistrement spécifique de la table Shippers en fonction de l'ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Shipper>> GetShipper(int id)
    {
        var shipper = await _context.Shippers.FindAsync(id);

        if (shipper == null)
        {
            return NotFound();
        }

        return shipper;
    }

    // PUT: api/Shippers/5
    // Cette action met à jour un enregistrement spécifique de la table Shippers.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShipper(int id, Shipper shipper)
    {
        if (id != shipper.ShipperID)
        {
            return BadRequest();
        }

        _context.Entry(shipper).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Shippers
    // Cette action crée un nouvel enregistrement dans la table Shippers.
    [HttpPost]
    public async Task<ActionResult<Shipper>> CreateShipper(Shipper shipper)
    {
        _context.Shippers.Add(shipper);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetShipper), new { id = shipper.ShipperID }, shipper);
    }

    // DELETE: api/Shippers/5
    // Cette action supprime un enregistrement spécifique de la table Shippers.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipper(int id)
    {
        var shipper = await _context.Shippers.FindAsync(id);
        if (shipper == null)
        {
            return NotFound();
        }

        _context.Shippers.Remove(shipper);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
