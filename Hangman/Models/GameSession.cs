namespace Hangman.Models;

public class GameSession
{
    public int SecretWord { get; set; }
    public string RevealedWord { get; set; }
    public string IncorrectLetters { get; set; }
    public int Guesses { get; set; }
    public bool Won { get; set; }
}