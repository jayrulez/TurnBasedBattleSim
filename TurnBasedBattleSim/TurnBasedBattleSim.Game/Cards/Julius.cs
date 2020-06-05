using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim.Cards
{
    public class Julius : Card
    {
        public Julius() : base()
        {
            Hp = 150;
            Defense = 50;
            Attack = 50;
            Speed = 50;
            Skill1 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 3
            };
            Skill2 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 7
            };
            Skill3 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 8
            };
            Skill4 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 9
            };
        }
    }
}
