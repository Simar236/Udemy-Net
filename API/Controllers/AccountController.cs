using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Threading.Tasks;
using API.Context;
using API.Entity;
using Microsoft.AspNetCore.Mvc;
using API.DTO;
using Microsoft.EntityFrameworkCore;
using API.Interface;

namespace API.Controllers
{
    public class AccountController:BaseController
    {
        public AccountController(DataContext Context, ITokenService tokenService)
        {
            this.Context = Context;
            TokenService = tokenService;
        }

        public DataContext Context { get; }
        public ITokenService TokenService { get; }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> register(RegisterDTO _rDTO)
        {
            if(!await UserExist(_rDTO.UserName))
            {
                using var hmac = new HMACSHA512();
                var user= new AppUser()
                {
                    UserName= _rDTO.UserName.ToLower(),
                    PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(_rDTO.Password)),
                    PasswordSalt=hmac.Key
                };
                Context.Users.Add(user);
                await Context.SaveChangesAsync();
                return new UserDto(){
                    UserName=user.UserName,
                    Token=TokenService.CreateToken(user)
                };
            }
            else
            {
                return BadRequest("UserName exist");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> login(loginDTO _lDTO)
        {
            var user=await Context.Users.SingleOrDefaultAsync(x=>x.UserName==_lDTO.UserName.ToLower());
            if(user== null)
            {
                return Unauthorized("User does not exist");
            }
            else
            {
                using var hmac=new HMACSHA512(user.PasswordSalt);
                var ComputeHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(_lDTO.Password));
                for(int i=0;i<ComputeHash.Length;i++)
                {
                    if(ComputeHash[i]!=user.PasswordHash[i])
                    {
                        return Unauthorized("Wrong Password");
                    }
                }
            }
            return new LoginResponseDTO{
                UserName=_lDTO.UserName,
                Token=TokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExist(string UserName)
        {
            return await Context.Users.AnyAsync(x=>x.UserName==UserName.ToLower());
        }

        [HttpGet("GetValidToken")]
        public async Task<ActionResult<string>> GetValidToken(string UserName)
        {
            if(await UserExist(UserName))
            {
                var user=await Context.Users.SingleOrDefaultAsync(x=>x.UserName==UserName.ToLower());
                return TokenService.CreateToken(user);
            }
            else
            {
                return BadRequest("User does not exist");
            }
        }
    }
}