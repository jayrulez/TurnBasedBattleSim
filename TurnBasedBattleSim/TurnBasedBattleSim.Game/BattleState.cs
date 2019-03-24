using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedBattleSim
{
    public enum BattleState
    {
        Start,
        Loading,
        Combat,
        TurnEnd,
        Win,
        Lose
    }
}
