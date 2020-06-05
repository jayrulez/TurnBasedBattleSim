using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim.Cards
{
    public class Golem : Card
    {
        public Golem() : base()
        {
            Hp = 120;
            Defense = 50;
            Attack = 45;
            Speed = 25;
            Skill1 = new Skill
            {
                Name = "Rock edge",
                Description = "Hits the enemy with rocks.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 1.3f,
                SkillIconId = 5
            };
            Skill2 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 16
            };
            Skill3 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 17
            };
            Skill4 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 18
            };
        }
    }
}
