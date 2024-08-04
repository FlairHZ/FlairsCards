using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModdingUtils.Extensions;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Utils;
using UnityEngine;
using WillsWackyManagers.Utils;


namespace FlairsCards.Cards
{
    class HeavenlyDraw : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var draw = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, DrawCondition);
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, draw, false, "", 2f, 2f, true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, draw, 3f);
            CurseManager.instance.CursePlayer(player, (curse) => {
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
            });
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }
        protected override string GetTitle()
        {
            return "Heavenly Draw";
        }
        protected override string GetDescription()
        {
            return "Draw a card above <color=#00000fff>rare</color> rarity, also draw a <color=#ff000fff>curse</color>";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Legendary");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public override string GetModName()
        {
            return FlairsCards.ModInitials;
        }
        private bool DrawCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity != CardInfo.Rarity.Common && card.cardName != "HeavenlyDraw" && card.rarity != CardInfo.Rarity.Uncommon && card.rarity != CardInfo.Rarity.Rare;
        }
    }
}
