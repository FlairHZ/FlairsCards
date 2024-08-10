﻿using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FlairsCards.MonoBehaviours
{
    class ArroganceMono : MonoBehaviour
    {
        private Player player;
        private Gun gun;
        private int playerTeamID;
        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
            gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            playerTeamID = player.teamID;
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, PointEnd);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPointEnd, PointEnd);
            GameModeManager.RemoveHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        IEnumerator RoundEnd(IGameModeHandler gm)
        {
            // Reset state if necessary
            yield break;
        }

        IEnumerator PointEnd(IGameModeHandler gm)
        {
            int[] roundWinners = gm.GetPointWinners();
            bool isWinner = roundWinners.Contains(playerTeamID);

            if (isWinner)
            {
                gun.damage += 0.05f;
                player.data.stats.movementSpeed += 0.05f;
            }
            else
            {
                gun.damage -= 0.075f;
                player.data.stats.movementSpeed -= 0.075f;
            }

            yield break;
        }
    }
}
