using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Définition de la route pour ce contrôleur. Toutes les actions de ce contrôleur peuvent être atteintes via 'api/Suppliers'.
[Route("api/[controller]")]
[ApiController]
public class SuppliersController : ControllerBase
{
    // Injection de dépendance de notre DbContext.
    private readonly NorthwindContext _context;

    public SuppliersController(NorthwindContext context)
    {
        _context = context;
    }

    // GET: api/Suppliers
    // Cette action renvoie tous les enregistrements de la table Suppliers.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
    {
        return await _context.Suppliers.ToListAsync();
    }

    // GET: api/Suppliers/5
    // Cette action renvoie un enregistrement spécifique de la table Suppliers en fonction de l'ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Supplier>> GetSupplier(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);

        if (supplier == null)
        {
            return NotFound();
        }

        return supplier;
    }

    // PUT: api/Suppliers/5
    // Cette action met à jour un enregistrement spécifique de la table Suppliers.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplier(int id, Supplier supplier)
    {
        if (id != supplier.SupplierID)
        {
            return BadRequest();
        }

        _context.Entry(supplier).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Suppliers
    // Cette action crée un nouvel enregistrement dans la table Suppliers.
    [HttpPost]
    public async Task<ActionResult<Supplier>> CreateSupplier(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSupplier), new { id = supplier.SupplierID }, supplier);
    }

    // DELETE: api/Suppliers/5
    // Cette action supprime un enregistrement spécifique de la table Suppliers.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return NotFound();
        }

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
