using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Projet.API.Model
{
    public class User: IdentityUser<int>
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Ville { get; set; }
        [Required]
        public bool Status { get; set; }

        public ICollection<Classe> classe { get; set; }



    }
}