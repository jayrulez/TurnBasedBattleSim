using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using TurnBasedBattleSim.Cards;
using Stride.UI;
using Stride.UI.Controls;
using Stride.UI.Events;

namespace TurnBasedBattleSim.Game
{
    public class BattleScript : SyncScript
    {
        public UIPage BattleUI { get; set; }

        public override void Start()
        {
            var Enemy1Position = SceneSystem.SceneInstance.RootScene.Entities.First(e => e.Name == "Enemy1Position");
            var Enemy2Position = SceneSystem.SceneInstance.RootScene.Entities.First(e => e.Name == "Enemy2Position");
            var Enemy3Position = SceneSystem.SceneInstance.RootScene.Entities.First(e => e.Name == "Enemy3Position");
            var Enemy4Position = SceneSystem.SceneInstance.RootScene.Entities.First(e => e.Name == "Enemy4Position");

            var Ally1Position = SceneSystem.SceneInstance.RootScene.Entities.First(e => e.Name == "Ally1Position");
            var Ally2Position = SceneSystem.SceneInstance.RootScene.Entities.First(e => e.Name == "Ally2Position");
            var Ally3Position = SceneSystem.SceneInstance.RootScene.Entities.First(e => e.Name == "Ally3Position");
            var Ally4Position = SceneSystem.SceneInstance.RootScene.Entities.First(e => e.Name == "Ally4Position");

            var playerCards = new List<BattleCard>()
            {
                new BattleCard(new Julius()) { Position = Ally1Position.Transform.Position, CharacterSpriteIndex = 0},
                new BattleCard(new Thor()){ Position = Ally2Position.Transform.Position, CharacterSpriteIndex = 1},
                new BattleCard(new Slime()){ Position = Ally3Position.Transform.Position, CharacterSpriteIndex = 2}
            };

            var enemyCards = new List<BattleCard>()
            {
                new BattleCard(new Golem()){ Position = Enemy1Position.Transform.Position , CharacterSpriteIndex = 3},
                new BattleCard(new Slime()){ Position = Enemy2Position.Transform.Position, CharacterSpriteIndex = 4},
                new BattleCard(new Slime()){ Position = Enemy3Position.Transform.Position, CharacterSpriteIndex = 4}
            };
            
            Entity.Add(new UIComponent()
            {
                Page = BattleUI
            });
            
            BattleManager.Instance.SetGameTime(Game.UpdateTime)
                .SetContentManager(Content)
                .SetSceneSystem(SceneSystem)
                .SetBattleUI(BattleUI)
                .Start(playerCards, enemyCards);
        }

        public override void Update()
        {
            BattleManager.Instance.Update();
            if(BattleManager.Instance.BattleState == BattleState.Loading)
            {
                BattleManager.Instance.Loaded();
            }
        }

        public override void Cancel()
        {
            BattleManager.Instance.Dispose();

            base.Cancel();
        }
    }
}
