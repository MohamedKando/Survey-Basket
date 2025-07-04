using ApiCourse.Authentications;
using ApiCourse.Contract.Authentcations;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace ApiCourse.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly int _refreshTokenExpiryDays = 14;
        public AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }
        public async Task<Result<AuthResponse?>> GetTokenAsync(string email, string password, CancellationToken cancellation = default)
        {
            // check user is exist or not 
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Failure<AuthResponse?>(UserErrors.UserInvalidCredentials);
            }

            // check passowrd

            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
            {
                return Result.Failure<AuthResponse?>(UserErrors.UserInvalidCredentials);

            }
            // generate the jwt
            var (token, expiersIn) = _jwtProvider.GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiryDate = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresIn = refreshTokenExpiryDate
            });

            await _userManager.UpdateAsync(user);

            return Result.Success<AuthResponse?>(new AuthResponse
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token,
                ExpiresIn = expiersIn * 60,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = refreshTokenExpiryDate
            });

        }



        public async Task<Result<AuthResponse?>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellation = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId == null)
                return Result.Failure<AuthResponse?>(UserErrors.UserInvalidCredentials);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result.Failure<AuthResponse?>(UserErrors.UserInvalidCredentials);
            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.isActive == true);
            if (userRefreshToken == null)
                return Result.Failure<AuthResponse?>(UserErrors.UserInvalidCredentials);

            userRefreshToken.RevokedOn = DateTime.UtcNow;
            var (newToken, expiersIn) = _jwtProvider.GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiryDate = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresIn = refreshTokenExpiryDate
            });
            await _userManager.UpdateAsync(user);
            return Result.Success<AuthResponse?>(new AuthResponse
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = newToken,
                ExpiresIn = expiersIn * 60,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiration = refreshTokenExpiryDate
            });
        }



        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellation = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId == null)
                return Result.Failure(JwtErrors.InvalidJwt);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result.Failure(UserErrors.UserNotFound);
            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.isActive == true);
            if (userRefreshToken == null)
                return Result.Failure(JwtErrors.ExpireToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return Result.Success();

        }
        private static string GenerateRefreshToken()
        {

            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
