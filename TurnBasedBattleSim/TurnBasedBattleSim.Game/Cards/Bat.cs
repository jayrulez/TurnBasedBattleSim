using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim.Cards
{
    public class Bat : Card
    {
        public Bat(): base()
        {
            Hp = 100;
            Defense = 25;
            Attack = 40;
            Speed = 40;
            Skill1 = new Skill
            {
                Name = "Bite",
                Description = "Bites the enemy to inflict damage.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 1.1f,
                SkillIconId = 2
            };
            Skill2 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 19
            };
            Skill3 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 20
            };
            Skill4 = new Skill
            {
                Name = "Crush",
                Description = "Deals a heavy blow to the target.",
                Mode = SkillMode.Active,
                Target = SkillTarget.SingleEnemy,
                Cooldown = 0,
                Modifier = 2,
                SkillIconId = 21
            };
        }
    }
}
