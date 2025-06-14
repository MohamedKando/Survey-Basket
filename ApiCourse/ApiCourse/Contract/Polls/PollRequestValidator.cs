namespace ApiCourse.Contract.Polls
{

    public class PollRequestValidator : AbstractValidator<PollRequest>
    {
        public PollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Please Add Title")
                .Length(3, 100)
                .WithMessage("The minimum chars is 3 and the max is 100");


            RuleFor(x => x.StartsAt).NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Pleas Enter Valid Start Date");
            RuleFor(x => x.EndsAt).NotEmpty();
            RuleFor(x => x).Must(HasValidDate).WithMessage("The End Date Should ne greater than start date");

        }

        private bool HasValidDate(PollRequest pollRequest)
        {
            return pollRequest.EndsAt >= pollRequest.StartsAt;
        }
    }
}
