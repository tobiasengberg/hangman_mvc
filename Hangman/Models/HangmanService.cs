namespace Hangman.Models;

public class HangmanService
{
    private string GetWord(int i)
    {
        string[] words = ["Picture", "Leaving", "Functional", "Symbolic", "Laughter"];
        return words[i];
    }
    public string PrepareGame()
    {
        int index = new Random().Next(0, 4);
        string secretWord = GetWord(index);
        string revealedWord = new String('_', secretWord.Length);
        string newGame = $"{index},{revealedWord},,10,false";
        return newGame;
    }
    
    public GameSession GetGameSession(string gameSession)
    {
        GameSession session = new GameSession();
        string[] GameData = gameSession.Split(',');
        session.SecretWord = int.Parse(GameData[0]);
        session.RevealedWord = GameData[1];
        session.IncorrectLetters = GameData[2];
        session.Guesses = int.Parse(GameData[3]);
        session.Won = Convert.ToBoolean(GameData[4]);
        return session;
    }

    public string HandleGuess(string gameData, string guess)
    {
        string newGuess = guess.ToLower();
        GameSession session = GetGameSession(gameData);
        if (!session.Won)
        {
            string secretWord = GetWord(session.SecretWord).ToLower();
            if (guess.Length > 1)
            {
                if (newGuess == secretWord.ToLower())
                {
                    session.Won = true;
                    session.RevealedWord = secretWord;
                }
                else
                {
                    session.Guesses--;
                }
            }
            else
            {
                if (!session.IncorrectLetters.Contains(newGuess) && !session.RevealedWord.Contains(newGuess))
                {
                    if (secretWord.Contains(newGuess))
                    {
                        char[] edited = session.RevealedWord.ToCharArray();
                        for (int i = 0; i < secretWord.Length; i++)
                        {
                            if (secretWord[i] == newGuess[0])
                            {
                                edited[i] = newGuess[0];
                            }
                        }
                    
                        session.RevealedWord = new string(edited);
                        if (!session.RevealedWord.Contains('_'))
                        {
                            session.Won = true;
                        }
                    }
                    else
                    {
                        session.IncorrectLetters += newGuess;
                        session.Guesses--;
                    }
                }
            }
        }
        
        
        return $"{session.SecretWord},{session.RevealedWord},{session.IncorrectLetters},{session.Guesses},{session.Won}";
        ;
    }
}