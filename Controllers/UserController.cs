using LBank.Data;
using LBank.Extensions;
using LBank.Models;
using LBank.Utils;
using LBank.ViewModel;
using LBank.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LBank.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("v1/user")]
    public async Task<IActionResult> PostAsync(
        [FromBody] RegisterViewModel model,
        [FromServices] DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = new User
        {
            UserId = 0,
            UserName = model.UserName,
            UserEmail = model.UserEmail,
            UserSlug = model.UserSlug,
            UserPassword = model.UserPassword
        };

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.UserName
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
}