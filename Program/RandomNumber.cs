//using System;
//using System.Collections.Generic;
//using System.Text;

namespace Program;
public interface INumberGenerator
{
    int GetNumber(int start, int end);
}
internal class RandomNumber : INumberGenerator
{
    public int GetNumber(int start, int end)
    {
        return new Random().Next(start, end + 1);
    }
}
