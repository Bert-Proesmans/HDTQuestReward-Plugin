using HearthDb;
using HearthDb.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using CollCards = HearthDb.CardIds.Collectible;
using NonCollCards = HearthDb.CardIds.NonCollectible;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDTQuestReward_Plugin
{
    public partial class PluginLogic
    {
        private bool IsQuestCard(HDTCard c)
        {
            if (!c.Collectible || c.CardSet == null || !c.CardSet.Equals(CardSet.UNGORO))
            {
                return false;
            }

            // Loop through all entities to find the passed down card
            var entity = CoreAPI.Game.Entities.Select(kv => kv.Value).Where(e => e.Card.Equals(c)).FirstOrDefault();

            if (entity == null || !entity.IsQuest)
            {
                return false;
            }

            return true;
        }

        private bool IsQuestCardV2(HDTCard c)
        {
            switch (c.Id)
            {
                case CollCards.Druid.JungleGiants:
                case CollCards.Hunter.TheMarshQueen:
                case CollCards.Mage.OpenTheWaygate:
                case CollCards.Paladin.TheLastKaleidosaur:
                case CollCards.Priest.AwakenTheMakers:
                case CollCards.Rogue.TheCavernsBelow:
                case CollCards.Shaman.UniteTheMurlocs:
                case CollCards.Warlock.LakkariSacrifice:
                case CollCards.Warrior.FirePlumesHeart:
                    return true;

                default:
                    return false;
            }
        }

        private HDTCard GetQuestReward(HDTCard questCard)
        {
            HDTCard rValue = null;


            switch (questCard.Id)
            {
                case CollCards.Druid.JungleGiants:
                    rValue = Database.GetCardFromId(NonCollCards.Druid.JungleGiants_BarnabusTheStomperToken);
                    break;

                case CollCards.Hunter.TheMarshQueen:
                    rValue = Database.GetCardFromId(NonCollCards.Hunter.TheMarshQueen_QueenCarnassaToken);
                    break;

                case CollCards.Mage.OpenTheWaygate:
                    rValue = Database.GetCardFromId(NonCollCards.Mage.OpentheWaygate_TimeWarpToken);
                    break;

                case CollCards.Paladin.TheLastKaleidosaur:
                    rValue = Database.GetCardFromId(NonCollCards.Paladin.TheLastKaleidosaur_GalvadonToken);
                    break;

                case CollCards.Priest.AwakenTheMakers:
                    rValue = Database.GetCardFromId(NonCollCards.Priest.AwakentheMakers_AmaraWardenOfHopeToken);
                    break;

                case CollCards.Rogue.TheCavernsBelow:
                    rValue = Database.GetCardFromId(NonCollCards.Rogue.TheCavernsBelow_CrystalCoreToken);
                    break;

                case CollCards.Shaman.UniteTheMurlocs:
                    rValue = Database.GetCardFromId(NonCollCards.Shaman.UnitetheMurlocs_MegafinToken);
                    break;

                case CollCards.Warlock.LakkariSacrifice:
                    // There are two tokens;
                    // Possibly! the first is the card to play.
                    // Possibly! the second is the card on board.
                    rValue = Database.GetCardFromId(NonCollCards.Warlock.LakkariSacrifice_NetherPortalToken1);
                    break;

                case CollCards.Warrior.FirePlumesHeart:
                    rValue = Database.GetCardFromId(NonCollCards.Warrior.FirePlumesHeart_SulfurasToken);
                    break;
            }

            return rValue;
        }
    }
}
