using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FlairsCards.MonoBehaviours
{
    class BlackjackMono : MonoBehaviour
    {
        private Player player;
        int dealerHand;
        int playerHand;
        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
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
            if (playerHand > 21)
            {
                yield return null;
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
                    yield return null;
                }
                else if (dealerHand > playerHand)
                {
                    yield return null;
                }
                else
                {

                }
            }

            yield break;
        }
    }
}
