namespace Economy
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
    public class Agent : MapObject
    {
        public bool Alive = true;
        public DeathCause CauseOfDeath = DeathCause.None;

        public string Name = "";
        
        public Market market = null;
        
        public int Food
        {
            private set
            {
                Seed(Item.Bread, value - CountItem(Item.Bread));
            }
            get
            {
                return CountItem(Item.Bread);
            }
        }

        public int Water
        {
            private set
            {
                Seed(Item.Water, value - CountItem(Item.Water));
            }
            get
            {
                return CountItem(Item.Water);
            }
        }

        public int Wealth
        {
            private set
            {
                Seed(Item.Currency, value - CountItem(Item.Currency));
            }
            get
            {
                return CountItem(Item.Currency);
            }
        }

        private Dictionary<Item, int> collection;

        public Agent(string Name)
        {
            collection = new Dictionary<Item, int>();

            this.Name = Name;
        }

        public void Tick()
        {
            this.Ascii = Alive ? "+" : "";
            if (Alive)
            {
                // Consume resources
                Seed(Item.Bread, -1);
                Seed(Item.Water, -1);
            }

            this.Alive = !(Food < 0 || Water < 0);
            if (this.Food < 0)
            {
                this.CauseOfDeath = DeathCause.Starvation;
            }
            if (this.Water < 0)
            {
                this.CauseOfDeath = DeathCause.Dehydration;
            }
        }

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

        public override string ToString()
        {
            return Name
                + " (Wealth: " + Wealth + ")"
                + " Food: " + Food
                + " Water: " + Water;
        }

        private int CountItem(Item item)
        {
            if (collection.ContainsKey(item))
            {
                return collection[item];
            }
            return 0;
        }
    }
}
