using System;
using System.ComponentModel.DataAnnotations;

namespace Projet.API.Model
{
    public class Publication
    {
        public int PublicationId { get; set; }

        [Required]
        public string Contenu { get; set; }
        public DateTime DatePublication { get; set; }
        [Required]
        public int ClasseId { get; set; }

       /*  public Publication()
        {
            DatePublication = DateTime.Now;
        } */

    }
}