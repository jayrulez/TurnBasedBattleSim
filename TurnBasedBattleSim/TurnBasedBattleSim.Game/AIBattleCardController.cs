using System;
using System.Collections.Generic;
using System.Linq;
using Xenko.Core.Mathematics;

namespace TurnBasedBattleSim
{
    public class AIBattleCardController : BattleCardController
    {
        private const float WaitTime = 1f;
        protected float PrevWaitTime = 0;

        public override void OnTurnEnded(BattleCard battleCard)
        {
            battleCard.HideTurnIndicator();
        }

        public override void OnTurnStarted(BattleCard battleCard)
        {
            battleCard.ShowTurnIndicator();
            PrevWaitTime = MathUtil.Clamp(BattleManager.Instance.GameTime.Total.Seconds + WaitTime, 0, 59); // cannot let this reach 60 because seconds will not pass 59.
        }

        public override void TurnUpdate(BattleCard battleCard)
        {
            if(BattleManager.Instance.GameTime.Total.Seconds >= PrevWaitTime)
            {
                var enemies = BattleManager.Instance.BattleCards.Where(card => !card.IsDead && card.TeamTag != battleCard.TeamTag).ToList();
                
                var random = new Random();

                var target = enemies?[random.Next(0, enemies.Count)];

                var targets = new List<BattleCard>() { target };

                PerformSkill(battleCard, battleCard.Skill1, targets);

                battleCard.EndTurn();
            }
        }

        public override void PerformSkill(BattleCard battleCard, Skill selectedSkill, List<BattleCard> targets)
        {
            foreach(var target in targets)
            {
                target.TakeDamage(battleCard.GetDamageDealt());
            }
        }
    }
}
