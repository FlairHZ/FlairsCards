using FC.Extensions;
using ModdingUtils.MonoBehaviours;
using ModsPlus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.GameModes;
using UnityEngine;
using UnityEngine.UI;
using static ModdingUtils.Utils.SortingController;

namespace FlairsCards.Monobehaviours
{
    class GamblerMono : MonoBehaviour
    {
        private Player player;
        private CustomHealthBar shieldBar;
        private HealthHandler healthHandler;
        private void Start()
        {
            player = GetComponent<Player>();
            healthHandler = player.GetComponent<HealthHandler>();
            GameModeManager.AddHook(GameModeHooks.HookRoundStart, RoundStart);
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, PointEnd);
        }
        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookRoundStart, RoundStart);
            GameModeManager.RemoveHook(GameModeHooks.HookPointEnd, PointEnd);
        }
        IEnumerator PointEnd(IGameModeHandler gm)
        {
            Destroy(shieldBar.gameObject);
            yield break;
        }

        IEnumerator RoundStart(IGameModeHandler gm)
        {
            var parent = player.GetComponentInChildren<PlayerWobblePosition>().transform;
            var obj = new GameObject("Shield Bar");
            obj.transform.SetParent(parent);
            shieldBar = obj.AddComponent<CustomHealthBar>();
            shieldBar.transform.localPosition = Vector3.up * 0.25f;
            shieldBar.transform.localScale = Vector3.one;
            int luck = player.data.stats.GetAdditionalData().luck;
            Color shieldColor;
            if (luck <= -2) {
                shieldColor = Color.black;
                shieldBar.CurrentHealth = 0f;
            }
            else if (luck <= -1)
            {
                shieldColor = Color.red;
                shieldBar.CurrentHealth = 15f;
            }
            else if (luck == 0)
            {
                shieldColor = Color.yellow;
                shieldBar.CurrentHealth = 30f;
            }
            else if (luck == 1)
            {
                shieldColor = Color.cyan;
                shieldBar.CurrentHealth = 45f;
            }
            else if (luck == 2)
            {
                shieldColor = Color.blue;
                shieldBar.CurrentHealth = 60f;
            }
            else if (luck <= 4)
            {
                shieldColor = Color.green;
                shieldBar.CurrentHealth = 80f;
            }
            else
            {
                shieldColor = Color.white;
                shieldBar.CurrentHealth = 100f;
            }
            shieldBar.SetColor(shieldColor);
            yield break;
        }
    }
}
