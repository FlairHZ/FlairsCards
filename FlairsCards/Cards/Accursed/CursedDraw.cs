using ClassesManagerReborn.Util;
using FC.Extensions;
using FlairsCards.Utilities;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using WillsWackyManagers.Utils;

namespace FlairsCards.Cards
{
    class CursedDraw : CustomCard
    {
        CardInfo chosenCard;
        internal static CardInfo Card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = AccursedClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            chosenCard = cardInfo;
            FCDebug.Log($"[{FlairsCards.ModInitials}][Card] {GetTitle()} has been setup.");
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var common = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CommonCondition);
            var common2 = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CommonCondition);
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, common, false, "", 2f, 2f, true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, common, 3f);
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, common2, false, "", 2f, 2f, true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, common2, 3f);
            CurseManager.instance.CursePlayer(player, (curse) => {
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
            });
            ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, chosenCard, ModdingUtils.Utils.Cards.SelectionType.Newest);
            player.data.stats.GetAdditionalData().curses += 1;
            FCDebug.Log($"[{FlairsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            FCDebug.Log($"[{FlairsCards.ModInitials}][Card] {GetTitle()} has been removed to player {player.playerID}.");
        }
        protected override string GetTitle()
        {
            return "Cursed Draw";
        }
        protected override string GetDescription()
        {
            return "Draw two common non-class cards, also draw a <color=#ff000fff>curse</color>";
        }
        protected override GameObject GetCardArt()
        {
            return FlairsCards.CardArtCursedDraw;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("CommonClass");
        }
        protected override CardInfoStat[] GetStats()
        {
            return null;
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
        public override string GetModName()
        {
            return FlairsCards.ModInitials;
        }
        private bool CommonCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == CardInfo.Rarity.Common && card.cardName != "Cursed Draw";
        }
    }
}