using LBank.Data;
using LBank.Extensions;
using LBank.Models;
using LBank.Services;
using LBank.Utils;
using LBank.ViewModel;
using LBank.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace LBank.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private AccountService accountService = new AccountService();
    private UserService userService = new UserService();

    [HttpPost("v1/user")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] RegisterViewModel model,
        [FromServices] DataContext context)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var userVerify = userService.GetUserByEmail(model.UserEmail);
            if (userVerify != null)
                return StatusCode(401, new ResultViewModel<User>(UtilMessages.user02XE05()));

            var user = new User
            {
                UserId = 0,
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                UserSlug = model.UserSlug,
                UserPassword = PasswordHasher.Hash(model.UserPassword)
            };

            userService.CreateUser(user);
            var account = accountService.AccountCreateUser(user.UserId);

            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.UserName,
                user.UserEmail,
                account
            }));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(400, new ResultViewModel<User>(UtilMessages.user02XE02(ex)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<User>(UtilMessages.user02XE01(ex)));
        }
    }

    [HttpPost("v1/user/login")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginViewModel model,
        [FromServices] DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = userService.GetUserByEmail(model.UserEmail);
        if (user == null)
            return StatusCode(401, new ResultViewModel<User>(UtilMessages.user02XE03()));

        if (!PasswordHasher.Verify(user.UserPassword, model.UserPassword))
            return StatusCode(401, new ResultViewModel<User>(UtilMessages.user02XE04()));

        try
        {
            //token
            return Ok("Usuario Logado com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<User>(UtilMessages.user02XE01(ex)));
        }
    }
}