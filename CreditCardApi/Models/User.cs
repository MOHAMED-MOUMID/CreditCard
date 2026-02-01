using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditCardApi.Models
{
    public class User
    {
        public int Id { get; set; }

        // Obligatoire : Email ne peut pas être null
        [Required]
        public string Email { get; set; } = null!;

        // Obligatoire : PasswordHash ne peut pas être null
        [Required]
        public string PasswordHash { get; set; } = null!;

        // Toujours initialisé pour éviter null
        public List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
    }
}
