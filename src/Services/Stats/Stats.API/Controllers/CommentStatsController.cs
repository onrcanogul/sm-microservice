using Microsoft.AspNetCore.Mvc;
using Shared.Base.Controller;
using Stats.API.Context;
using Stats.API.Models;
using Stats.API.Models.Dto;
using Stats.API.Services;

namespace Stats.API.Controllers;

public class CommentStatsController(IStatsService<CommentStats, CommentStatsDto, StatsDbContext> service) : AbstractBaseController
{
    //create and delete will manage by events
    [HttpGet("{commentId:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid commentId)
        => ControllerResponse(await service.GetFirstOrDefaultAsync(x => x.CommentId == commentId));
}