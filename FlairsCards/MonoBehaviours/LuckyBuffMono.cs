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
    class LuckyBuffMono : MonoBehaviour
    {
        private Player player;
        private Gun gun;
        private GunAmmo gunAmmo;
        private int playerTeamID;
        int num;
        int chance;
        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
            gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            gunAmmo = gun.GetComponentInChildren<GunAmmo>();
            playerTeamID = player.teamID;
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        IEnumerator PickEnd(IGameModeHandler gm)
        {
            if (player.data.stats.GetAdditionalData().curseAverse == true)
            {
                System.Random rndPos = new System.Random();
                num = rndPos.Next(1, 5);
                chance = UnityEngine.Random.Range(player.data.stats.GetAdditionalData().luck, player.data.stats.GetAdditionalData().luck);
            }
            else
            {
                System.Random rndNeu = new System.Random();
                num = rndNeu.Next(1, 5);
                chance = UnityEngine.Random.Range(-2 + player.data.stats.GetAdditionalData().luck, player.data.stats.GetAdditionalData().luck + 1);
            }


            if (num == 1)
            {
                gunAmmo.maxAmmo += chance;
            }
            else if (num == 2)
            {
                gun.damage += (float)(1.0 + chance * 0.1);
            }
            else if (num == 3)
            {
                player.data.stats.movementSpeed += (float)(chance * 0.1);
            }
            else
            {
                player.data.stats.gravity += (float)(chance * 0.1);
            }

            yield break;
        }
    }
}
