using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenko.Core.Mathematics;
using Xenko.Input;
using Xenko.Engine;
using Xenko.Rendering.Sprites;
using Xenko.UI.Controls;
using Xenko.UI;
using Xenko.UI.Panels;
using Xenko.UI.Events;

namespace TurnBasedBattleSim.Game
{
    public class BattleCardScript : SyncScript
    {
        public int CharacterSpriteIndex { get; set; }

        public UIPage BattleCardUI { get; set; }

        public delegate void SelectCard();

        public event SelectCard OnSelectCard = null;

        private ImageElement HpBar;
        private ImageElement TurnBar;
        private ImageElement TargetSelector;
        private ImageElement TurnIndicator;
        private Canvas CardContainer;
        private Button CardSelector;

        private float HpPercentage = 100;
        private float TurnBarPercentage = 100;

        //private static readonly RoutedEvent<RoutedEventArgs> ClickEvent = EventManager.RegisterRoutedEvent<RoutedEventArgs>("Click", RoutingStrategy.Bubble, typeof(Canvas));

        /*  
      private event EventHandler<RoutedEventArgs> Click
      {
          add { CardContainer.AddHandler(ClickEvent, value); }
          remove { CardContainer.RemoveHandler(ClickEvent, value); }
      }
      */

        public void HideUI()
        {
            BattleCardUI.RootElement.Visibility = Visibility.Hidden;
        }

        public void ShowUI()
        {
            BattleCardUI.RootElement.Visibility = Visibility.Visible;
        }

        public override void Start()
        {
            var sprite = (SpriteFromSheet)Entity.Get<SpriteComponent>().SpriteProvider;

            sprite.CurrentFrame = CharacterSpriteIndex;

            CardContainer = BattleCardUI.RootElement.FindVisualChildOfType<Canvas>("CardContainer");

            CardSelector = BattleCardUI.RootElement.FindVisualChildOfType<Button>("CardSelector");

            //var routedEvent = EventManager.RegisterRoutedEvent<RoutedEventArgs>("ClickCard", RoutingStrategy.Tunnel, typeof(Canvas));

            //RoutedEvent<RoutedEventArgs> routedEvent = EventManager.GetRoutedEvent(typeof(Canvas), "ClickCard");

            //CardContainer.AddHandler<RoutedEventArgs>(ButtonBase.ClickEvent, OnClickCardContainer, true);

            CardSelector.Click += delegate { OnClickCardContainer(); };

            HpBar = BattleCardUI.RootElement.FindVisualChildOfType<ImageElement>("HpBar");

            TurnBar = BattleCardUI.RootElement.FindVisualChildOfType<ImageElement>("TurnBar");

            TargetSelector = BattleCardUI.RootElement.FindVisualChildOfType<ImageElement>("TargetSelector");

            TurnIndicator = BattleCardUI.RootElement.FindVisualChildOfType<ImageElement>("TurnIndicator");

            HideTargetSelector();
            HideTurnIndicator();
        }

        public void SetHpPercentage(float percentage)
        {
            HpPercentage = percentage;
        }

        public void SetTurnBarPercentage(float percentage)
        {
            //TurnBarPercentage = MathUtil.Lerp(TurnBarPercentage, percentage, 2);
            TurnBarPercentage = percentage;
        }

        static float MoveTowards(float a, float b, float rate)
        {
            float diff = b - a;
            if (Math.Abs(rate) < Math.Abs(diff))
                return a + Math.Sign(diff) * rate;

            return b;
        }

        private void UpdateHpBar()
        {
            var value = MathUtil.Clamp(HpPercentage / 100 * CardContainer.ActualWidth, 0, CardContainer.ActualWidth);

            //HpBar.Width = MathUtil.Lerp(HpBar.Width, value, 0.05f);
            HpBar.Width = MoveTowards(HpBar.Width, value, 0.5f);
        }

        private void UpdateTurnBar()
        {
            var value = MathUtil.Clamp(TurnBarPercentage / 100 * CardContainer.ActualWidth, 0, CardContainer.ActualWidth);

            //TurnBar.Width = MathUtil.Lerp(TurnBar.Width, value, 0.5f);
            //TurnBar.Width = MoveTowards(TurnBar.Width, value, 1f);
            TurnBar.Width = value;
        }

        public void HideTargetSelector()
        {
            if (TargetSelector != null)
                TargetSelector.Visibility = Visibility.Hidden;
        }

        public void ShowTargetSelector()
        {
            if (TargetSelector != null)
                TargetSelector.Visibility = Visibility.Visible;
        }

        public void HideTurnIndicator()
        {
            if (TurnIndicator != null)
                TurnIndicator.Visibility = Visibility.Hidden;
        }

        public void ShowTurnIndicator()
        {
            if (TurnIndicator != null)
                TurnIndicator.Visibility = Visibility.Visible;
        }

        public void HideHpBar()
        {
            if (HpBar != null)
                HpBar.Visibility = Visibility.Hidden;
        }

        private void OnClickCardContainer()
        {
            OnSelectCard?.Invoke();

            Script.AddTask(() => {
                return Task.CompletedTask;
            });
        }

        public override void Update()
        {
            UpdateHpBar();
            UpdateTurnBar();

            if (true)
            {
                //OnSelectCard?.Invoke();
            }
        }
    }
}
