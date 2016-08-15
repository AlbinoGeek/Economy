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

            this.collection = new Dictionary<Item, int>();
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
                return this.ItemCount(Item.Bread);
            }

            private set
            {
                this.Seed(Item.Bread, value - this.ItemCount(Item.Bread));
            }
        }

        /// <summary>
        /// Gets the total sum of all potable liquids carried
        /// </summary>
        public int Water
        {
            get
            {
                return this.ItemCount(Item.Water);
            }

            private set
            {
                this.Seed(Item.Water, value - this.ItemCount(Item.Water));
            }
        }

        /// <summary>
        /// Gets the total sum of all currencies carried
        /// </summary>
        public int Wealth
        {
            get
            {
                return this.ItemCount(Item.Currency);
            }

            private set
            {
                this.Seed(Item.Currency, value - this.ItemCount(Item.Currency));
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
            if (this.collection.ContainsKey(item))
            {
                // (does have) Add to quantity
                this.collection[item] = quantity + this.collection[item];
            }
            else
            {
                // (doesn't have) Add to collection
                this.collection.Add(item, quantity);
            }
        }

        /// <summary>
        /// called by \ref Market to move us forward a cycle
        /// </summary>
        public void Tick()
        {
            this.Ascii = this.Alive ? "+" : "-";
            if (this.Alive)
            {
                // Consume resources
                this.Seed(Item.Bread, -1);
                this.Seed(Item.Water, -1);
            }

            this.Alive = !(this.Food < 0 || this.Water < 0);
            if (this.Food < 0)
            {
                this.CauseOfDeath = DeathCause.Starvation;
            }

            if (this.Water < 0)
            {
                this.CauseOfDeath = DeathCause.Dehydration;
            }
        }

        /// <summary>
        /// basic status information that represents the Agent
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return $"{this.Name} (Wealth {this.Wealth}) Food: {this.Food} Water: {this.Water}";
        }
        
        /// <summary>
        /// counts the number of item in \ref this.collection
        /// </summary>
        /// <param name="item">item to search for</param>
        /// <returns>total count</returns>
        private int ItemCount(Item item)
        {
            if (this.collection.ContainsKey(item))
            {
                return this.collection[item];
            }

            return 0;
        }
    }
}
