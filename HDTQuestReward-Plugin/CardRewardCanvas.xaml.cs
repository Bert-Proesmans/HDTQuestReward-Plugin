using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace HDTQuestReward_Plugin
{
    /// <summary>
    /// Interaction logic for CardCanvas.xaml
    /// </summary>
    public partial class CardRewardCanvas : UserControl
    {
        public CardRewardCanvas()
        {
            InitializeComponent();
        }

        internal void Update(Card c)
        {
            Debug.WriteLine(c.ToString(), "HDTQUESTREWARD: Updating card: ");

            if (c == null)
            {
                Hide();
                return;
            }

            this.DataContext = c;

            UpdatePosition();
        }

        // Ripped from https://github.com/RedHatter/Graveyard/blob/master/src/Graveyard.cs#L35
        internal void UpdatePosition()
        {
            var border = Hearthstone_Deck_Tracker.API.Core.OverlayCanvas.FindName("BorderStackPanelOpponent") as Border;
            DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border)).AddValueChanged(border, Layout);
            DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border)).AddValueChanged(border, Layout);
            DependencyPropertyDescriptor.FromProperty(ActualWidthProperty, typeof(StackPanel)).AddValueChanged(this, Layout);
        }

        // Remove all event handlers when this component is disposed..
        public void Dispose()
        {
            var border = Hearthstone_Deck_Tracker.API.Core.OverlayCanvas.FindName("BorderStackPanelOpponent") as Border;
            DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border)).RemoveValueChanged(border, Layout);
            DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border)).RemoveValueChanged(border, Layout);
            DependencyPropertyDescriptor.FromProperty(ActualWidthProperty, typeof(StackPanel)).RemoveValueChanged(this, Layout);
        }

        private void Layout(object obj, EventArgs e)
        {
            var border = Hearthstone_Deck_Tracker.API.Core.OverlayCanvas.FindName("BorderStackPanelOpponent") as Border;
            Canvas.SetLeft(this, Canvas.GetLeft(border) + border.ActualWidth * Config.Instance.OverlayOpponentScaling / 100 + 10);
            Canvas.SetTop(this, Canvas.GetTop(border) + border.ActualHeight);
        }

        internal void Hide()
        {
            this.Visibility = Visibility.Hidden;
        }

        internal void Show()
        {
            this.Visibility = Visibility.Visible;
        }
    }
}
