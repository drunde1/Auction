using Auction.API.Contracts;
using Auction.Application.Services;
using Auction.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TangerinesController : ControllerBase  
    {
        private readonly ITangerinesService _tangerinesService;
        private readonly IBetSingletonService _betSingletonService;

        public TangerinesController(ITangerinesService tangerinesService, IBetSingletonService betSingletonService)
        {
            _tangerinesService = tangerinesService;
            _betSingletonService = betSingletonService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TangerinesResponse>>> GetTangerines()
        {
            var tangerines = await _tangerinesService.GetAllTangerines();

            var response = tangerines.Select(t => new TangerinesResponse(t.Id, t.Name, t.Place, t.Weight, t.StartPrice, t.IsActive, t.ExpirationDate));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTangerine([FromBody] TangerinesRequest request)
        {
            var (tangerine, error) = Tangerine.Create(
                Guid.NewGuid(),
                request.Name,
                request.Place,
                request.Weight,
                request.StartPrice,
                request.IsActive,
                request.ExpirationDate);

            if (!string.IsNullOrEmpty(error)) 
            {
                return BadRequest(error);
            }

            var tangerineId = await _tangerinesService.CreateTangerine(tangerine);

            return Ok(tangerineId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateTangerine(Guid id, [FromBody] TangerinesRequest request)
        {
            var tangerineId = await _tangerinesService
                .UpdateTangerine(id, request.Name, request.Place, request.Weight, request.StartPrice, request.IsActive, request.ExpirationDate);

            return Ok(tangerineId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteTengerine(Guid id)
        {
            return Ok(await _tangerinesService.DeleteTangerine(id));
        }

        [Authorize]
        [HttpPut("NewBet{tangerineId:guid}")]
        public async Task<ActionResult<Guid>> NewBet(Guid tangerineId, [FromBody] NewBetRequest request)
        {
            try
            {
                var prevUserId = await _tangerinesService.NewBet(tangerineId, HttpContext.Request.Cookies["SecretStuff"]!, request.newBet);
                await _betSingletonService.NewBet(tangerineId);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex); }
        }
    }
}
