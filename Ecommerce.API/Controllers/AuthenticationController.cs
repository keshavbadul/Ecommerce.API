using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Ecommerce.API.DbContexts;
using Ecommerce.API.Entities;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
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
		private readonly IAuthenticationRepository _authRepository;
		private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration configuration,
            IAuthenticationRepository authRepository,
            IMapper mapper)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            _authRepository = authRepository ??
                throw new ArgumentNullException(nameof(authRepository));
            _mapper = mapper ??
				throw new ArgumentNullException(nameof(mapper));
        }

        public class AuthenticationRequestBody
		{
			public string UserName { get; set; } = string.Empty;
			public string Password { get; set; } = string.Empty;
		}

		[HttpPost("authenticate")]
		public ActionResult<string> Authenticate(
			AuthenticationRequestBody authenticationRequestBody)
		{
			var user = _authRepository.ValidateUserCredentials(
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

		[HttpPost("register")]
		public async Task<ActionResult> Register(UserForCreationDto user)
		{
			var userEntity = _mapper.Map<Entities.User>(user);
			_authRepository.CreateUser(userEntity);
			await _authRepository.SaveChangesAsync();

			return Ok();
		}
	}
}

