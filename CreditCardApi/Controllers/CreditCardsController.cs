using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CreditCardApi.Data;
using CreditCardApi.Models;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/cards")]
public class CreditCardsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CreditCardsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetCards()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(_context.CreditCards.Where(c => c.UserId == userId));
    }

    [HttpPost]
    public async Task<IActionResult> AddCard(CreditCard card)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        card.UserId = userId;
        _context.CreditCards.Add(card);
        await _context.SaveChangesAsync();
        return Ok(card);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var card = _context.CreditCards.FirstOrDefault(c => c.Id == id && c.UserId == userId);
        if (card == null) return NotFound();

        _context.CreditCards.Remove(card);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
