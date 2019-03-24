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
        }
    }
}
