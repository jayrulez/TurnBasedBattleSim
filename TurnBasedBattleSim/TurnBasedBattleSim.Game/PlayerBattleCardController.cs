using Stride.Engine;
using Stride.Rendering.Sprites;
using Stride.UI;
using Stride.UI.Controls;
using System.Collections.Generic;
using System.Linq;

namespace TurnBasedBattleSim
{
    public class PlayerBattleCardController : BattleCardController
    {
        private UIPage BattleUI;

        private List<BattleCard> SelectedTargets = new List<BattleCard>();
        private Skill SelectedSkill = null;

        public PlayerBattleCardController(UIPage battleUI)
        {
            BattleUI = battleUI;
        }

        private void PopulateSkillsList(BattleCard battleCard)
        {
            var skillListContainer = BattleUI.RootElement;

            var skillButtons = skillListContainer.FindVisualChildrenOfType<Button>();

            foreach (var skillButton in skillButtons)
            {
                skillButton.Visibility = Visibility.Collapsed;
            }

            if (battleCard.Skill1 != null)
            {
                SelectedSkill = battleCard.Skill1;

                var skill1Button = skillButtons.ToList().First(b => b.Name == "Skill1Button");

                var skillImage = skill1Button.FindVisualChildOfType<ImageElement>();

                if (skillImage != null)
                {
                    var sprite = (SpriteFromSheet)skillImage.Source;

                    sprite.CurrentFrame = battleCard.Skill1.SkillIconId;
                }

                skill1Button.Visibility = Visibility.Visible;

                skill1Button.Click += delegate
                {
                    SelectedSkill = battleCard.Skill1;
                };
            }

            if (battleCard.Skill2 != null)
            {
                var skill2Button = skillButtons.ToList().First(b => b.Name == "Skill2Button");

                var skillImage = skill2Button.FindVisualChildOfType<ImageElement>();

                if (skillImage != null)
                {
                    var sprite = (SpriteFromSheet)skillImage.Source;

                    sprite.CurrentFrame = battleCard.Skill2.SkillIconId;
                }

                skill2Button.Visibility = Visibility.Visible;

                skill2Button.Click += delegate
                {
                    SelectedSkill = battleCard.Skill2;
                };
            }

            if (battleCard.Skill3 != null)
            {
                var skill3Button = skillButtons.ToList().First(b => b.Name == "Skill3Button");

                var skillImage = skill3Button.FindVisualChildOfType<ImageElement>();

                if (skillImage != null)
                {
                    var sprite = (SpriteFromSheet)skillImage.Source;

                    sprite.CurrentFrame = battleCard.Skill3.SkillIconId;
                }

                skill3Button.Visibility = Visibility.Visible;

                skill3Button.Click += delegate
                {
                    SelectedSkill = battleCard.Skill3;
                };
            }

            if (battleCard.Skill4 != null)
            {
                var skill4Button = skillButtons.ToList().First(b => b.Name == "Skill4Button");

                var skillImage = skill4Button.FindVisualChildOfType<ImageElement>();

                if (skillImage != null)
                {
                    var sprite = (SpriteFromSheet)skillImage.Source;

                    sprite.CurrentFrame = battleCard.Skill4.SkillIconId;
                }

                skill4Button.Visibility = Visibility.Visible;

                skill4Button.Click += delegate
                {
                    SelectedSkill = battleCard.Skill4;
                };
            }

            skillListContainer.Visibility = Visibility.Visible;
        }

        private void _SelectTarget(BattleCard battleCard)
        {
            if (SelectedSkill != null)
            {
                SelectedTargets.Clear();

                SelectedTargets.Add(battleCard);
            }
        }

        public override void OnTurnEnded(BattleCard battleCard)
        {
            SelectedSkill = null;

            SelectedTargets.Clear();

            battleCard.HideTurnIndicator();

            var enemies = BattleManager.Instance.BattleCards.Where(card => card.TeamTag != battleCard.TeamTag).ToList();

            foreach (var enemy in enemies)
            {
                enemy.HideTargetSelector();

                enemy.OnSelect -= _SelectTarget;
            }
        }

        public override void OnTurnStarted(BattleCard battleCard)
        {
            SelectedSkill = null;

            SelectedTargets.Clear();

            PopulateSkillsList(battleCard);

            battleCard.ShowTurnIndicator();

            var enemies = BattleManager.Instance.BattleCards.Where(card => !card.IsDead && card.TeamTag != battleCard.TeamTag).ToList();

            foreach (var enemy in enemies)
            {
                enemy.OnSelect += _SelectTarget;
            }
        }

        public override void TurnUpdate(BattleCard battleCard)
        {
            if (SelectedSkill != null)
            {
                var enemies = BattleManager.Instance.BattleCards.Where(card => !card.IsDead && card.TeamTag != battleCard.TeamTag).ToList();

                foreach (var enemy in enemies)
                {
                    enemy.ShowTargetSelector();
                }

                if (SelectedTargets.Count > 0)
                {
                    var skillListContainer = BattleUI.RootElement;
                    skillListContainer.Visibility = Visibility.Visible;

                    PerformSkill(battleCard, SelectedSkill, SelectedTargets);
                    battleCard.EndTurn();
                }
            }
        }

        public override void PerformSkill(BattleCard battleCard, Skill selectedSkill, List<BattleCard> targets)
        {
            foreach (var target in targets)
            {
                target.TakeDamage(battleCard.GetDamageDealt());
            }
        }
    }
}
