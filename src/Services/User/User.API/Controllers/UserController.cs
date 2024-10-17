using Microsoft.AspNetCore.Mvc;
using Shared.Base.Controller;
using User.API.Models.Dtos;
using User.API.Services.Abstracts;

namespace User.API.Controllers;

public class UserController(IUserService service) : AbstractBaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
        => ControllerResponse(await service.GetById(id));
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        => ControllerResponse(await service.Login(loginDto));
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        => ControllerResponse(await service.Register(registerDto));
    
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(Guid userId)
        => ControllerResponse(await service.ResetPassword(userId));
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
        => ControllerResponse(await service.Delete(id));
    
}