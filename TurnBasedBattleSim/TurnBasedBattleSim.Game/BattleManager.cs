using System;
using System.Collections.Generic;
using System.Linq;
using TurnBasedBattleSim.Game;
using Stride.Core.Mathematics;
using Stride.Core.Serialization.Contents;
using Stride.Engine;
using Stride.Games;
using Stride.UI;
using Stride.UI.Controls;

namespace TurnBasedBattleSim
{
    public class BattleManager : IDisposable
    {
        private static BattleManager _instance;

        public static BattleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattleManager();
                }

                return _instance;
            }
        }

        public BattleState BattleState { get; private set; } = BattleState.Start;
        public BattleMode BattleMode { get; private set; } = BattleMode.Manual;

        public bool IsBattleOver => BattleState == BattleState.Win || BattleState == BattleState.Lose;

        private List<BattleCard> PlayerCards = new List<BattleCard>();
        private List<BattleCard> EnemyCards = new List<BattleCard>();
        public List<BattleCard> BattleCards = new List<BattleCard>();

        public BattleCardController PlayerController { get; private set; }
        public BattleCardController EnemyController { get; private set; }

        public GameTime GameTime { get; private set; }
        public UIPage BattleUI { get; private set; }
        public ContentManager ContentManager { get; set; }
        public SceneSystem SceneSystem;

        public int MaxTurnBar
        {
            get
            {
                return GetCurrentTurnBattleCard().TurnBar;
            }
        }

        private Prefab BattleCardPrefab = null;

        public BattleManager()
        {
        }

        public BattleManager SetGameTime(GameTime gameTime)
        {
            GameTime = gameTime;

            return this;
        }

        public BattleManager SetBattleUI(UIPage battleUI)
        {
            BattleUI = battleUI;

            return this;
        }

        public BattleManager SetContentManager(ContentManager contentManager)
        {
            ContentManager = contentManager;

            return this;
        }

        public BattleManager SetSceneSystem(SceneSystem sceneSystem)
        {
            SceneSystem = sceneSystem;

            return this;
        }

        private void AddBattleCards(List<BattleCard> battleCards)
        {
            foreach (var battleCard in battleCards)
            {
                var battleCardEntity = BattleCardPrefab.Instantiate()[0];

                battleCardEntity.Transform.Position = battleCard.Position;

                var script = (BattleCardScript)battleCardEntity.Get<ScriptComponent>();

                //script.Start();

                script.CharacterSpriteIndex = battleCard.CharacterSpriteIndex;

                battleCard.SetEntity(battleCardEntity);

                battleCard.SetScript(script);

                BattleCards.Add(battleCard);

                SceneSystem.SceneInstance.RootScene.Entities.Add(battleCardEntity);

                battleCard.OnDeath += KillCard;

                battleCard.OnBattleStart();
            }
        }

        private void RemoveBattleCards(List<BattleCard> battleCards)
        {
            battleCards.ForEach(battleCard =>
            {
                if (PlayerCards.Contains(battleCard))
                {
                    PlayerCards.Remove(battleCard);
                }
                if (EnemyCards.Contains(battleCard))
                {
                    EnemyCards.Remove(battleCard);
                }

                battleCard.OnDeath -= KillCard;
            });

            battleCards.Clear();
        }

        private void KillCard(BattleCard battleCard)
        {
            battleCard.DisableEntity();
        }

        private void AddPlayerCards(List<BattleCard> playerCards)
        {
            PlayerCards = playerCards;

            var teamTag = Guid.NewGuid();

            foreach (var playerCard in PlayerCards)
            {
                playerCard.Controller = PlayerController;
                playerCard.TeamTag = teamTag;
            }

            AddBattleCards(PlayerCards);
        }

        private void AddEnemyCards(List<BattleCard> enemyCards)
        {
            EnemyCards = enemyCards;

            var teamTag = Guid.NewGuid();

            foreach (var enemyCard in EnemyCards)
            {
                enemyCard.Controller = EnemyController;
                enemyCard.TeamTag = teamTag;
            }

            AddBattleCards(EnemyCards);
        }

        private BattleCard GetCurrentTurnBattleCard()
        {
            var currentTurnBattleCard = BattleCards.Where(battleCard => !battleCard.IsDead).OrderByDescending(battleCard => battleCard.TurnBar).First();

            return currentTurnBattleCard;
        }

        private void SetupBattleUI()
        {

        }

        public void Loaded()
        {
            BattleState = BattleState.TurnEnd;
        }

        public void Start(List<BattleCard> playerCards, List<BattleCard> enemyCards)
        {
            BattleCardPrefab = ContentManager.Load<Prefab>("BattleCard");

            PlayerController = new PlayerBattleCardController(BattleUI);

            EnemyController = new AIBattleCardController();

            SetupBattleUI();

            BattleState = BattleState.Start;

            AddPlayerCards(playerCards);

            AddEnemyCards(enemyCards);

            BattleState = BattleState.Loading;
        }

        public void StartTurn()
        {
            if (IsBattleOver)
            {
                return;
            }

            if (BattleState == BattleState.TurnEnd)
            {
                UpdateTurnBars();
            }

            var battleCard = GetCurrentTurnBattleCard();

            BattleState = BattleState.Combat;

            battleCard.StartTurn();
        }

        public void EndTurn()
        {
            if (IsBattleOver)
            {
                return;
            }

            BattleState = BattleState.TurnEnd;

            UpdateBattleState();
        }

        private void UpdateTurnBars()
        {
            BattleCards.Where(battleCard => !battleCard.IsDead).ToList().ForEach(battleCard =>
            {
                battleCard.AdvanceTurnBar();
            });
        }

        public void EndBattle(bool playerWon)
        {
            BattleState = playerWon ? BattleState.Win : BattleState.Lose;

            GetCurrentTurnBattleCard()?.EndTurn();

            BattleCards.ForEach(battleCard =>
            {
                battleCard.OnBattleEnd();
            });
        }

        private void UpdateBattleState()
        {
            if (IsBattleOver)
            {
                return;
            }

            if (PlayerCards.Count == 0 || !PlayerCards.Any(playerCard => playerCard.IsDead == false))
            {
                EndBattle(false);
            }

            if (EnemyCards.Count == 0 || !EnemyCards.Any(enemyCard => enemyCard.IsDead == false))
            {
                EndBattle(true);
            }
        }

        public void Update()
        {
            if (BattleState == BattleState.TurnEnd)
            {
                StartTurn();
            }

            if (!IsBattleOver && BattleState != BattleState.TurnEnd && BattleState != BattleState.Loading)
            {
                var currentTurnBattleCard = GetCurrentTurnBattleCard();

                currentTurnBattleCard?.TurnUpdate();
            }
        }

        public void Dispose()
        {
            RemoveBattleCards(BattleCards);

            PlayerController.Dispose();
            EnemyController.Dispose();

            _instance = null;
        }
    }
}
