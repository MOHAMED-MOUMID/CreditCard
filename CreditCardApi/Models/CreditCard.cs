using System;
using System.ComponentModel.DataAnnotations;

namespace CreditCardApi.Models
{
    public class CreditCard
    {
        public int Id { get; set; }

        [Required]
        public string CardNumber { get; set; } = null!;

        [Required]
        public string CardHolderName { get; set; } = null!;

        [Required]
        public DateTime ExpirationDate { get; set; }

        // Chaque carte doit être liée à un utilisateur
        [Required]
        public int UserId { get; set; }

        // Nullable pour éviter la validation côté API
        public User? User { get; set; }
    }
}
