using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("gyms/{gymId:guid}/rooms")]
public class RoomsController: ApiController
{
    private readonly ISender _mediator;
    
    public RoomsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom()
    {
        
    }
    
    [HttpDelete("{roomId:guid}")]
    public async Task<IActionResult> DeleteRoom()
}