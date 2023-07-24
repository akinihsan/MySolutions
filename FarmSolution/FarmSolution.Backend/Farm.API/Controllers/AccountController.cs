using AutoMapper;
using Farm.API.Model;
using Farm.API.Model.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Farm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("[action]")]
        public async Task<ApiResult<LoginResponseModel>> Login(string userName, string password)
        {
            if (userName != "user1@mail.com" || password != "password")
                return new ApiResult<LoginResponseModel>(false, null, new LoginResponseModel("no-token"));

            string token = CreateBearerToken(userName, 123);
            var result = new ApiResult<LoginResponseModel>(true, null, new LoginResponseModel(token));
 
            return result;

        }
        private string CreateBearerToken(string userName, int sid)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Sid, sid.ToString())
            };


            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(issuer: _configuration["JWT:ValidIssuer"], audience: _configuration["JWT:ValidAudience"], claims: userClaims, expires: DateTime.Now.AddDays(1), signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

    }
}
