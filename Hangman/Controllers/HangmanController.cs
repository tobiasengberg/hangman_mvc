using Microsoft.AspNetCore.Mvc;

namespace Hangman.Controllers;

public class HangmanController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}