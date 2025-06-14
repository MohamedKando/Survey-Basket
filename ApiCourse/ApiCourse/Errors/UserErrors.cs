namespace ApiCourse.Errors
{
    public static class UserErrors
    {
        public static readonly Error UserInvalidCredentials = new Error("User.InvalidCredentials", "Invalid Email / Password");

        public static readonly Error UserNotFound = new Error("User.NotFound", "There is no user with the give id");
    }
}
