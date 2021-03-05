using Data;
using Microsoft.EntityFrameworkCore;
using Services.Helper;
using Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly WalletContext _walletContext;

        public AuthService(WalletContext walletContext)
        {
            _walletContext = walletContext;
        }

        public async Task<User> GetUserByLogin(string login, string password)
        {
            var md5 = new MD5CryptoServiceProvider();

            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var user = await _walletContext.Users.SingleOrDefaultAsync(f => f.Login == login && f.PasswordHash == hash.ByteArrayToString());

            return user;
        }

        public async Task<User> Register(string login, string password)
        {
            var md5 = new MD5CryptoServiceProvider();

            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var user = new User {Login = login, PasswordHash = hash.ByteArrayToString()};
            await _walletContext.Users.AddAsync(user);
            await _walletContext.SaveChangesAsync();
            return user;
        }
    }
}
