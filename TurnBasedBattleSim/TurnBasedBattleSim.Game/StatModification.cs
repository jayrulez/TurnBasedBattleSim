using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim
{
    public class StatModification
    {
        public Guid Key { get; }

        public int Amount = 0;
        public float Percentage = 0;

        public StatModification(int amount, float percentage)
        {
            Key = Guid.NewGuid();
            Amount = amount;
            Percentage = percentage;
        }
    }
}
