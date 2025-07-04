
using Mapster;

namespace ApiCourse.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
           // config.NewConfig<Poll, PollResponse>().Map(dest => dest.Notes, src => src.Description);
           config.NewConfig<QuestionRequest,Question>().Ignore(nameof(Question.Answers));
        }
    }
}
