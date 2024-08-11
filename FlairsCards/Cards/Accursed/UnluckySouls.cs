﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesManagerReborn.Util;
using FC.Extensions;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using WillsWackyManagers.Utils;

namespace FlairsCards.Cards
{
    class UnluckySouls : CustomCard
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
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            for (int i = 0; i <= 1; i++)
            {
                var randomPlayer = UnityEngine.Random.Range(0, PlayerManager.instance.players.Count);
                var chosenPlayer = PlayerManager.instance.players[randomPlayer];
                chosenPlayer.data.stats.GetAdditionalData().curses += 1;
                CurseManager.instance.CursePlayer(chosenPlayer, (curse) => { ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(chosenPlayer, curse); });
            }

            ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, chosenCard, ModdingUtils.Utils.Cards.SelectionType.Newest);
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }

        protected override string GetTitle()
        {
            return "Unlucky Souls";
        }
        protected override string GetDescription()
        {
            return "Choose two random players to get a curse, including you";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("RareClass");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
        public override string GetModName()
        {
            return FlairsCards.ModInitials;
        }
    }
}