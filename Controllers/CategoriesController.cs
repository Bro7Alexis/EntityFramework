using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// Définition de la route pour ce contrôleur. Toutes les actions de ce contrôleur peuvent être atteintes via 'api/Categories'.
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    // Injection de dépendance de notre DbContext.
    private readonly NorthwindContext _context;

    public CategoriesController(NorthwindContext context)
    {
        _context = context;
    }

    // GET: api/Categories
    // Cette action renvoie tous les enregistrements de la table Categories.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    // GET: api/Categories/5
    // Cette action renvoie un enregistrement spécifique de la table Categories en fonction de l'ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    // PUT: api/Categories/5
    // Cette action met à jour un enregistrement spécifique de la table Categories.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, Category category)
    {
        if (id != category.CategoryID)
        {
            return BadRequest();
        }

        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Categories
    // Cette action crée un nouvel enregistrement dans la table Categories.
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryID }, category);
    }

    // DELETE: api/Categories/5
    // Cette action supprime un enregistrement spécifique de la table Categories.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
