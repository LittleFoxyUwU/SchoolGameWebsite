using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolGameSite.Models;

namespace SchoolGameSite.Pages;

public class Login : PageModel
{
    private readonly DataBaseContext _context;
    [BindProperty] public string LoginErrorMessage { get; set; } = "";

    public Login(DataBaseContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        var form = HttpContext.Request.Form;
        var username = form["username"][0]!;
        var password = form["password"][0]!;

        var person = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        if (person is null)
        {
            LoginErrorMessage = "Имя пользователя или пароль неверный!";
            return Page();
        }

        var claims = new List<Claim>{ new(ClaimTypes.Name, person.Username) };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        return Redirect("/");
    }
}