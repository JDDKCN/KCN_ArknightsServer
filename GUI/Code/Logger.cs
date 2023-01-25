using System;
using System.Diagnostics;
using System.Linq;

namespace KCNAPI;

public enum VerboseLevel
{
    NONE = 0, // No logging except for errors
    WARNS = 1, // Log warns
    ALL = 2, // Warns and (useless) debug
    VERBL = 3, // Warns, debug and verbose
    VERBH = 4, // Warns, debug, verbose and very verbose (thanks copilot this is so funny)
}

public class Logger
{
    public static VerboseLevel VerboseLevel = VerboseLevel.WARNS;
    private string _name;
    private ConsoleColor _nameColor;

    public Logger(string name, ConsoleColor nameColor = ConsoleColor.Gray)
    {
        _name = name;
        _nameColor = nameColor;
    }

    public void Log(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        var time = DateTime.Now.ToString("[HH:mm:ss]");
        Console.Write($"{time} ");
        Console.Write('<');
        Console.ForegroundColor = _nameColor;
        Console.Write(_name);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("> ");
        Console.ResetColor();
        //Console.Write(string.Join('\t', msg));
        Console.Write(msg);
        Console.Write('\n');
    }

    public void Warn(params string[] msg)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        var time = DateTime.Now.ToString("[HH:mm:ss]");
        Console.Write($"{time} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.Write($"WARN<{_name}>");
        Console.ResetColor();
        Console.Write(' ');
        //Console.Write(string.Join('\t', msg));
        Console.Write(('\t', msg));
        Console.Write('\n');
    }

    public void Trail(params string[] msg)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        //Console.WriteLine($"\t→ {string.Join(' ', msg)}");
        Console.WriteLine($"\t→ {(' ', msg)}");
        Console.ResetColor();
    }

    public void Error(string msg, bool stack = true)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        var time = DateTime.Now.ToString("[HH:mm:ss]");
        Console.Write($"{time} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.Write($"ERROR<{_name}>");
        Console.ResetColor();
        Console.WriteLine($" {msg}");
        if (stack)
        {
            StackTrace trace = new(true);
            Trail(trace.ToString());
        }
    }

    public void Debug(params string[] msg)
    {
#if DEBUG
        Console.ForegroundColor = ConsoleColor.Gray;
        var time = DateTime.Now.ToString("[HH:mm:ss]");
        Console.Write($"{time} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Write($"DEBUG<{_name}>");
        Console.ResetColor();
        Console.Write(' ');
        //Console.Write(string.Join('\t', msg));
        Console.Write(('\t', msg));
        Console.Write('\n');
#endif
    }
}
