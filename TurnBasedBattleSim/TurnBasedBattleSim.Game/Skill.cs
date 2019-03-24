using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim
{
    public enum SkillTarget
    {
        SingleEnemy,
        RandomEnemy,
        AllEnemies,
        SingleAlly,
        RandomAlly,
        AllAllies,
        AllAlliesExceptSelf,
        Field,
        FieldExcludingSelf,
        Self
    }

    public enum SkillMode
    {
        Active,
        Passive
    }

    public class SkillEffect
    {
        public SkillTarget Target { get; set; }
    }

    public enum SkillType
    {
        Damage,

    }

    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillTarget Target { get; set; }
        public SkillMode Mode { get; set; }
        public int Cooldown { get; set; }
        public float Modifier { get; set; }
        public int SkillIconId { get; set; }

        public List<SkillEffect> Effects { get; set; } = new List<SkillEffect>();
    }
}
