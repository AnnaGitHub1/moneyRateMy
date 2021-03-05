using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Auth;
using Services.Interfaces;

namespace Api.Money.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        protected long UserId { get; set; }

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginModel model)
        {
            var user = await _authService.GetUserByLogin(model.Login, model.Password);

            if (user == null)
                return BadRequest(new { message = "Логин или пароль не корректный" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel model)
        {
            var user = await _authService.GetUserByLogin(model.Login, model.Password);

            if (user != null)
                return BadRequest(new { message = "Пользователь уже существует" });

            var newUser = await _authService.Register(model.Login, model.Password);
            return Ok(newUser);
        }
    }
}