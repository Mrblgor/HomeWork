using System;
using System.Collections.Generic;
using System.Text;

namespace Program;
class Test
{
    public static bool Control(int numbUser, int start, int end)
    {
        if (numbUser < start || numbUser > end)
        {
            Console.WriteLine("Число выходит за границы игры");
            return false;
        }
        else return true;
    }
    public static Exodus Skelet(int numbUser, int secrteNumbers)
    {
        if (numbUser > secrteNumbers) return Exodus.Less;
        if (numbUser < secrteNumbers) return Exodus.More;
        return Exodus.Victory;

    }
}
