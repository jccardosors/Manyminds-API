using Manyminds.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Manyminds.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _http;
        private readonly IRegistroLogsService _registroLogsService;

        public TokenService(IConfiguration configuration, IHttpContextAccessor http, IRegistroLogsService registroLogsService)
        {
            _config = configuration;
            _http = http;
            _registroLogsService = registroLogsService;
        }

        public async Task<string> GerarToken(string email)
        {
            var tokenString = string.Empty;
            try
            {
                await _registroLogsService.RegistrarLogs(email, "TokenService", "GerarToken");

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, email));

                var key = _config["Jwt:Key"];
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var tokenDuration = Convert.ToInt32(_config["Jwt:TokenDuration"]);

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(tokenDuration),
                    signingCredentials: signinCredentials);

                tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }

            return tokenString;
        }

        public async Task<string> RetornarEmailTokenClaims()
        {
            string email = string.Empty;
            try
            {

                var identity = _http.HttpContext.User.Identity! as ClaimsIdentity;

                IEnumerable<Claim> claim = identity.Claims;

                var usernameClaim = claim
                    .Where(x => x.Type == ClaimTypes.Name)
                    .FirstOrDefault();

                email = usernameClaim!.Value;

                await _registroLogsService.RegistrarLogs(email, "TokenService", "RetornarEmailTokenClaims");
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }

            return email;
        }

        #region Disposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
