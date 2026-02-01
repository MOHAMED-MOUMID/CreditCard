using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CreditCardApi.Data;
using CreditCardApi.Models;
using CreditCardApi.DTOs;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    // -----------------------------
    // REGISTER
    // -----------------------------
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // Vérifier si l'email existe déjà
        if (_context.Users.Any(u => u.Email == request.Email))
            return BadRequest("Cet email est déjà utilisé.");

        // Créer l'utilisateur
        var user = new User
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Générer le token JWT
        var token = GenerateToken(user);

        return Ok(new { token });
    }

    // -----------------------------
    // LOGIN
    // -----------------------------
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // Chercher l'utilisateur
        var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized("Email ou mot de passe incorrect.");

        // Générer le token JWT
        var token = GenerateToken(user);

        return Ok(new { token });
    }

    // -----------------------------
    // MÉTHODE PRIVÉE POUR LE TOKEN
    // -----------------------------
    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _config["Jwt:Issuer"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
