

using ApiCourse.Authentications;
using Microsoft.Extensions.Options;

namespace ApiCourse.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private protected IAuthService _authService;
        private protected JwtOptions _jwtOptions;
        public AuthController(IAuthService authService, IOptions<JwtOptions> JwtOptions)
        {
            _authService = authService;
            _jwtOptions = JwtOptions.Value;

        }
        [HttpPost("")]
        public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var authResult = await  _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);  
            if (authResult.IsFailure) 
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: authResult.Error.Code, detail: authResult.Error.Description);
            }
            else
                return Ok(authResult.Value);

        }

        [HttpPost("refresh")]

        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
            if (authResult.IsFailure)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest,title:authResult.Error.Code,detail: authResult.Error.Description);
            }
            else
                return Ok(authResult.Value);

        }

        [HttpPost("revoke-refresh-token")]

        public async Task<IActionResult> RevokeRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var isRevoked = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
            if (isRevoked.IsFailure)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: isRevoked.Error.Code, detail: isRevoked.Error.Description);
            }
            else
                return Ok();

        }
    }
}
