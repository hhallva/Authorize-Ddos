using DataLayer.DataContexts;
using DataLayer.DTOs;
using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class AccountController(TokenService service, AppDbContext context) : ControllerBase
    {
        /// <summary>
        /// Метод для получения JWT-токена 
        /// </summary>
        /// <remarks>Принимает учетные данные пользователя(login и password), при успешной авторизации возвращает JWT-токен</remarks>
        /// <param name="user">Учетные данные пользователя(login и password)</param>
        /// <returns>JWT-токен</returns>
        /// <response code="201">Успешная аутентификация</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="400">Неверные параметры</response>
        /// <response code="403">Доступ запрещен</response>
        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiErrorDto))]
        public async Task<IActionResult> LoginAsync(LoginDto user)
        {
            if (string.IsNullOrEmpty(user.Login))
                return BadRequest(new ApiErrorDto("Логин не указан", 1000));
            if (string.IsNullOrEmpty(user.Password))
                return BadRequest(new ApiErrorDto("Пароль не указан", 1001));

            var dbUser = await context.Users.SingleOrDefaultAsync(u => u.Login == user.Login);
            if (dbUser == null)
                return NotFound(new ApiErrorDto("Пользователь не найден", 1002));

            if (dbUser.LockTime != null && dbUser.LockTime.Value > DateTime.Now)
                return BadRequest(new ApiErrorDto($"Пользователь заблокированн. Дождитесь окончания блокировки: {dbUser.LockTime.Value - DateTime.Now}", 1003));

            if (dbUser.Password != user.Password)
            {
                dbUser.FailedAttempts++;
                if (dbUser.FailedAttempts >= 3)
                {
                    dbUser.LockTime = DateTime.Now.AddMinutes(2);

                    context.Users.Update(dbUser);
                    await context.SaveChangesAsync();
                    return BadRequest(new ApiErrorDto($"Пользователь заблокирован на 2 минуты", 1004));
                }
                context.Users.Update(dbUser);
                await context.SaveChangesAsync();
                return StatusCode(403, new ApiErrorDto($"Доступ запрещен", 1005));
            }
            return Created("", service.GenerateToken(dbUser));
        }
 
        [HttpPut("Password")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(string password)
        {
            var tokenUser = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            string login = tokenUser.Value;

            var user = await context.Users.SingleOrDefaultAsync(u => u.Login == login);
            user.Password = password;
            context.Users.Update(user);
            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return Ok(user);
        }

    }
}
