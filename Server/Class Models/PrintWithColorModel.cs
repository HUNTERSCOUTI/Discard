namespace DiscardSERVER.Class_Models;

public static class PrintWithColorModel
{
    public static void PrintWithColor(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}