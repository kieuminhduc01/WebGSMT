using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebGSMT.Models;

namespace WebGSMT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        GiamSatMoiTruongDbContext _context = new GiamSatMoiTruongDbContext();
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public IActionResult Login(string username, string pass)
        {
            Account login = new Account();
            login.UserName = username;
            login.Password = pass;
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);

                response = Ok(new { token = tokenStr });
            }
            return response;
        }

        private object GenerateJSONWebToken(Account userinfor)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userinfor.UserName),
                new Claim(JwtRegisteredClaimNames.Email,userinfor.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,userinfor.UserName),
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        private Account AuthenticateUser(Account login)
        {

            return _context.Accounts.FirstOrDefault(x => x.UserName == login.UserName && x.Password == login.Password);
        }
        [Authorize]
        [HttpPost("Post")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            return "chao ban :" + userName;
        }
        [Authorize(Roles="Duc")]
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "value3" };
        }
    }
}