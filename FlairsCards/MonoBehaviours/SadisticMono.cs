using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;
using ModdingUtils.GameModes;

namespace FlairsCards.MonoBehaviours
{
    class SadisticMono : MonoBehaviour
    {
        private Player player;
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
            player.data.stats.movementSpeed = (float)(1.0 + (player.data.stats.GetAdditionalData().curses * 0.2));

            yield break;
        }
    }
}