﻿// <copyright file="Program.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Economy;

using Console = Colorful.Console;

/// <summary>
/// represents the main entry point when executed
/// </summary>
public class Program
{
    /// <summary>
    /// string printed to console to clear a line
    /// </summary>
    private static string clearLine = "                                                                                                    ";

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
        Map map = new Map(100, 16);
        map.Generate();

        string[] population =
        {
            "AngryAlbino",
            "JohnGeese",
            "StabbyGaming",
            "Le Chat",
            "Malscythe",
            "Prxy",
            "vassvik",
            "human_supremacist",
            "SadCloud123",
            "Sense",
            "Westermin",
        };
        foreach (string name in population)
        {
            Agent agent = new Agent(name);

            agent.X = r.Next(map.Width - 2) + 1;
            agent.Y = r.Next(map.Height - 2) + 1;
            map.Register(ref agent);

            // Everyone gets a basic allowance
            agent.Seed(Item.Currency, r.Next(7) + 3);
            agent.Seed(Item.Bread, r.Next(22) + 3);
            agent.Seed(Item.Water, r.Next(22) + 3);

            market.Register(ref agent);
        }

        // Give one agent $1000
        market.Seed(1, Item.Currency, 1000);

        // Give one agent 100x Bread
        market.Seed(2, Item.Bread, 100);
        
        // Give Malscythe 10x Liquor
        market.Seed(5, Item.Liquor, 10);

        // Give vassvik control of the world supply of memes
        market.Seed(7, Item.Meme, 9001);

        // Give SadCloud123 agent 10x Crystals
        market.Seed(9, Item.Crystal, 10);

        // Give vassvik agent 100x Water
        market.Seed(10, Item.Water, 100);

        // Give Westermin agent 10x Magic Mushrooms
        market.Seed(11, Item.MagicMushroom, 10);

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
            Console.SetCursorPosition(0, 1);

            countAlive = market.Step();
            map.Display(2);

            for (int i = 0; i < market.Agents.Count; i++)
            {
                Console.SetCursorPosition(0, map.Height + 2 + i);
                Console.Write(clearLine);

                if (market.Agents[i].Alive)
                {
                    Console.SetCursorPosition(0, map.Height + 2 + i);
                    Utils.LogDebug("Agent: " + market.Agents[i]);
                }
            }

            Console.SetCursorPosition(0, map.Height + 2 + market.Agents.Count);
            System.Threading.Thread.Sleep(1000);
        }

        // Everyone is dead, show the final map status
        Console.SetCursorPosition(0, 1);
        market.Step();
        map.Display(2);

        for (int i = 0; i < market.Agents.Count; i++)
        {
            Console.SetCursorPosition(0, map.Height + 2 + i);
            Console.Write(clearLine);
        }

        Console.SetCursorPosition(0, map.Height + 3);

        var result =
            from a in market.Agents
            where a.Alive
            select a;

        Utils.LogWarn("Economy WE HAVE FOUND A WINNER");
        Utils.LogWarn("Economy Winner: " + result.First().ToString());
        return retCode;
    }
}
