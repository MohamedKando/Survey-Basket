using ApiCourse.Contract.Questions;
using ApiCourse.Persistence;

namespace ApiCourse.Services.QuestionSerice
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _context;
        public QuestionService(AppDbContext context)
        {
            _context = context;   
        }
        public async Task<Result<QuestionResponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellationToken = default)
        {
            var pollIsExist = await _context.Polls.AnyAsync(x=>x.Id == pollId, cancellationToken);
            if (!pollIsExist)
            {
                return Result.Failure<QuestionResponse>(PollErrors.EmptyPoll);
            }
            var questionIsExist = await _context.Questions.AnyAsync(x=>x.Content == request.Content&&x.PollId==pollId, cancellationToken);

            if (questionIsExist)
            {
                return Result.Failure<QuestionResponse>(QuestionErrors.DublicateQuestion);
            }
            var question = request.Adapt<Question>();
            question.PollId = pollId;
            request.Answers.ForEach(answer => question.Answers.Add(new Answer { Content = answer }));
            await _context.AddAsync(question, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(question.Adapt<QuestionResponse>());

        }
    }
}
