using Microsoft.AspNetCore.Mvc;
using Shared.Base.Controller;
using Stats.API.Context;
using Stats.API.Models;
using Stats.API.Models.Dto;
using Stats.API.Services;

namespace Stats.API.Controllers;

public class PostStatsController(IStatsService<PostStats, PostStatsDto, StatsDbContext> service) : AbstractBaseController
{
    //create and delete will manage by events
    [HttpGet("{postId:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid postId)
        => ControllerResponse(await service.GetFirstOrDefaultAsync(x => x.PostId == postId));
}