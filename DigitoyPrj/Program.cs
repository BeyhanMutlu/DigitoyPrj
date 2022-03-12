using System;
using System.Collections.Generic;

using DigitoyPrj.Tools;

namespace DigitoyPrj
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();  
            players.Add(new Player(1, "Player 1"));
            players.Add(new Player(2, "Player 2"));
            players.Add(new Player(3, "Player 3"));
            players.Add(new Player(4, "Player 4"));

            HelperFunctions helperFunctions = new HelperFunctions();
            
            List<Player> playerTiles = helperFunctions.PrepareGame(2, players);
            for (int i = 0; i < playerTiles.Count; i++)
            {
                Console.WriteLine((i+1)+". place " + playerTiles[i].name + " and score: "+ playerTiles[i].rank);
            }           
            Console.ReadLine();
        }
    }
}
