namespace ApiCourse.Errors
{
    public static class JwtErrors
    {
        public static readonly Error InvalidJwt = new Error("Jwt.InvalidJwt", "Jwt is not valid");

        public static readonly Error ExpireToken = new Error("Jwt.ExpireToken", "Jwt or refresh token is expired");
    }
}
