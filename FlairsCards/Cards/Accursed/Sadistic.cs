using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesManagerReborn.Util;
using FC.Extensions;
using FlairsCards.MonoBehaviours;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using WillsWackyManagers.Utils;


namespace FlairsCards.Cards
{
    class Sadistic : CustomCard
    {
        internal static CardInfo Card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = AccursedClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            float curseFactor = (float)(1 + characterStats.GetAdditionalData().curses * 0.15);
            statModifiers.movementSpeed = curseFactor; 
            statModifiers.health = curseFactor;
            characterStats.GetAdditionalData().curses = 0; 
            CurseManager.instance.RemoveAllCurses(player);
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }

        protected override string GetTitle()
        {
            return "Sadistic";
        }
        protected override string GetDescription()
        {
            return "Consume all curses you currently have, gain buffs depending on the amount destroyed";
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
                    stat = "Speed per curse",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }      ,         
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage per curse",
                    amount = "+15%",
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