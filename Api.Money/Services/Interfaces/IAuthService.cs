using Data;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Получает юзера по логину
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<User> GetUserByLogin(string login, string password);
        
        /// <summary>
        /// Регистрация юзера
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Register(string login, string password);
    }
}
