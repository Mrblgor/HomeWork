//using Program;
//using System;
//using System.Collections.Generic;
//using System.Reflection.Emit;
//using System.Text;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Program;

internal class GameSession(INumberGenerator generator, int lives)
{
    public delegate void GameMessage(string message);
    public event GameMessage OnGameMessage;
    public event Action<string> OnGameSave;
    private readonly INumberGenerator _generator = generator;
    private int _lives = lives;

    public void Start(int left, int right)
    {
        int secret = _generator.GetNumber(left, right);
        Console.WriteLine($"Числовые границы игры [ {left} : {right} ]");
        Console.WriteLine($"У вас есть {_lives} жизней");

        while (_lives > 0)
        {
            Console.WriteLine("Введи число");
            bool numbUserTroo = int.TryParse(Console.ReadLine(), out int numbUser);
            if (numbUserTroo == false)
            {
                OnGameMessage?.Invoke("Вы вводите не число");
                _lives--;
                OnGameMessage?.Invoke($"Жизней осталось {_lives}");
                continue;
            }
            bool proverka = Test.Control(numbUser, left, right);
            if (proverka == false)
            {
                OnGameMessage?.Invoke("Число выходит за границы игры");
                _lives--;
                OnGameMessage?.Invoke($"Жизней осталось {_lives}");
                continue;
            }
            Exodus result = Test.Skelet(numbUser, secret);
            if (result == Exodus.Victory)
            {
                OnGameMessage?.Invoke("Ура!!! Победа!!!");
                OnGameSave?.Invoke($" Победа! Осталось жизней: {_lives}\n");
                return;
            }
            if (result == Exodus.More)
            {
                OnGameMessage?.Invoke("Загаданное число больше");
            }
            if (result == Exodus.Less)
            {
                OnGameMessage?.Invoke("Загаданное число меньше");
            }
            _lives--;
            OnGameMessage?.Invoke($"Жизней осталось {_lives}");
        }
        OnGameSave?.Invoke($" Поражение! Жизни закончились. {_lives}\n");
        Console.WriteLine("Вы проиграли жизни закончились");
        OnGameMessage?.Invoke("Нажмите любую клавишу для выхода из игры");
        Console.ReadKey();
        Console.Clear();
    }
}
