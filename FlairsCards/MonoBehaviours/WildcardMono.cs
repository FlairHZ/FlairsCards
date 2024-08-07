using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;
using System.Runtime.Remoting.Messaging;
using System.Collections.Generic;

namespace FlairsCards.MonoBehaviours
{
    class WildcardMono : MonoBehaviour
    {
        private Player player;
        private Gun gun;
        private GunAmmo gunAmmo;
        private CharacterData data;
        private HealthHandler health;
        private Gravity gravity;
        private Block block;
        private CharacterStatModifiers characterStats;
        CardInfo chosenCard;
        CardInfo oldCard;
        bool wait = false;
        private void Start()
        {
            player = GetComponentInParent<Player>();
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd); 
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        IEnumerator PickEnd(IGameModeHandler gm)
        {
            int chance = UnityEngine.Random.Range(-2 + player.data.stats.GetAdditionalData().luck, player.data.stats.GetAdditionalData().luck);
            oldCard = chosenCard;
            if (chance < 0)
            {
                var cursedCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CursedCondition);
                CurseManager.instance.CursePlayer(player, (curse) => { ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, cursedCard); });
                var brokeCard = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CursedNameCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, brokeCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, brokeCard, 0f);
                chosenCard = brokeCard;
                player.data.stats.GetAdditionalData().broke = true;
            }
            else if (chance == 0)
            {
                var neutralCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, NeutralNameCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, neutralCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, neutralCard, 0f);
                chosenCard = neutralCard;
            }
            else if (chance > 0 && chance <= 2)
            {
                var decentCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CommonUnCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, decentCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, decentCard, 0f);
                chosenCard = decentCard;
            }
            else if (chance > 2 && chance <= 4)
            {
                // Logic for rare card
            }
            else
            {
                // Logic for very good cards
            }

            // This if statement only gets run the second turn when wildcard gets set to true below this
            if (player.data.stats.GetAdditionalData().broke == true)
            {
                if (wait)
                {
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, chosenCard, ModdingUtils.Utils.Cards.SelectionType.Oldest);
                    player.data.stats.GetAdditionalData().broke = false;
                    wait = false;
                }
                else
                {
                    wait = true;
                }
                }
            else if (player.data.currentCards.Count > 3)
            {
                ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, oldCard, ModdingUtils.Utils.Cards.SelectionType.Oldest);
            }

            yield break;
        }
        private bool CursedCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return CurseManager.instance.IsCurse(card);
        }
        private bool CursedNameCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.cardName == "Broke";
        }
        private bool NeutralNameCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.cardName == "Neutral";
        }
        private bool CommonUnCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == CardInfo.Rarity.Common || card.rarity == CardInfo.Rarity.Uncommon;
        }
    }
}