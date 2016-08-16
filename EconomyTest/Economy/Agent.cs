// <copyright file="Agent.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
namespace Economy
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// represents an entity which takes part in a Market simulation
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
    public class Agent : MapObject
    {
        /// <summary>
        /// whether we are still a functioning agent
        /// </summary>
        public bool Alive = true;

        public Point Destination;

        /// <summary>
        /// if ! \ref this.Alive then this is how we died
        /// </summary>
        public DeathCause CauseOfDeath = DeathCause.None;

        /// <summary>
        /// our friendly name
        /// </summary>
        public string Name = string.Empty;
        
        // TODO(Albino) This musnt't be a public variable.

        /// <summary>
        /// economy simulation we take part in (our parent)
        /// </summary>
        public Market market = null;
        
        /// <summary>
        /// represents everything the Agent owns
        /// </summary>
        private List<Item> collection;

        /// <summary>
        /// number of trades this turn
        /// </summary>
        private int trades = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent" /> class. with a name and no items
        /// </summary>
        /// <param name="name">friendly name, shown in leaderboard</param>
        public Agent(string name)
        {
            this.Name = name;

            collection = new List<Item>();
        }

        /// <summary>
        /// various ways an Agent could have died
        /// </summary>
        public enum DeathCause
        {
            /// <summary>
            /// not currently dead
            /// </summary>
            None,

            /// <summary>
            /// death by \ref Food less than 0
            /// </summary>
            Starvation,

            /// <summary>
            /// death by \ref Water less than 0
            /// </summary>
            Dehydration,

            /// <summary>
            /// death by unknown cause
            /// </summary>
            Unknown,
        }
        
        /// <summary>
        /// Gets the total sum of all edible foods carried
        /// </summary>
        public int Food
        {
            get
            {
                return ItemCount("Bread");
            }

            private set
            {
                Seed("Bread", value - ItemCount("Bread"));
            }
        }

        /// <summary>
        /// Gets the total sum of all potable liquids carried
        /// </summary>
        public int Water
        {
            get
            {
                return ItemCount("Water");
            }

            private set
            {
                Seed("Water", value - ItemCount("Water"));
            }
        }

        /// <summary>
        /// Gets the total sum of all currencies carried
        /// </summary>
        public int Wealth
        {
            get
            {
                return ItemCount("Money");
            }

            private set
            {
                Seed("Money", value - ItemCount("Money"));
            }
        }
        
        /// <summary>
        /// present an item (miraculous creation)
        /// </summary>
        /// <param name="item">item to create</param>
        /// <param name="quantity">amount to add</param>
        public void Seed(string itemName, int quantity)
        {
            // If we already have on, increase stack count
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].Name == itemName)
                {
                    collection[i].Quantity += quantity;
                    return;
                }
            }

            // Else create a new stack
            Item item = new Item(itemName);
            item.Quantity = quantity;
            collection.Add(item);
        }

        /// <summary>
        /// initialize our status
        /// </summary>
        public void Start()
        {
            // Have no destination at the start
            Destination = Position;
        }

        /// <summary>
        /// called by \ref Market to move us forward a cycle
        /// </summary>
        public void Tick()
        {
            if (Alive)
            {
                // If we have reached our destination
                if (Point.Distance(Position, Destination) < 1f)
                {
                    // Set a new destination
                    Destination = new Point(
                        market.Random.Next(parent.Width - 2) + 1,
                        market.Random.Next(parent.Height - 2) + 1
                    );
                }

                var nearby = GetNeighbors(5);
                Search(nearby.ToList());

                // Using Linq : Filter our list down to Agents only
                var objects =
                    from mapObject in nearby
                    where mapObject.GetType() == typeof(Agent)
                    select mapObject;

                List<Agent> agents = objects.OfType<Agent>().ToList();

                var alive =
                    from a in agents
                    where a.Alive
                    select a;

                Trade(alive.ToList());

                var dead =
                    from a in agents
                    where !a.Alive
                    select a;

                Loot(dead.ToList());

                // Move towards destination
                if (Destination.X > this.X)
                {
                    this.X += 1;
                }
                else if (Destination.Y > this.Y)
                {
                    this.Y += 1;
                }
                else if (Destination.X < this.X)
                {
                    this.X -= 1;
                }
                else if (Destination.Y < this.Y)
                {
                    this.Y -= 1;
                }

                // Consume resources
                Seed("Bread", -1);
                Seed("Water", -1);
            }

            bool newlyDead = false;
            if ((Food <= 0 || Water <= 0) && Alive)
            {
                newlyDead = true;
            }

            Alive = !(Food <= 0 || Water <= 0);
            if (Food <= 0)
            {
                CauseOfDeath = DeathCause.Starvation;
            }

            if (Water <= 0)
            {
                CauseOfDeath = DeathCause.Dehydration;
            }

            if (newlyDead)
            {
                Utils.LogWarn($" {Name, -18} has died of " + CauseOfDeath.ToString() );
            }
        }

        private void Loot(List<Agent> nearby)
        {
            for (int i = 0; i < nearby.Count(); i++)
            {
                Agent other = nearby.ElementAt(i) as Agent;

                if (other.Wealth == 0 && other.Food == 0 && other.Water == 0)
                {
                    Utils.LogInfo($" {Name, -18} found the looted body of {other.Name}!");
                    continue;
                }

                Wealth += other.Wealth;
                Food += other.Food;
                Water += other.Water;

                other.Wealth -= other.Wealth;
                other.Food -= other.Food;
                other.Water -= other.Water;

                Utils.LogInfo($" {Name, -18} looted everything from {other.Name}!");
            }
        }

        private void Search(List<MapObject> nearby)
        {

        }

        private void Trade(List<Agent> nearby)
        {
            trades = 0;
            for (int i = 0; i < nearby.Count(); i++)
            {
                // Do no trades if we are broke
                if (Wealth <= 0)
                {
                    return;
                }

                Agent other = nearby.ElementAt(i) as Agent;
                
                // Buy Food from others if we need it
                if (Food < 5 && other.Food > 10)
                {
                    // Buy 1 food for 2 Money
                    other.Food--;
                    Wealth -= 2;

                    other.Wealth += 2;
                    Food++;

                    Utils.LogInfo($" {Name, -18} bought Food  from {other.Name}!");
                }
                // Buy Water from others if we need it
                else if (Water < 5 && other.Water > 10)
                {
                    // Buy 1 Water for 2 Money
                    other.Water--;
                    Wealth -= 2;

                    other.Wealth += 2;
                    Water++;

                    Utils.LogInfo($" {Name, -18} bought Water from {other.Name}!");
                }
                else
                {
                    break;
                }

                trades++;

                // Limit of 4 trades per turn
                if (trades == 4)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// based on \ref Alive status
        /// </summary>
        /// <returns>single character string representation</returns>
        public override string ToAscii()
        {
            return Alive ? "+" : "-";
        }
        
        /// <summary>
        /// basic status information that represents the Agent
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return $"{trades} | {Name, -18} | Wealth: {Wealth, 4}  Food: {Food, 3}  Water: {Water, 3}";
        }
        
        /// <summary>
        /// counts the number of item in \ref this.collection
        /// </summary>
        /// <param name="item">item to search for</param>
        /// <returns>total count</returns>
        private int ItemCount(string itemName)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].Name == itemName)
                {
                    return collection[i].Quantity;
                }
            }

            return 0;
        }
    }
}
