using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Diagnostics;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace HDTQuestReward_Plugin
{
    public partial class PluginLogic
    {
        private CardRewardCanvas _canvas;

        internal PluginLogic(CardRewardCanvas canvas)
        {
            _canvas = canvas;

            HideIfNecessary();
        }

        internal void ForceHide()
        {
            Debug.WriteLine("HDTQUESTREWARD: ForceHide called");
            _canvas.Hide();
        }

        internal void HideIfNecessary()
        {
            if (Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu)
            {
                _canvas.Hide();
            }
        }

        internal void GameStart()
        {
            Debug.WriteLine("HDTQUESTREWARD: OnGameStart called");
            // Do nothing
        }

        internal void OnCardHover(Card c)
        {
            Debug.WriteLine("HDTQUESTREWARD: OnCardHover called");
            // Don't perform expensive operations if we can say with this limited information that 
            if (!IsQuestCardV2(c))
            {
                _canvas.Hide();
                return;
            }

            Card rewardCard = GetQuestReward(c);
            _canvas.Update(rewardCard);
            _canvas.Show();
        }

        
    }
}
