using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;

namespace FlairsCards.MonoBehaviours
{
    class CoinflipMono : MonoBehaviour
    {
        private Player player;
        int luck;
        private void Start()
        {
            player = GetComponentInParent<Player>();
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        IEnumerator RoundEnd(IGameModeHandler gm)
        {
            if (player.data.stats.GetAdditionalData().curseAverse == true)
            {
                luck = 0;
            }
            else
            {
                luck = UnityEngine.Random.Range(0, 2);
            }

            if (luck == 0)
            {
                player.data.stats.GetAdditionalData().luck += 1;
            }
            else
            {
                player.data.stats.GetAdditionalData().luck -= 1;
            }

            yield break;
        }
    }
}
