using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.Enums;
using OfficeControlSystemApi.Models.Identity;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Interaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OfficeControlSystemApi.Controllers
{
    public class UserController : Controller
    {
        private readonly ICreateUserCommand _createUserCommand;

        public UserController(
            ICreateUserCommand userCommand)
        {
            _createUserCommand = userCommand;
        }

        [HttpPost("/user")]
        public async Task<IActionResult> CreateUser(
            [FromBody] UserCreationModel user,
            UserRole role,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _createUserCommand.ExecuteAsync(user, role, cancellationToken);
            return Ok("Adminstrator created successfully.");
        }
    }
}
