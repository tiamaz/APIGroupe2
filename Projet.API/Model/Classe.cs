using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projet.API.Model
{
    public class Classe
    {
        public int ClasseId { get; set; }
        [Required]
        public string Filiere { get; set; }
        [Required]
        public string Niveau { get; set; }
        [Required]
        public int NombreEtudiant { get; set; }
        [Required]
        public int UserId { get; set; }
        public ICollection<Publication> Publication { get; set; }
    }
}