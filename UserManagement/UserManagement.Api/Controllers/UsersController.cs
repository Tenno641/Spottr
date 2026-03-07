using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Users.Commands.RegisterUser;
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
        RegisterUserCommand command = new RegisterUserCommand(request.FullName, request.Email, request.Password);

        ErrorOr<Guid> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.FirstError.Description)
            : Ok(result.Value);
    }
}