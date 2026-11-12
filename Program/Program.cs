//using System.CodeDom.Compiler;
//using System.IO;
//using System.Reflection.Emit;
//using System.Threading.Channels;
//using static Program.GameSession;

namespace Program;


class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        INumberGenerator myGem = new RandomNumber();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Привет, это Игра \"Угадай Число\" ");
        Console.WriteLine("Выбери сложность игры");
        Console.WriteLine("1 - Легкая сложность: 10 жизней");
        Console.WriteLine("2 - Средняя сложность: 5 жизней");
        Console.WriteLine("3 - Хард сложность: 2 жизней");
        int complexity = Convert.ToInt32(Console.ReadLine());
        int lives;
        switch (complexity)
        {
            case 1:
                lives = 10;
                break;
            case 2:
                lives = 5;
                break;
            case 3:
                lives = 2;
                break;
            default:
                Console.WriteLine("Дальше Бога нет");
                return;
        }
        GameSession session = new(myGem, lives);
        FileSave saver = new(@"C:\Users\igorb\OneDrive\Desktop\вуз\2_курс\Технологии_програмирования_C#\Угадай игру\Program\Program\bin\Debug\net10.0\GameStats.txt");
        session.OnGameMessage += Console.WriteLine;
        session.OnGameSave += saver.Save;
        Viborka(session);
    }
    static void Viborka(GameSession session)
    {
        Console.WriteLine("Хотите сами выставить границы или же поиграть со встроиными границами?");
        Console.WriteLine("1 - Сам");
        Console.WriteLine("2 - Встроенные");
        int lox = Convert.ToInt32(Console.ReadLine());
        switch (lox)
        {
            case 1:
                Console.WriteLine("Введи граници");
                bool userLeft = int.TryParse(Console.ReadLine(), out int start);
                bool userRight = int.TryParse(Console.ReadLine(), out int end);
                if (userLeft && userRight)
                {
                    session.Start(start, end);
                }
                else
                {
                    Console.WriteLine("Границы игры должны бить числами");
                }
                break;

            case 2:
                Console.WriteLine("Запуск со встроенными границами");
                session.Start(1, 100);
                break;
            default:
                Console.WriteLine("Такой команды нет!!!");
                break;
        }
    }
}
