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
        CardInfo previousCard;  // Field to track the previous card

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
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        IEnumerator PickEnd(IGameModeHandler gm)
        {
            // Remove the card chosen in the previous round if it exists
            if (previousCard != null)
            {
                ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, previousCard, ModdingUtils.Utils.Cards.SelectionType.Oldest);
                previousCard = null;
            }

            int chance = UnityEngine.Random.Range(-2 + player.data.stats.GetAdditionalData().luck, player.data.stats.GetAdditionalData().luck);
            if (chance < 0)
            {
                var cursedCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CursedCondition);
                CurseManager.instance.CursePlayer(player, (curse) => { ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, cursedCard); });
                var brokeCard = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CursedNameCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, brokeCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, brokeCard, 0f);
                previousCard = brokeCard;
            }
            else if (chance == 0)
            {
                var neutralCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, NeutralNameCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, neutralCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, neutralCard, 0f);
                previousCard = neutralCard;
            }
            else if (chance > 0 && chance <= 2)
            {
                var decentCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CommonUnCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, decentCard, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, decentCard, 0f);
                previousCard = decentCard;
            }
            else if (chance > 2 && chance <= 4)
            {
                // Logic for rare card
            }
            else
            {
                // Logic for very good cards
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
