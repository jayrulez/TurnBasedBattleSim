using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim.Cards
{
    public class Thor : Card
    {
        public Thor() : base()
        {
            Hp = 140;
            Defense = 55;
            Attack = 55;
            Speed = 55;
            Skill1 = new Skill
            {
                Name = "Ragnarok",
                Description = "Attacks all enemies with brimstone and fire.",
                Mode = SkillMode.Active,
                Target = SkillTarget.AllEnemies,
                Cooldown = 0,
                Modifier = 3,
                SkillIconId = 6
            };
            Skill2 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 13
            };
            Skill3 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 14
            };
            Skill4 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 15
            };
        }
    }
}
