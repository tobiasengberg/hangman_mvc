using Hangman.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.Controllers;

public class HangmanController : Controller
{
    private readonly HangmanService _service;

    public HangmanController()
    {
        _service = new HangmanService();
    }
    // GET
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Guess") == null)
        {
            string newGame = _service.PrepareGame();
            HttpContext.Session.SetString("Guess", newGame);
        } 
        string gameData = HttpContext.Session.GetString("Guess");
        GameSession game = _service.GetGameSession(gameData);

        ViewBag.guesses = game.Guesses;
        ViewBag.letters = game.IncorrectLetters;
        ViewBag.secret = game.RevealedWord;
        if (game.Won)
        {
            ViewBag.Won = $"You won the game with {game.Guesses} guesses left!";
        }
       
        return View();
    }

    [HttpPost]
    public IActionResult Index(string guess)
    {
        string gameData = HttpContext.Session.GetString("Guess");
        string editedString = _service.HandleGuess(gameData, guess);
        HttpContext.Session.SetString("Guess", editedString);
        return RedirectToAction("Index");
    }

    public IActionResult Restart()
    {
        HttpContext.Session.Remove("Guess");
        return RedirectToAction("Index");
    }
}