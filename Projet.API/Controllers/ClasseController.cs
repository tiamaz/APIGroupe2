using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projet.API.Data;
using Projet.API.Model;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClasseController: ControllerBase
    {
        /// Repository pattern
        private readonly IGestionRepository _repo;

        public ClasseController(IGestionRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AjouterClasse(Classe classe) // Il faut créer DTO
        {
            _repo.Ajouter(classe);

            if(await _repo.Sauvegarder()) return Ok("A ete enregistrer");
            return BadRequest("Un problème est survenu");
        }
    }
}