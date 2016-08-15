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
        private Dictionary<Item, int> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent" /> class. with a name and no items
        /// </summary>
        /// <param name="name">friendly name, shown in leaderboard</param>
        public Agent(string name)
        {
            this.Name = name;

            collection = new Dictionary<Item, int>();
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
                return ItemCount(Item.Bread);
            }

            private set
            {
                Seed(Item.Bread, value - ItemCount(Item.Bread));
            }
        }

        /// <summary>
        /// Gets the total sum of all potable liquids carried
        /// </summary>
        public int Water
        {
            get
            {
                return ItemCount(Item.Water);
            }

            private set
            {
                Seed(Item.Water, value - ItemCount(Item.Water));
            }
        }

        /// <summary>
        /// Gets the total sum of all currencies carried
        /// </summary>
        public int Wealth
        {
            get
            {
                return ItemCount(Item.Currency);
            }

            private set
            {
                Seed(Item.Currency, value - ItemCount(Item.Currency));
            }
        }

        /// <summary>
        /// present an item (miraculous creation)
        /// </summary>
        /// <param name="item">item to create</param>
        /// <param name="quantity">amount to add</param>
        public void Seed(Item item, int quantity)
        {
            // Check if we have an item already
            if (collection.ContainsKey(item))
            {
                // (does have) Add to quantity
                collection[item] = quantity + collection[item];
            }
            else
            {
                // (doesn't have) Add to collection
                collection.Add(item, quantity);
            }
        }

        /// <summary>
        /// called by \ref Market to move us forward a cycle
        /// </summary>
        public void Tick()
        {
            Ascii = Alive ? "+" : "-";
            if (Alive)
            {
                // Consume resources
                Seed(Item.Bread, -1);
                Seed(Item.Water, -1);
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
        private int ItemCount(Item item)
        {
            if (collection.ContainsKey(item))
            {
                return collection[item];
            }

            return 0;
        }
    }
}
