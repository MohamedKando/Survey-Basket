using ApiCourse.Contract.Answers;

namespace ApiCourse.Contract.Questions
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;

        public IEnumerable<AsnwersResponse> Answers { get; set; } = [];
    }
}
