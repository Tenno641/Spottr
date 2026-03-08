using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Participants.Commands.CreateParticipantProfile;
using UserManagement.Application.Users.Commands.RegisterUser;
using UserManagement.Contracts.Participants;
using UserManagement.Contracts.Users;

namespace UserManagement.Api.Controllers;

public class UsersController: ApiController
{
    private readonly ISender _mediator;
    
    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        RegisterUserCommand command = new RegisterUserCommand(request.Name, request.Email, request.Password);

        ErrorOr<Guid> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.FirstError.Description)
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateParticipantProfile(CreateParticipantProfileRequest request)
    {
        CreateParticipantProfileCommand command = new CreateParticipantProfileCommand(request.UserId);

        ErrorOr<Guid> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.FirstError.Description)
            : Ok(result.Value);
    }
}