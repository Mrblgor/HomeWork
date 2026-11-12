using System;
using System.Collections.Generic;
using System.Text;

namespace Program;

class FileSave(string filePath)
{
    private readonly string _filePath = filePath;
    public void Save(string messag)
    {
        File.AppendAllText(_filePath, $"[{DateTime.Now}]: {messag}\n");
    }
}
