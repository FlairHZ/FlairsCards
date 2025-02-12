using System.Collections;
using System.Linq;
using UnboundLib.GameModes;
using UnityEngine;
using WillsWackyManagers.Utils;

namespace FlairsCards.MonoBehaviours
{
    class PlaguebearerMono : MonoBehaviour
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
            for (int i = 0; i < PlayerManager.instance.players.Count; i++)
            {
                var chosenPlayer = PlayerManager.instance.players[i];
                CurseManager.instance.CursePlayer(chosenPlayer, (curse) => { ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(chosenPlayer, curse); });
            }

            yield break;
        }
    }
}