namespace ApiCourse.Contract.Questions
{
    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(x => x.Content).NotEmpty()
                .Length(3,1000) ;
            RuleFor(x => x.Answers).NotNull();
            RuleFor(x => x.Answers).Must(x => x.Count > 1)
                .WithMessage("Question Should has at least 2 answers")
                 .When(x => x.Answers != null); 

            RuleFor(x => x.Answers).Must(x => x.Distinct().Count()==x.Count)
                .WithMessage("You cannot add dublicated answers for the same quesiton")
                .When(x=>x.Answers!=null);
        }
    }
}
