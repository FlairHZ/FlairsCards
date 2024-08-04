using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModdingUtils.Extensions;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;


namespace FlairsCards.Cards
{
    class BattleAngel : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            statModifiers.numberOfJumps += 1;
            statModifiers.movementSpeed = 1.2f;
            statModifiers.gravity = 0.7f;
            gun.numberOfProjectiles += 2;
            gun.spread = 1.05f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //
        }
        protected override string GetTitle()
        {
            return "Battle Angel";
        }
        protected override string GetDescription()
        {
            return "Angelic";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Masterwork");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Jumps",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Projectiles",
                    amount = "+2",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Gravity",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
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
