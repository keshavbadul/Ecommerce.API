using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.API.DbContexts;
using Ecommerce.API.Entities;
using Ecommerce.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.API.Controllers
{
	[Route("api/authentication")]
	[ApiController]
	public class AuthenticationController : Controller
	{
        private readonly IConfiguration _configuration;
		private readonly EcommerceDbContext _context;

        public AuthenticationController(IConfiguration configuration,
			EcommerceDbContext context)
		{
			_configuration = configuration ??
				throw new ArgumentNullException(nameof(configuration));
			_context = context ??
				throw new ArgumentException(nameof(context));
		}

        public class AuthenticationRequestBody
		{
			public string? UserName { get; set; }
			public string? Password { get; set; }
		}

		[HttpPost("authenticate")]
		public ActionResult<string> Authenticate(
			AuthenticationRequestBody authenticationRequestBody)
		{
			var user = ValidateUserCredentials(
				authenticationRequestBody.UserName,
				authenticationRequestBody.Password);

			if (user == null)
			{
				return Unauthorized();
			}

			var securityKey = new SymmetricSecurityKey(
				Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
			var signingCredentials = new SigningCredentials(
				securityKey, SecurityAlgorithms.HmacSha256);

			var claimsForToken = new List<Claim>();
			
			claimsForToken.Add(new Claim("userId", user.Id.ToString()));
			claimsForToken.Add(new Claim("username", user.UserName));
			claimsForToken.Add(new Claim("role", user.Role));
			

			var jwtSecurityToken = new JwtSecurityToken(
				_configuration["Authentication:Issuer"],
				_configuration["Authentication:Audience"],
				claimsForToken,
				DateTime.UtcNow,
				DateTime.UtcNow.AddHours(1),
				signingCredentials);

			var tokenToReturn = new JwtSecurityTokenHandler()
				.WriteToken(jwtSecurityToken);

			return Ok(tokenToReturn);
		}

		private User ValidateUserCredentials(string? userName, string? password)
		{
			var res = _context.Users.FirstOrDefault(u => u.UserName == userName
				&& u.Password == password);

			return res;
		}

		[HttpPost("register")]
		public ActionResult Register(UserForCreationDto user)
		{
			return Ok();
		}
	}
}

