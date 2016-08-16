// <copyright file="Market.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
namespace Economy
{
    using System.Collections.Generic;

    /// <summary>
    /// represents a simulated economy, must contain \ref Agent s
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
    public class Market
    {
        // TODO(Albino) This probably doesn't have to be public.

        /// <summary>
        /// registered list we call each \ref Agent.Tick on every \ref this.Tick
        /// </summary>
        public List<Agent> Agents;

        /// <summary>
        /// reference to our random library, constant to keep seed
        /// </summary>
        public System.Random Random = new System.Random();

        /// <summary>
        /// Gets the current round
        /// </summary>
        public int Round
        {
            get
            {
                return _round;
            }

            private set
            {
                _round = value;
            }
        }

        /// <summary>
        /// \see Round
        /// </summary>
        private int _round = 0;
        
        /// <summary>
        /// register an Agent for inclusion in the simulation
        /// </summary>
        /// <param name="agent">Agent to register</param>
        public void Register(ref Agent agent)
        {
            agent.Market = this;
            agent.Start();
            Agents.Add(agent);
        }

        /// <summary>
        /// \see Agent.Seed
        /// </summary>
        /// <param name="agent">agent to gift</param>
        /// <param name="itemName">name of item to create</param>
        /// <param name="quantity">amount to add</param>
        public void Seed(int agent, string itemName, int quantity)
        {
            Agents[agent - 1].Seed(itemName, quantity);
        }
        
        // TODO(Albino) Naming inconsistency: Everything else Tick() s

        /// <summary>
        /// represents a single round of simulation
        /// </summary>
        /// <returns>0 on success</returns>
        public int Step()
        {
            Round++;

            Utils.LogInfo("Economy [Turn " + Round + "]");
            int countAlive = 0;
            for (int i = 0; i < Agents.Count; i++)
            {
                Agents[i].Tick();
                if (Agents[i].Alive)
                {
                    countAlive++;
                }
            }

            return countAlive;
        }
    }
}
