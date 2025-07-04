using ApiCourse.Contract.Questions;

namespace ApiCourse.Services.QuestionSerice
{
    public interface IQuestionService
    {
        Task<Result<QuestionResponse>> AddAsync(int pollId , QuestionRequest request , CancellationToken cancellationToken=default);
    }
}
