namespace Manyminds.Application.Interfaces
{
    public interface ITokenService : IDisposable
    {

        Task<string> GerarToken(string email);

        Task<string> RetornarEmailTokenClaims();
    }
}
