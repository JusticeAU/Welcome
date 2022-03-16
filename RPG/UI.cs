using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using StringTheory;

namespace RPG
{
    public static class UI
    {
        public static int Choice(string prompt, ChoiceData cd)
        {
            // Config
            const char cursor = '>';

            // Internals
            int currentSelection = 0;
            bool confirmed = false;

            // Clear input buffer
            while (Console.KeyAvailable)
                Console.ReadKey(true);

            // Start rendering
            Console.WriteLine(prompt);

            // Render options
            for (int i = 0; i < cd.options.Length; i++)
            {
                if(cd.selectable[i]) Console.WriteLine($"\t{cd.options[i]}");
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"\t{cd.options[i]}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            
            }

            // Render Cursor
            while (!confirmed)
            {
                // Move Console Cursor back to the start of the options and insert our Cursor character.
                Console.CursorTop = Console.CursorTop - cd.options.Length;
                for (int i = 0; i < cd.options.Length; i++)
                {
                    // Render cursor
                    if (currentSelection == i) Console.WriteLine(cursor);
                    else Console.WriteLine(" ");
                }

                // Wait for Input to be in the buffer
                Console.CursorVisible = false;
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(16);
                }

                // Get input when in buffer
                ConsoleKeyInfo cki = Console.ReadKey(true);
                switch(cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentSelection--;
                        break;
                    case ConsoleKey.DownArrow:
                        currentSelection++;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        if(cd.selectable[currentSelection]) confirmed = true;
                        break;
                }

                // Action input
                if (confirmed) break;
                else
                {
                    if (currentSelection == cd.options.Length) currentSelection = 0;
                    else if (currentSelection < 0) currentSelection = cd.options.Length - 1;
                }
            }

            // Clear choices from screen
            // Move Console Cursor back to the start of the prompt + options and insert our Cursor character.
            // Assumes the prompt was a single line.
            Console.CursorTop = Console.CursorTop - (cd.options.Length+1);
            string blankLine = new string(' ', Console.BufferWidth-1);
            
            // Blank out text
            for (int i = 0; i < (cd.options.Length+1); i++)
            {
                // Blank out text
                Console.WriteLine(blankLine);
            }

            // Move cursor back to original position.
            Console.CursorTop = Console.CursorTop - (cd.options.Length+1);
            return cd.returnValue[currentSelection];
        }
    }
}
