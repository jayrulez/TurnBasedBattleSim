using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim.Cards
{
    public class Slime : Card
    {
        public Slime() : base()
        {
            Hp = 100;
            Defense = 35;
            Attack = 35;
            Speed = 35;
            Skill1 = new Skill
            {
                Name = "Lick",
                Description = "Licks the enemy.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 1.5f,
                SkillIconId = 4
            };
            Skill2 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 10
            };
            Skill3 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 11
            };
            Skill4 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 12
            };
        }
    }
}
