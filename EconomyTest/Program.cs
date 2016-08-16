// <copyright file="Program.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;

using Economy;

using Console = Colorful.Console;

/// <summary>
/// represents the main entry point when executed
/// </summary>
public class Program
{
    /// <summary>
    /// represents the main entry point when executed
    /// \see Program.Run
    /// </summary>
    /// <param name="args">ignored launch parameters</param>
    public static void Main(string[] args)
    {
        Program program = new Program();

        int ret = program.Run();

        // Run() has finished, and returned a code
        Console.WriteLine("Program has terminated " + (ret == 0 ? "successfully" : "with exit code " + ret));
        Console.ReadKey();
    }

    /// <summary>
    /// actual program code, instanced
    /// calls \ref this.MainLoop
    /// </summary>
    /// <returns>0 on success</returns>
    public int Run()
    {
        // Use refelction to show our version number
        Console.WriteLine("EconomyTest v" + Assembly.GetExecutingAssembly().GetName().Version.ToString());
        
        Market market = new Market();
        market.Agents = new List<Agent>();

        Random r = new Random();
        Map map = new Map(100, 20);
        map.Generate();

        string[] population =
        {
            "AngryAlbino",
            "JohnGeese",
            "StabbyGaming",
            "deccer",
            "E.B.",
            "human_supremacist",
            "Le Chat",
            "Malscythe",
            "Prxy",
            "RobbieW",
            "SadCloud123",
            "sean",
            "Sense",
            "Westermin",
            "wubbalubbadubdub",
            "vassvik",
        };
        foreach (string name in population)
        {
            Agent agent = new Agent(name);

            agent.X = r.Next(map.Width - 2) + 1;
            agent.Y = r.Next(map.Height - 2) + 1;
            map.Register(ref agent);

            // Everyone gets a basic allowance
            agent.Seed("Money", r.Next(9) + 6);
            agent.Seed("Bread", r.Next(24) + 6);
            agent.Seed("Water", r.Next(24) + 6);

            market.Register(ref agent);
        }

        // Give one agent $800
        market.Seed(1, "Money", 800);

        // Give one agent 100x Bread
        market.Seed(2, "Bread", 100);
        
        // Give Malscythe 10x Liquor
        //market.Seed(5, Item.Liquor, 10);

        // Give vassvik control of the world supply of memes
        //market.Seed(7, Item.Meme, 9001);

        // Give SadCloud123 agent 10x Crystals
        //market.Seed(9, Item.Crystal, 10);

        // Give vassvik agent 100x Water
        market.Seed(16, "Water", 100);

        // Give Westermin agent 10x Magic Mushrooms
        //market.Seed(11, Item.MagicMushroom, 10);

        map.Display();

        Utils.LogInfo("Press any key to simulate...");
        Console.ReadKey();
        Console.ReadKey();
        Console.Clear();

        // Economy main loop
        int ret = this.MainLoop(ref market, ref map);

        Utils.LogInfo("Economy finished simulating.");
        Console.ReadKey();
        return ret;
    }

    /// <summary>
    /// looping portion of program code
    /// </summary>
    /// <param name="market">economy instance to simulate</param>
    /// <param name="map">map to draw agents on</param>
    /// <returns>0 on success</returns>
    private int MainLoop(ref Market market, ref Map map)
    {
        int retCode = 0;

        Utils.LogInfo("Economy Simulating ...");
        int countAlive = market.Agents.Count;

        // Loop economy until only one person is left.
        while (countAlive > 1)
        {
            // Clear old Logs
            Utils.ClearArea(0, map.Height + 2, 10, 63);

            // Displya the map at 0,0
            Console.SetCursorPosition(0, 0);
            map.Display();

            // Show events that happened during the Turn
            Console.SetCursorPosition(0, map.Height + 1);
            countAlive = market.Step();

            // Show table header
            Console.SetCursorPosition(55, map.Height + 1);
            Console.Write("         Player            |   $|Food|Water", Color.SteelBlue);

            // Show player status per alive Agent
            Utils.ClearArea(63, map.Height + 2, market.Agents.Count, 50);
            for (int i = 0; i < market.Agents.Count; i++)
            {
                if (market.Agents[i].Alive)
                {
                    Console.SetCursorPosition(63, map.Height + 2 + i);
                    Console.Write(market.Agents[i].ToString(), Color.LightSteelBlue);
                }
            }
            
            // Wait 2 seconds per turn .
            Thread.Sleep(2000);
        }

        // Everyone is dead, show the final map status
        map.Display();
        
        // Clear everything but the map
        Utils.ClearArea(0, map.Height, market.Agents.Count + 10, 100);

        // Show the result screen
        Console.SetCursorPosition(0, map.Height + 3);

        var result =
            from a in market.Agents
            where a.Alive
            select a;

        string winner = "Nobody";
        try
        {
            winner = result.First().ToString();
            Utils.LogWarn("Economy WE HAVE FOUND A WINNER");
        }
        // HACK: Ignore (or really, use) the error where nobody wins.
        catch { }
        
        Utils.LogWarn("Economy Winner: " + winner);
        Utils.LogWarn($"Economy Lasted {market.Round} rounds.");

        return retCode;
    }
}
