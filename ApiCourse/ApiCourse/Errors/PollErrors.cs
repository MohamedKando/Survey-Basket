namespace ApiCourse.Errors
{
    public static class PollErrors
    {
        public static readonly Error EmptyPoll = new Error("Poll.Wrong ID", "There is no poll with this id");

        public static readonly Error TitleExist = new Error("Poll.Dublicate Title","This title already exist");
    }
}
