



using Microsoft.AspNetCore.Authorization;

namespace ApiCourse.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet("{id}")]

        public IActionResult Get()
        {
            return Ok();
        }
        [HttpPost("")]

        public async Task<IActionResult> Add([FromRoute] int pollId, [FromBody] QuestionRequest request , CancellationToken cancellationToken)
        {
            var isAdded = await _questionService.AddAsync(pollId, request , cancellationToken);

            if(isAdded.IsFailure)
            {
                if(isAdded.Error.Code== "Question.Dublicate")
                { 
                    return Problem(statusCode: StatusCodes.Status409Conflict, title: isAdded.Error.Code, detail: isAdded.Error.Description);
                }
                else
                {
                    return Problem(statusCode: StatusCodes.Status404NotFound, title: isAdded.Error.Code, detail: isAdded.Error.Description);
                }
            }    
            return  CreatedAtAction(nameof(Get), new { pollId , isAdded.Value!.Id}, isAdded.Value);
        }

    }
}
