﻿using Knox.ConsoleCommanding;
using Sol.Domain.Common.Maybes;

var running = true;

var commands = new List<IConsoleCommand>();

while (running)
{
    foreach (var command in commands)
    {
        WriteLine(command.CommandDocumentation);
    }

    Write("\n] ");
    var inputRaw = Console.ReadLine().ToMaybe().GetOrElse(string.Empty)!;
    var input = inputRaw.Split(" ");
    var first = input.First().ToMaybe().GetOrElse(string.Empty);

    switch (first)
    {
        case "exit":
            WriteLine("Closing...");
            running = false;
            break;

        default:
            try
            {
                var success = false;
                foreach (var command in commands)
                {
                    if (first == command.CommandName)
                    {
                        var context = new ConsoleCommandContext(input);
                        await command.ExecuteAsync(context);

                        WriteSuccess($"{command.SuccessMessage}");

                        success = true;
                        break;
                    }
                }

                if (!success)
                {
                    WriteError($"Could not find command matching pattern {inputRaw}");
                }
            }
            catch (Exception ex)
            {
                WriteError($"Could not execute command matching pattern \"{inputRaw}\"");
                WriteError(ex.Message);
            }
            finally
            {
                WriteLine(string.Empty);
            }

            break;
    }
}

static void WriteError(string error) => WriteLine(error, ConsoleColor.Red);
static void WriteSuccess(string success) => WriteLine(success, ConsoleColor.DarkGreen);

static void WriteLine(string line = "", ConsoleColor color = ConsoleColor.Gray)
{
    Console.ForegroundColor = color;
    Console.WriteLine(line);
    Console.ResetColor();
}

static void Write(string line, ConsoleColor color = ConsoleColor.Gray)
{
    Console.ForegroundColor = color;
    Console.Write(line);
    Console.ResetColor();
}