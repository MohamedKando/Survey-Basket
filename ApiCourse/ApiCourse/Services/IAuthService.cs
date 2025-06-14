﻿
using ApiCourse.Contract.Authentcations;

namespace ApiCourse.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponse?>> GetTokenAsync(string email , string password , CancellationToken cancellation = default);

        Task<Result<AuthResponse?>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellation = default);

        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellation = default);
    }
}
