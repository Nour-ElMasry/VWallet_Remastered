using Application.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.CommandHandlers;

public class CreateUserHandler : IRequestHandler<CreateUser, object>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public CreateUserHandler(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    public async Task<object> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var uniqueCheck = await _userManager.FindByNameAsync(request.Username) == null;

        if (uniqueCheck)
        {
            var address = new Address(request.Country, request.City, request.Street);
            var user = new User(request.Name, request.DateOfBirth, address)
            {
                UserName = request.Username
            };

            var create = await _userManager.CreateAsync(user, request.Password);

            if (create.Succeeded)
            {
                var dbUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName, cancellationToken: cancellationToken);

                var role = "Customer";
                var roleCheck = await _roleManager.FindByNameAsync(role);

                if (roleCheck == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role
                    });
                }


                var addRoleToUser = await _userManager.AddToRoleAsync(user, role);

                if (!addRoleToUser.Succeeded)
                {
                    return null;
                }

                var authClaims = new List<Claim>
                    {
                        new Claim("Name", dbUser.UserName),
                        new Claim(ClaimTypes.Role, role)
                    };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

                var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: authClaims,
                        expires: DateTime.Now.AddDays(3),
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new
                {
                    customer = new
                    {
                        dbUser.Id,
                        dbUser.UserName,
                        dbUser.Name,
                    },
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };

            }
        }

        return null;
    }
}
