using Models.DTO;

namespace Services.Common.Interface
{
    public interface ITokenRepository
    {
        Task<OAuthTokenResponse> GetUser(string username, string password);
        Task<OAuthTokenResponse> GetMongoData(string username, string password);
    }
}