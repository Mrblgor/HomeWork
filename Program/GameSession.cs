using Program;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Program;

internal class GameSession
{
    public delegate void GameMessage(string message);
    public event GameMessage OnGameMessage;
    private readonly INumberGenerator _generator;
    private int _lives = 5;
    public GameSession(INumberGenerator generator, int lives)
    {
        _generator = generator;
        _lives = lives;
    }
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
                _lives--;
                if (OnGameMessage != null)
                {
                    OnGameMessage?.Invoke("Вы вводите не число");
                }
                OnGameMessage?.Invoke($"Жизней осталось {_lives}");
                continue;
            }
            bool proverka = Test.Control(numbUser, left, right);
            if (proverka == false && OnGameMessage != null)
            {
                OnGameMessage?.Invoke("Число выходит за границы игры");
                _lives--;
                OnGameMessage?.Invoke($"Жизней осталось {_lives}");
            }
            Exodus result = Test.Skelet(numbUser, secret);
            if (result == Exodus.Victory)
            {
                if (OnGameMessage != null)
                {
                    OnGameMessage?.Invoke("Ура!!! Победа!!!");
                }
                File.AppendAllText("GameStats.txt", $"{DateTime.Now}: Победа! Осталось жизней: {_lives}\n");
                return;
            }
            if (result == Exodus.More)
            {
                if (OnGameMessage != null)
                {
                    OnGameMessage?.Invoke("Загаданное число больше");
                }
            }
            if (result == Exodus.Less)
            {
                if (OnGameMessage != null)
                {
                    OnGameMessage?.Invoke("Загаданное число меньше");
                }
            }
            _lives--;
            OnGameMessage?.Invoke($"Жизней осталось {_lives}");
        }
        File.AppendAllText("GameStats.txt", $"{DateTime.Now}: Поражение! Жизни закончились. {_lives}\n");
        Console.WriteLine("Вы проиграли жизни закончились");
        if (OnGameMessage != null)
        {
            OnGameMessage.Invoke("Нажмите любую клавишу для выхода из игры");
        }
        Console.ReadKey();
        Console.Clear();
    }
}
