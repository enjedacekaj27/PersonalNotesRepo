using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonalNotes.Validation;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonalNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly IRepositoryManager _repositoryManager;
        public UserController(IRepositoryManager repositoryManager, IConfiguration configuration) {
            _repositoryManager = repositoryManager;
            _configuration = configuration;

        }

        //krijimi i nje user
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserAddDTO adduserDTO)
        {
            var checkemail = _repositoryManager.UserRepository.FindByCondition(u=>u.Email==adduserDTO.Email, false);

            if (checkemail.Any())
            {
                return BadRequest("Email is existent");
            }

            var validEmail = AccountValidation.isValidEmail(adduserDTO.Email);

            if (!validEmail)
            {
                return BadRequest("Email is not valid");
            }

        
            var checkPassword = AccountValidation.isValidPassword(adduserDTO.Password);

            //if (!checkPassword)
            //{
            //    return BadRequest("Sorry password is not valid");
            //}

            AccountValidation.CreatePasswordHash(adduserDTO.Password,
                out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                FirstName = adduserDTO.FirstName,
                LastName = adduserDTO.LastName,
                Email = adduserDTO.Email,    
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now

            };
            _repositoryManager.UserRepository.Create(user);
            _repositoryManager.Save();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = _repositoryManager.UserRepository.FindByCondition(u => u.Email == loginDTO.Email, false).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("User not found");
            }

          
            if (!AccountValidation.VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }


            if (AccountValidation.VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt) == true  && user.Email==loginDTO.Email)
            {
                List<Claim> claims = new List<Claim>
            {
                new Claim("ID", user.ID.ToString()),
               
                new Claim(ClaimTypes.NameIdentifier, user.Email),


            };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AppSettings:Token"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7124",
                    audience: "https://localhost:7124",
                      claims: claims,
                    expires: DateTime.Now.AddMinutes(30),                  
                    signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(tokenString);
            }


            return Unauthorized();
        }
       


    }



}
