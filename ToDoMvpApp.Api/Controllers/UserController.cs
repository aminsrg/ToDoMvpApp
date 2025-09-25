using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoMvpApp.Application.Commands.User.Login;
using ToDoMvpApp.Application.Commands.User.SignUp;

namespace ToDoMvpApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator, IConfiguration config) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IConfiguration _config = config;

    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var userId = await _mediator.Send(command);
        if (userId is null) return Unauthorized();

        var token = GenerateJwtToken(userId);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(string userId)
    {
        var key = _config["Jwt:Key"];
        var issuer = _config["Jwt:Issuer"];

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim("UserId", userId)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
