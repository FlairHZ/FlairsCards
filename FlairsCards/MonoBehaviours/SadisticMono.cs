using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;

namespace FlairsCards.MonoBehaviours
{
    class SadisticMono : ReversibleEffect
    {
        public override void OnStart()
        {
            player = GetComponentInParent<Player>();
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        public override void OnOnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        IEnumerator PickEnd(IGameModeHandler gm)
        {
            this.characterStatModifiersModifier.movementSpeed_add = (float)(1.0 + (player.data.stats.GetAdditionalData().curses * 0.2));
            this.ApplyModifiers();

            yield break;
        }
    }
}