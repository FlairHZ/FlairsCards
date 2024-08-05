using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModdingUtils.Extensions;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Utils;
using UnityEngine;
using WillsWackyManagers;
using WillsWackyManagers.Utils;


namespace FlairsCards.Cards
{
    class AllOrNothing : CustomCard
    {
        public static CardCategory[] noLotteryCategories = new CardCategory[] { CardChoiceSpawnUniqueCardPatch.CustomCategories.CustomCardCategories.instance.CardCategory("CardManipulation"), CardChoiceSpawnUniqueCardPatch.CustomCategories.CustomCardCategories.instance.CardCategory("NoRandom") };
        CardInfo chosenCard;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { CardChoiceSpawnUniqueCardPatch.CustomCategories.CustomCardCategories.instance.CardCategory("CardGamba") };
            chosenCard = cardInfo; 
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CardInfo randomDraw = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, this.condition);
            if (randomDraw == null)
            {
                // if there is no valid card, then try drawing from the list of all cards (inactive + active) but still make sure it is compatible
                CardInfo[] allCards = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null)).ToList().Concat((List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null)).ToArray();
                randomDraw = ModdingUtils.Utils.Cards.instance.DrawRandomCardWithCondition(allCards, player, null, null, null, null, null, null, null, this.condition);
            }

            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, randomDraw, addToCardBar: true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, randomDraw, 0f);
            ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, chosenCard, ModdingUtils.Utils.Cards.SelectionType.Newest);
    }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }
        protected override string GetTitle()
        {
            return "All Or Nothing";
        }
        protected override string GetDescription()
        {
            return "Draw a <color=#ff000fff>random</color> card";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }

        public bool condition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            return !card.categories.Intersect(AllOrNothing.noLotteryCategories).Any();

        }
        public override string GetModName()
        {
            return FlairsCards.ModInitials;
        }
    }
}