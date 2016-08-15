namespace Economy
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
    public class Market
    {
        // TODO(Albino) This probably doesn't have to be public.
        public List<Agent> Agents;

        /// <summary>
        /// current number of turns simulated
        /// </summary>
        private int round = 0;

        public void Register(Agent agent)
        {
            agent.market = this;
            Agents.Add(agent);
        }

        public void Seed(int agent, Item item, int quantity)
        {
            Agents[agent-1].Seed(item, quantity);
        }
        
        // TODO(Albino) Naming inconsistency: Everything else Tick() s
        public int Step()
        {
            round++;

            Utils.LogInfo("Economy [Turn " + round + "]");
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
