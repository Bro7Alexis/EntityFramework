using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Définition de la route pour ce contrôleur. Toutes les actions de ce contrôleur peuvent être atteintes via 'api/Products'.
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    // Injection de dépendance de notre DbContext.
    private readonly NorthwindContext _context;

    public ProductsController(NorthwindContext context)
    {
        _context = context;
    }

    // GET: api/Products
    // Cette action renvoie tous les enregistrements de la table Products.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    // GET: api/Products/5
    // Cette action renvoie un enregistrement spécifique de la table Products en fonction de l'ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // PUT: api/Products/5
    // Cette action met à jour un enregistrement spécifique de la table Products.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.ProductID)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Products
    // Cette action crée un nouvel enregistrement dans la table Products.
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
    }

    // DELETE: api/Products/5
    // Cette action supprime un enregistrement spécifique de la table Products.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
