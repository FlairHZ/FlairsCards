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
        private CardInfo previousCard;  // Field to track the previous card
        private CardInfo cursedCard;     // Field to track the cursed card
        int chance;

        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
            gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            gunAmmo = gun.GetComponentInChildren<GunAmmo>();
            data = player.GetComponent<CharacterData>();
            health = player.GetComponent<HealthHandler>();
            gravity = player.GetComponent<Gravity>();
            block = player.GetComponent<Block>();
            characterStats = player.GetComponent<CharacterStatModifiers>();
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        IEnumerator RoundEnd(IGameModeHandler gm)
        {
            // Remove the previous card from the player
            if (previousCard != null)
            {
                ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, previousCard, ModdingUtils.Utils.Cards.SelectionType.Newest);
            }

            // Remove the cursed card from the player, if applicable
            if (cursedCard != null)
            {
                ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, cursedCard, ModdingUtils.Utils.Cards.SelectionType.Newest);
                cursedCard = null;  // Reset the cursed card after removal
            }

            yield break;
        }

        IEnumerator PickEnd(IGameModeHandler gm)
        {
            // Draw a new card for this turn
            if (player.data.stats.GetAdditionalData().curseAverse == true)
            {
                chance = UnityEngine.Random.Range(player.data.stats.GetAdditionalData().luck, player.data.stats.GetAdditionalData().luck + 1);
            }
            else
            {
                chance = UnityEngine.Random.Range(-2 + player.data.stats.GetAdditionalData().luck, player.data.stats.GetAdditionalData().luck);
            }
            CardInfo newCard = null;

            if (chance < 0)
            {
                newCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CursedCondition);
                CurseManager.instance.CursePlayer(player, (curse) => { ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, newCard); });

                // Track the cursed card separately
                cursedCard = newCard;
            }
            else if (chance == 0)
            {
                newCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, NeutralNameCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, newCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, newCard, 0f);
            }
            else if (chance > 0 && chance <= 1)
            {
                newCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CommonCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, newCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, newCard, 0f);
            }
            else if (chance >= 2 && chance <= 3)
            {
                newCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, UncommonCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, newCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, newCard, 0f);
            }
            else if (chance >= 4 && chance <= 5)
            {
                newCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, RareCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, newCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, newCard, 0f);
            }
            else
            {
                newCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, ExcellentCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, newCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, newCard, 0f);
            }

            previousCard = newCard;  // Update the previous card to the newly drawn card

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
        private bool CommonNameCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.cardName == "Small Blind";
        }
        private bool UncommonNameCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.cardName == "Big Blind";
        }
        private bool RareNameCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.cardName == "Jackpot";
        }
        private bool ExcellentNameCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.cardName == "777";
        }

        private bool CommonCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == CardInfo.Rarity.Common;
        }
        private bool UncommonCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == CardInfo.Rarity.Uncommon;
        }
        private bool RareCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == CardInfo.Rarity.Rare;
        }
        private bool ExcellentCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity != CardInfo.Rarity.Common || card.rarity != CardInfo.Rarity.Uncommon || card.rarity != CardInfo.Rarity.Rare;
        }
    }
}