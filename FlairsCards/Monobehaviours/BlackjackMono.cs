using FC.Extensions;
using System.Collections;
using System.Linq;
using UnboundLib.GameModes;
using UnityEngine;

namespace FlairsCards.MonoBehaviours
{
    class BlackjackMono : MonoBehaviour
    {
        private Player player; 
        private Gun gun;
        private GunAmmo gunAmmo;
        private CharacterData data;
        private HealthHandler health;
        private Gravity gravity;
        private Block block;
        private CharacterStatModifiers characterStats;
        public int dealerHand;
        public int playerHand; 
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
            dealerHand = 0;
            playerHand = 15;
            var playerNumber = UnityEngine.Random.Range(-2 + player.data.stats.GetAdditionalData().luck, player.data.stats.GetAdditionalData().luck + 1);
            playerHand += playerNumber;

            // Players hand is above 21, auto lose without need to check for dealer
            if (playerHand > 21 && player.data.stats.GetAdditionalData().curseAverse == true)
            {
                playerHand = 17;
            }
            else if (playerHand > 21)
            {
                gun.damage -= 0.1f;
            }
            // Players hand isn't above 21, need to check what dealer gets (draw to 17)
            else
            {
                while (dealerHand < 17)
                {
                    var dealerNumber = UnityEngine.Random.Range(1, 12);
                    dealerHand += dealerNumber;
                }
                if (dealerHand > 21)
                {
                    gun.damage += 0.2f;
                }
                else if (dealerHand > playerHand)
                {
                    gun.damage -= 0.1f;
                }
                else
                {
                    gun.damage += 0.2f;
                }
            }

            yield break;
        }
    }
}
