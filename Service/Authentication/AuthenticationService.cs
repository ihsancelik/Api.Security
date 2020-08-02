using Api.Security.Database;
using Api.Security.Database.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Security.Service
{
    public class AuthenticationService
    {
        private readonly DataContext db;
        private readonly TokenService tokenService;

        public AuthenticationService(DataContext db, TokenService tokenService)
        {
            this.db = db;
            this.tokenService = tokenService;
        }
        public TokenInfo Authenticate(string username, string password, string clientType)
        {
            var user = db.Users
                .Include(s => s.UserTokens)
                .ThenInclude(s => s.AccessToken)
                .FirstOrDefault(s => s.Username == username && s.Password == password);

            var client = db.Clients.FirstOrDefault(s => s.Type == clientType);

            var userToken = user.UserTokens.FirstOrDefault(s => s.ClientId == client.Id);

            var tokenInfo = tokenService.GenerateToken(user.Id, user.Username);

            // Create a new token
            if (userToken == null)
            {
                var accessToken = new AccessToken()
                {
                    MultipleUseCount = 1,
                    Token = tokenInfo.Token,
                    TokenExpire = tokenInfo.Expire,
                };
                db.AccessTokens.Add(accessToken);
                db.SaveChanges();

                userToken = new UserToken()
                {
                    AccessTokenId = accessToken.Id,
                    UserId = user.Id,
                    ClientId = client.Id
                };
                db.UserTokens.Add(userToken);
                db.SaveChanges();
            }

            // update token
            else if (userToken.AccessToken.MultipleUseCount == 1)
            {
                var tokenId = userToken.AccessToken.Id;
                var accessToken = db.AccessTokens.FirstOrDefault(s => s.Id == tokenId);
                accessToken.Token = tokenInfo.Token;
                accessToken.TokenExpire = tokenInfo.Expire;

                db.AccessTokens.Update(accessToken);
                db.SaveChanges();
            }

            // return same token
            else
            {
                tokenInfo.Token = userToken.AccessToken.Token;
                tokenInfo.Expire = userToken.AccessToken.TokenExpire;
            }

            return tokenInfo;
        }
    }
}
