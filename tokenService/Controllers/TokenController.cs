using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace tokenService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        static readonly HttpClient Client = new HttpClient();
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] user content)
        {
            if (ModelState.IsValid)
            {

                var userId = GetUserIdFromCredentials(content).Result;

                switch (userId)
                {
                    case 200:
                        var claims = new[]
                        {
                 new Claim(JwtRegisteredClaimNames.Sub, content.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                 };

                        var token = new JwtSecurityToken
                        (
                            issuer: _configuration["Issuer"],
                            audience: _configuration["Audience"],
                            expires: DateTime.UtcNow.AddDays(60),
                            claims: claims,
                            notBefore: DateTime.UtcNow,
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),
                                    SecurityAlgorithms.HmacSha256)
                        );

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                    case 401:
                        return StatusCode(401, "Invalid Password");
                    case 404:
                        return StatusCode(404, "user not found");
                    default:
                        return Ok(userId);
                }



            }

            return BadRequest();
        }
        private async Task<int> GetUserIdFromCredentials(user account)
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await Client.GetAsync("https://localhost:44350/accounts/find/"+ account.UserName);
            var responseStr = await response.Content.ReadAsStringAsync();
            var newresponse = responseStr.Substring(1, responseStr.Length-2);
            Console.WriteLine(response);
            Console.WriteLine(account.Password);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (newresponse == account.Password)
                {
                    return 200;
                }
                else
                {
                    return 401;
                }
            }
            else if (responseStr==null)
            {
                return 404;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return 400;
            }
            else
            {
                return 0;
            }
        }


    }
}
