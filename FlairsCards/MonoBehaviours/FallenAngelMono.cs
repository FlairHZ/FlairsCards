using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;

namespace FlairsCards.MonoBehaviours
{
    class FallenAngelMono : MonoBehaviour
    {
        private Player player;
        private void Start()
        {
            player = GetComponentInParent<Player>();
            GameModeManager.AddHook(GameModeHooks.HookBattleStart, BattleStart);
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookBattleStart, BattleStart);
        }

        IEnumerator BattleStart(IGameModeHandler gm)
        {
            player.data.stats.respawns = (int)(player.data.stats.GetAdditionalData().curses / 4);

            yield break;
        }
    }
}