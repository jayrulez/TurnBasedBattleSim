using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim
{
    public class StatModifier
    {
        private Dictionary<StatType, List<StatModification>> StatModifications = new Dictionary<StatType, List<StatModification>>();

        private Dictionary<StatType, StatModification> StatTotals = new Dictionary<StatType, StatModification>();

        public StatModifier()
        {
            Enum.GetValues(typeof(StatType)).OfType<StatType>().ToList().ForEach((statType) =>
            {
                StatTotals.Add(statType, new StatModification(0, 1));
            });
        }

        public void AddModification(StatType statType, StatModification modification)
        {
            if (StatModifications.ContainsKey(statType))
            {
                StatModifications[statType].Add(modification);
            }
            else
            {
                StatModifications.Add(statType, new List<StatModification>()
                {
                    modification
                });
            }

            StatTotals[statType].Amount += modification.Amount;
            StatTotals[statType].Percentage += modification.Percentage;
        }

        public void RemoveModification(StatType statType, Guid key)
        {
            if (StatModifications.ContainsKey(statType))
            {
                var modifications = StatModifications[statType];

                var modification = modifications.FirstOrDefault(m => m.Key.Equals(key));

                if (modification != null)
                {
                    StatTotals[statType].Amount -= modification.Amount;
                    StatTotals[statType].Percentage = modification.Percentage;

                    modifications.Remove(modification);
                }
            }
        }

        public void ClearModifications()
        {
            StatTotals.ToList().ForEach(statTotal =>
            {
                statTotal.Value.Amount = 0;
                statTotal.Value.Percentage = 1;
            });

            StatModifications.Clear();
        }

        public int GetModifiedAmount(StatType statType)
        {
            return StatTotals[statType].Amount;
        }

        public float GetModifiedPercentage(StatType statType)
        {
            return StatTotals[statType].Percentage;
        }
    }
}
