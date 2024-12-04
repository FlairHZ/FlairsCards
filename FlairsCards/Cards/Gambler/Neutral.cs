using ClassesManagerReborn.Util;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FlairsCards.Cards
{
    class Neutral : CustomCard
    {
        internal static CardInfo Card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = GamblerClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //
        }
        protected override string GetTitle()
        {
            return "Neutral";
        }
        protected override string GetDescription()
        {
            return "Neither in the red nor black";
        }
        protected override GameObject GetCardArt()
        {
            return FlairsCards.CardArtNeutral;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Unobtainable");
        }
        protected override CardInfoStat[] GetStats()
        {
            return null;
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
        public override string GetModName()
        {
            return FlairsCards.ModInitials;
        }
    }
}