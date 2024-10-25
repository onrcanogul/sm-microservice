using Microsoft.AspNetCore.Mvc;
using Post.API.Models.Dtos;
using Post.API.Services.Abstracts;
using Shared.Base.Controller;

namespace Post.API.Controllers;

public class PostController(IPostService service) : AbstractBaseController
{
    [HttpGet]
    public async Task<IActionResult> Get()
        => ControllerResponse(await service.GetListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]Guid id)
        => ControllerResponse(await service.GetFirstOrDefaultAsync(x => x.Id == id));
    
    [HttpGet("/user/{userId}")]
    public async Task<IActionResult> GetByUser([FromRoute] Guid userId)
        => ControllerResponse(await service.GetByUser(userId));
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostDto dto)
        => ControllerResponse(await service.CreateAsync(dto));
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PostDto dto)
        => ControllerResponse(await service.UpdateAsync(dto));
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => ControllerResponse(await service.DeleteAsync(id));
}