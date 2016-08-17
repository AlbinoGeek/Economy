// <copyright file="Program.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
using System;
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
    private static string[] population = {
        "AngryAlbino",
        "JohnGeese",
        "StabbyGaming",
        "Big Hoss",
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

        Random r = new Random();
        Map map = new Map(100, 25);
        map.Generate();
        
        // Because Windows CMD is limited to 16 colors, we only use 8 for players.
        Color[] ourColors = new Color[11]; // Also : Wheat , LawnGreen , Yellow , White , Black
        for (int i = 0; i < ourColors.Length; i++)
        {
            ourColors[i] = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
        }

        int j = 0;
        foreach (string name in population)
        {
            Agent agent = new Agent(name);
            
            agent.X = r.Next(map.Width - 2) + 1;
            agent.Y = r.Next(map.Height - 2) + 1;

            // Because Windows CMD is limited to 16 colors, we only use 8 for players.
            agent.Colour = ourColors[j];
            j++;
            if (j >= ourColors.Length)
            {
                j = 0;
            }

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
        
        // Give vassvik agent 100x Water
        market.Seed(16, "Water", 100);
        
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
            Console.Write("         Player            |   $|Food|Water", Color.Wheat);

            // Show player status per alive Agent
            Utils.ClearArea(63, map.Height + 2, market.Agents.Count, 37);
            for (int i = 0; i < market.Agents.Count; i++)
            {
                if (market.Agents[i].Alive)
                {
                    Console.SetCursorPosition(63, map.Height + 2 + i);
                    Console.Write(market.Agents[i].ToString(), market.Agents[i].Colour);
                }
            }
            
            // Wait 1.2 seconds per turn .
            Thread.Sleep(1200);
        }

        // Everyone is dead, show the final map status
        map.Display();
        
        // Clear everything but the map
        Utils.ClearArea(0, map.Height, market.Agents.Count + 2, 100);

        // Show the result screen
        Console.SetCursorPosition(0, map.Height + 3);

        var result =
            from a in market.Agents
            where a.Alive
            select a;

        // HACK: Ignore (or really, use) the error where nobody wins.
        string winner = "Nobody";
        try
        {
            winner = result.First().ToString();
            Utils.LogWarn("Economy WE HAVE FOUND A WINNER");
        }
        catch { }

        Utils.LogWarn("Economy Winner: " + winner);
        Utils.LogWarn($"Economy Lasted {market.Round} rounds.");

        return retCode;
    }
}
