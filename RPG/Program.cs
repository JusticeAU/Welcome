using System;
using System.Collections.Generic;
using StringTheory;

namespace RPG
{
    internal class Program
    {
        
        static void Main()
        {
            Console.CursorVisible = false;
            Console.Title = "RPG";
            GameManager game = GameManager.getInstance();
            game.RunGame();
        }
    }
}
