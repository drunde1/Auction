using Auction.API.Contracts;
using Auction.Application.Services;
using Auction.Core.Models;
using Auction.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<ActionResult<List<UsersResponse>>> GetUsers()
        {
            var users = await _usersService.GetAllUsers();

            var response = users.Select(u => new UsersResponse(u.Id, u.UserName, u.Email, u.HashedPassword));

            return Ok(response);
        }

        [Authorize]
        [Route("getByEmail")]
        [HttpGet]
        public async Task<ActionResult<UsersResponse>> GetUserByEmail(string email)
        {
            var user = await _usersService.GetUserByEmail(email);

            return Ok(new UsersResponse(user.Id, user.UserName, user.Email, user.HashedPassword));
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<Guid>> RegisterUser([FromBody] UsersRequest request)
        {
            try
            {
                var userId = await _usersService.Register(
                request.UserName,
                request.Email,
                request.Password);

                return Ok(userId);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserRequest request)
        {
            var token = await _usersService.Login(request.Email, request.Password);

            if (token == "") { return BadRequest("Login Error"); }

            HttpContext.Response.Cookies.Append("SecretStuff", token);

            return Ok(token);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] UsersRequest request)
        {
            var userId = await _usersService
                .UpdateUser(id, request.UserName, request.Email, request.Password);

            return Ok(userId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> Deleteuser(Guid id)
        {
            return Ok(await _usersService.DeleteUser(id));
        }
    }
}
