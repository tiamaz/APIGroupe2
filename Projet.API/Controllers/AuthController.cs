using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Projet.API.DTO;
using Projet.API.Model;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
     private readonly UserManager<User> _userManager;
     private readonly IMapper _mapper;
     private readonly RoleManager<Role> _roleManager;
     private readonly SignInManager<User> _signInManager;

     private readonly IConfiguration _config;

        public AuthController(UserManager<User> userManager, 
        IMapper mapper, 
        RoleManager<Role> roleManager, 
        SignInManager<User> signInManager,
        IConfiguration config
        )
        {
         _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            var roles = new List<Role>{
                new Role{Name = "Prof"},
                new Role{Name = "Etudiant"}
            };
            foreach(var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }
            //dotnet add package AutoMapper.Extensions.Microsoft.dependencyInjection
            var user = _mapper.Map<User>(userForRegister);
            var result = await _userManager.CreateAsync(user, userForRegister.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, roles[0].Name);
                return Ok("Enregistrement réussi");
            }
            return BadRequest(result.Errors);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);
    
            var Result = await _signInManager.CheckPasswordSignInAsync(
                user, userForLoginDto.Password, false);

                var role = await _userManager.GetRolesAsync(user);

            if(Result.Succeeded)
            {
                /// Créer le JWT
                return Ok(
                    new {
                        token = CreateJwtToken(user, role)
                    });
            }

            return BadRequest("Username ou le mot de passe sont erronés");
        } 

        
        private async Task<string> CreateJwtToken(User user, IList<string> roles)
        {

            //// Création des informations de PlayLoad/Data
               var claims = new List<Claim>{
                   new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                   new Claim(ClaimTypes.Name, user.UserName),
               };

               foreach(var role in roles)
               {
                   claims.Add(new Claim(ClaimTypes.Role, role));
               } 

            /// Récuperer le code
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)
            );
                ////Crypter le code
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds,
                Expires = DateTime.Now.AddDays(1)
            };


            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
            
        } 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Hello(){
            return Ok("Token Valid");
        }

    }


         
} 

