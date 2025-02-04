﻿using ClassesManagerReborn.Util;
using RarityLib.Utils;
using UnboundLib;
using ModsPlus;
using FlairsCards.Utilities;

namespace FlairsCards.Cards
{
    public class TerminalVelocity : CustomEffectCard<SpeedOnBlockEffect>
    {
        internal static CardInfo Card = null; 
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = BlockerClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Terminal Velocity",
            Description = "Gain movement speed on block",
            ModName = FlairsCards.ModInitials,
            Rarity = RarityUtils.GetRarity("CommonClass"),
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Art = FlairsCards.CardArtTerminalVelocity,
            Stats = new[]
            {
                new CardInfoStat()
                {
                    positive = true,                    
                    stat = "Movement speed per block",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf,
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Block Cooldown",
                    amount = "+0.25s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf,
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            FCDebug.Log($"[{FlairsCards.ModInitials}][Card] {GetTitle()} has been setup.");
        }
    }
}
public class SpeedOnBlockEffect : CardEffect
{
    public override void OnBlockRecharge()
    {
        StatManager.Apply(player, new StatChanges
        {
            MovementSpeed = 1.1f
        });
    }
}