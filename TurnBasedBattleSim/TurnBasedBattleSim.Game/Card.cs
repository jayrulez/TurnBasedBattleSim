using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenko.Core.Mathematics;

namespace TurnBasedBattleSim
{
    public abstract class Card
    {
        public string Name { get; protected set; }

        public int Hp { get; protected set; }

        public int Attack { get; protected set; }

        public int Defense { get; protected set; }

        public int Speed { get; protected set; }
        
        public Skill Skill1 { get; set; }

        public Skill Skill2 { get; set; }

        public Skill Skill3 { get; set; }

        public Skill Skill4 { get; set; }

        public Card()
        {
            Name = GetType().Name;
            Hp = 0;
            Attack = 0;
            Speed = 0;
            Defense = 0;
            Skill1 = null;
            Skill2 = null;
            Skill3 = null;
            Skill4 = null;
        }

        public Card(string name, int hp, int attack, int defense, int speed, Skill skill1, Skill skill2, Skill skill3, Skill skill4)
        {
            Name = name;
            Hp = hp;
            Attack = attack;
            Defense = defense;
            Speed = speed;
        }
    }
}
