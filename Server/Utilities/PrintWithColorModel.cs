namespace Server.Utilities;

public static class PrintWithColorModel
{
    /// <summary>
    /// Prints the specified message with the specified color to the console.
    /// </summary>
    /// <param name="message">The message to print.</param>
    /// <param name="color">The color of the message.</param>
    public static void PrintWithColor(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}