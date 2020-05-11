using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projet.API.Model;

namespace Projet.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
   
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        /// Fluet API

        protected override void OnModelCreating(ModelBuilder builder)
        {
                base.OnModelCreating(builder);
                builder.Entity<EtudiantClasse>().HasKey(
                    k => new {k.ClasseId, k.UserId}
                ); 

               
        }

        public DbSet<Classe> classe {get; set;}
        public DbSet<Publication> publication { get; set; } 
       public DbSet<EtudiantClasse> etudiantClasse { get; set; }
    }
}