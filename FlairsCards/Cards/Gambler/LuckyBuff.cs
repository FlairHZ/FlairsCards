using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesManagerReborn.Util;
using ModdingUtils.Extensions;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;


namespace FlairsCards.Cards
{
    class LuckyBuff : CustomCard
    {
        internal static CardInfo Card = null;

        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = GamblerClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }

        protected override string GetTitle()
        {
            return "Lucky Buff";
        }
        protected override string GetDescription()
        {
            return "Roll a random permanent buff or debuff with its strength depending on your luck each turn";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "???",
                    amount = "b?ff",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "???",
                    amount = "i??re?se",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }
        public override string GetModName()
        {
            return FlairsCards.ModInitials;
        }
    }
}