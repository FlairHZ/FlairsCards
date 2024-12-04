using ClassesManagerReborn.Util;
using FlairsCards.MonoBehaviours;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FlairsCards.Cards
{
    class Coinflip : CustomCard
    {
        internal static CardInfo Card = null;

        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = GamblerClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            statModifiers.health = 1.25f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<CoinflipMono>();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Destroy(player.gameObject.GetOrAddComponent<CoinflipMono>());
        }
        protected override string GetTitle()
        {
            return "Coinflip";
        }
        protected override string GetDescription()
        {
            return "50/50 odds of changing your luck and projectile speed at the end of each round";
        }
        protected override GameObject GetCardArt()
        {
            return FlairsCards.CardArtCoinflip;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("CommonClass");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Projectile Speed",
                    amount = "±25%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Luck",
                    amount = "±1",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
            };
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