using System.Runtime.CompilerServices;
using ConsoleHelperLibrary.Classes;
using Spectre.Console;

namespace NorthWindExampleApp2;

internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample";
        
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

        AnsiConsole.Write(
            new FigletText("EF Core Order by")
                .Alignment(Justify.Center)
                .Color(Color.White));
        AnsiConsole.Write(
            new FigletText("Samples")
                .Alignment(Justify.Center)
                .Color(Color.White));


    }

    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

    private static void ExitPrompt()
    {
        Console.WriteLine();

        Render(new Rule($"[white on blue]Done[/]")
            .RuleStyle(Style.Parse("cyan"))
            .Centered());

        Console.ReadLine();

    }
}