using System;
using TurnBasedBattleSim.Game;
using Xenko.Core.Mathematics;
using Xenko.Engine;

namespace TurnBasedBattleSim
{
    public class BattleCard : IDisposable
    {
        public string Name { get; private set; }
        public Guid TeamTag { get; set; }

        public int TurnBar { get; private set; }

        public int BaseHp { get; set; }

        public int Hp { get; private set; }

        public int Attack { get; private set; }

        public int Defense { get; private set; }

        public int Speed { get; private set; }

        public int TrueSpeed => GetTotalStat(StatType.Speed);

        public Skill Skill1 { get; private set; }

        public Skill Skill2 { get; private set; }

        public Skill Skill3 { get; private set; }

        public Skill Skill4 { get; private set; }

        public Vector3 Position { get; set; } = new Vector3(0, 0, 1);

        public int CharacterSpriteIndex { get; set; } = 0;

        public Entity Entity { get; private set; }

        private readonly StatModifier StatModifier = new StatModifier();

        public delegate void Select(BattleCard battleCard);

        public event Select OnSelect = null;

        public delegate void TurnStart();
        public delegate void TurnEnd();
        public delegate void Death(BattleCard battleCard);

        public event TurnStart OnTurnStart = null;
        public event TurnEnd OnTurnEnd = null;
        public event Death OnDeath = null;

        public bool IsDead => Hp <= 0;

        public BattleCardController Controller { get; set; }

        private BattleCardScript BattleCardScript { get; set; }

        public void DisableEntity()
        {
            BattleCardScript.HideUI();
            Entity.EnableAll(false);
        }

        public BattleCard(Card card)
        {
            Name = card.Name;

            BaseHp = card.Hp;
            Hp = card.Hp;
            Attack = card.Attack;
            Defense = card.Defense;
            Speed = card.Speed;

            Skill1 = card.Skill1;
            Skill2 = card.Skill2;
            Skill3 = card.Skill3;
            Skill4 = card.Skill4;

            ResetTurnBar();
        }

        public void SetEntity(Entity entity)
        {
            Entity = entity;
        }

        public void SetScript(BattleCardScript script)
        {
            BattleCardScript = script;

            BattleCardScript.OnSelectCard += delegate
            {
                OnSelect?.Invoke(this);
            };
        }

        public int GetBaseStat(StatType statType)
        {
            switch (statType)
            {
                default:
                case StatType.Hp: return Hp;
                case StatType.Attack: return Attack;
                case StatType.Defense: return Attack;
                case StatType.Speed: return Speed;
            }
        }

        public int GetTotalStat(StatType statType)
        {
            var stat = GetBaseStat(statType);

            var total = (int)((stat + StatModifier.GetModifiedAmount(statType)) * StatModifier.GetModifiedPercentage(statType));

            return total;
        }

        public int GetDamageDealt()
        {
            int totalDamage = 0;

            totalDamage = GetTotalStat(StatType.Attack);

            return totalDamage;
        }

        public int GetDamageReceived(int damage)
        {
            int totalDamage = damage;

            var defense = GetTotalStat(StatType.Defense);

            totalDamage -= defense;

            return MathUtil.Clamp(totalDamage, BattleGlobals.MinDamage, BattleGlobals.MaxDamage);
        }

        public void TakeDamage(int damage)
        {
            int totalDamage = GetDamageReceived(damage);

            ModifyHp(Hp - totalDamage);

            OnDamageReceived(totalDamage);
        }

        public virtual void OnDamageReceived(int totalDamage)
        {
            var percentage = (((float)Hp / (float)BaseHp) * 100);

            BattleCardScript.SetHpPercentage(percentage);
        }

        public void ModifyHp(int value)
        {
            Hp = MathUtil.Clamp(value, 0, BattleGlobals.MaxHp);

            if (Hp <= 0)
            {
                Kill();
            }
        }

        public Guid AddStatModification(int amount, float percentage)
        {
            var modification = new StatModification(amount, percentage);

            return modification.Key;
        }

        public void RemoveStatModification(StatType statType, Guid key)
        {
            StatModifier.RemoveModification(statType, key);
        }

        public void ClearStatModifications()
        {
            StatModifier.ClearModifications();
        }

        public void OnBattleStart() { }

        public void OnBattleEnd() { }

        public void ShowTurnIndicator()
        {
            BattleCardScript.ShowTurnIndicator();
        }

        public void HideTurnIndicator()
        {
            BattleCardScript.HideTurnIndicator();
        }

        public void ShowTargetSelector()
        {
            BattleCardScript.ShowTargetSelector();
        }

        public void HideTargetSelector()
        {
            BattleCardScript.HideTargetSelector();
        }

        public void StartTurn()
        {
            OnTurnStart?.Invoke();

            HandleTurnStart();

            //EndTurn();
        }

        public void EndTurn()
        {
            OnTurnEnd?.Invoke();

            HandleTurnEnd();

            ResetTurnBar();

            BattleManager.Instance.EndTurn();
        }

        public void Kill()
        {
            Hp = 0;

            OnDeath?.Invoke(this);
        }

        private void HandleTurnStart()
        {
            Controller.OnTurnStarted(this);
        }

        public void HandleTurnEnd()
        {
            Controller.OnTurnEnded(this);
        }

        private void ResetTurnBar()
        {
            TurnBar = 0;
        }

        public void AdvanceTurnBar()
        {
            TurnBar += TrueSpeed * 2;
            
            var maxTurnBar = (float)BattleManager.Instance.MaxTurnBar;

            var percentage = (float)TurnBar / maxTurnBar * 100;

            BattleCardScript.SetTurnBarPercentage(percentage);
        }

        public void RestartTurn()
        {
            HandleTurnEnd();
            HandleTurnStart();
        }

        public void TurnUpdate()
        {
            Controller.TurnUpdate(this);
        }

        public void Dispose()
        {
            ClearStatModifications();

            OnSelect = null;

            OnTurnStart = null;
            OnTurnEnd = null;
            OnDeath = null;
        }
    }
}
