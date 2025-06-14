
using ApiCourse.Contract.Polls;
using ApiCourse.Mapping;
using ApiCourse.Models;
using ApiCourse.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace ApiCourse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PollsController : ControllerBase
{
    private readonly IPollService _pollService;

    public PollsController(IPollService pollService)
    {
        _pollService = pollService;
    }
    
    [HttpGet("")]
    public async Task< IActionResult> GetAll(CancellationToken cancellationtoken)
    {
        var polls = await _pollService.GetAllAsync(cancellationtoken);
        var Polls= polls.Adapt<IEnumerable<PollResponse>>();
        return Ok(Polls);
    }

    [HttpGet("{id}")]
    public async Task< IActionResult> Get([FromRoute] int id, CancellationToken cancellationtoken)
    {
        var poll = await _pollService.GetAsync(id, cancellationtoken);
        if (poll.IsFailure)
            return BadRequest(poll?.Error);
        // statues code 200

        var Polls = poll.Value.Adapt<PollResponse>();

        return Ok(Polls);
    }

    [HttpPost("")]
    // Cancletion Token deh mn el a5r 3ashan tl8y ay action lo ant 3ml close ll browser aw w2ft el action fe nos el tnfez
    public async Task< IActionResult> Add([FromBody] PollRequest request , CancellationToken cancellationtoken)
    {

        var newpoll = await _pollService.AddAsync(request, cancellationtoken);
        
        // statues code 201
        return CreatedAtAction(nameof(Get), new { id = newpoll.Value!.Id }, newpoll.Value);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationtoken)
    {
        var isupdated = await _pollService.UpdateAsync(id, request, cancellationtoken);

        if (isupdated.IsFailure)
            return Problem(statusCode:StatusCodes.Status404NotFound,title:isupdated.Error.Code,detail:isupdated.Error.Description);
        // statues code 204
        return NoContent();

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationtoken)
    {
        var isDeleted =await _pollService.DeleteAsync(id, cancellationtoken);

        if (isDeleted.IsFailure)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: isDeleted.Error.Code, detail: isDeleted.Error.Description);
        // statues code 204
        return NoContent();

    }

    [HttpPut("{id}/togglePublish")]
    public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationtoken)
    {
        var isDeleted = await _pollService.TogglePublishStatuesAsync(id, cancellationtoken);

        if (isDeleted.IsFailure)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: isDeleted.Error.Code, detail: isDeleted.Error.Description);
        // statues code 204
        return NoContent();

    }
    [HttpDelete("clear")]
    public async Task<IActionResult> ClearAll(CancellationToken cancellationToken)
    {
        var result = await _pollService.ClearAllPollsAsync(cancellationToken);

        if (result.IsFailure)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Code, detail: result.Error.Description);

        return NoContent(); // HTTP 204
    }


}
