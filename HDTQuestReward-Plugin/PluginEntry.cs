using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;

using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace HDTQuestReward_Plugin
{
    public class PluginEntry : IPlugin
    {
        private CardRewardCanvas _canvas;
        private PluginLogic _pLogic;

        public void OnLoad()
        {
            Debug.WriteLine("HDTQUESTREWARD: OnLoad");
            //when it's loaded upon each restart/turned on by the user

            _canvas = new CardRewardCanvas();
            CoreAPI.OverlayCanvas.Children.Add(_canvas);

            _pLogic = new PluginLogic(_canvas);
            // All event handlers during the game..
            GameEvents.OnGameStart.Add(_pLogic.GameStart);
            GameEvents.OnPlayerHandMouseOver.Add(_pLogic.OnCardHover);
            GameEvents.OnPlayerMinionMouseOver.Add(_pLogic.OnCardHover);

            GameEvents.OnMouseOverOff.Add(_pLogic.ForceHide);
            GameEvents.OnInMenu.Add(_pLogic.ForceHide);

            // DeckManagerEvents.OnDeckSelected.Add(null);
        }

        public void OnUnload()
        {
            Debug.WriteLine("HDTQUESTREWARD: DESTROY");
            // handle unloading here. HDT does not literally unload the assembly
            CoreAPI.OverlayCanvas.Children.Remove(_canvas);

            _canvas = null;
            _pLogic = null;
        }

        public void OnButtonPress()
        {
            //when user presses the menu button
        }

        public void OnUpdate()
        {
            // called every ~100ms
        }

        public string Name => "QuestReward-Plugin";

        public string Description => "Shows the reward cards when hovering quest cards";

        public string ButtonText => "QuestReward Button text";

        public string Author => "BertP";

        public Version Version => new Version(0, 0, 1);

        public MenuItem MenuItem => null;

    }
}
