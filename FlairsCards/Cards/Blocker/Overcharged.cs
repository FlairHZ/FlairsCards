﻿using ClassesManagerReborn.Util;
using FlairsCards.MonoBehaviours;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace FlairsCards.Cards
{
    class Overcharged : CustomCard
    {
        internal static CardInfo Card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = BlockerClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<OverchargedMono>();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Destroy(player.gameObject.GetOrAddComponent<OverchargedMono>());
        }
        protected override string GetTitle()
        {
            return "Overcharged";
        }
        protected override string GetDescription()
        {
            return "Gain increasing block cooldown as your health decreases. Max at 1% HP.";
        }
        protected override GameObject GetCardArt()
        {
            return FlairsCards.CardArtOvercharged;
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
                    stat = "Block Cooldown",
                    amount = "Up to -75%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
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