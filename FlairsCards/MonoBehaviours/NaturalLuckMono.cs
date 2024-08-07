using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;

namespace FlairsCards.MonoBehaviours
{
    class NaturalLuckMono : MonoBehaviour
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


            yield break;
        }
    }
}