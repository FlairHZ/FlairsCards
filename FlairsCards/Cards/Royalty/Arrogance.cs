using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModdingUtils.Extensions;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using FlairsCards.MonoBehaviours;
using ClassesManagerReborn.Util;


namespace FlairsCards.Cards
{
    class Arrogance : CustomCard
    {
        internal static CardInfo Card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = RoyaltyClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<ArroganceMono>();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Destroy(player.gameObject.GetOrAddComponent<ArroganceMono>());
        }

        protected override string GetTitle()
        {
            return "Arrogance";
        }
        protected override string GetDescription()
        {
            return "Losing to peasants should never happen";
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
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage/Speed per point won",
                    amount = "+5%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },                
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage/Speed per point lost",
                    amount = "-7.5%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
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
    }
}