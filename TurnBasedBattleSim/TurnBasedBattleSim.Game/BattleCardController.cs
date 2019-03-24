using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim
{
    public abstract class BattleCardController : IDisposable
    {
        public void Dispose()
        {
        }

        public abstract void TurnUpdate(BattleCard battleCard);

        public abstract void OnTurnStarted(BattleCard battleCard);

        public abstract void OnTurnEnded(BattleCard battleCard);

        public abstract void PerformSkill(BattleCard battleCard, Skill selectedSkill, List<BattleCard> targets);
    }
}
