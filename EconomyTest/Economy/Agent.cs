// <copyright file="Agent.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
namespace Economy
{
    using System.Collections.Generic;

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
            Destination.X = this.X;
            Destination.Y = this.Y;
        }

        /// <summary>
        /// called by \ref Market to move us forward a cycle
        /// </summary>
        public void Tick()
        {
            if (Alive)
            {
                // If we have reached our destination
                if (Destination.Distance(new Point(this.X, this.Y)) < 1f)
                {
                    // Set a new destination
                    Destination.X = market.Random.Next(parent.Width - 2) + 1;
                    Destination.Y = market.Random.Next(parent.Height - 2) + 1;
                }

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

            Alive = !(Food < 0 || Water < 0);
            if (Food < 0)
            {
                CauseOfDeath = DeathCause.Starvation;
            }

            if (Water < 0)
            {
                CauseOfDeath = DeathCause.Dehydration;
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
            return $"{Name} (Wealth {Wealth}) Food: {Food} Water: {Water}";
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
