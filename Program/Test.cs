using System;
using System.Collections.Generic;
using System.Text;

namespace Program;
class Test
{
    public static bool Control(int numbUser, int start, int end)
    {
        return numbUser > start && numbUser < end;
    }
    public static Exodus Skelet(int numbUser, int secrteNumbers)
    {
        if (numbUser > secrteNumbers) return Exodus.Less;
        if (numbUser < secrteNumbers) return Exodus.More;
        return Exodus.Victory;

    }
}
