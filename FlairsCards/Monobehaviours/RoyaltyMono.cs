﻿using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnboundLib.GameModes;
using UnityEngine;

namespace FlairsCards.MonoBehaviours
{
    class RoyaltyMono : MonoBehaviour
    {
        private Player player;
        private Gun gun;
        private int playerTeamID;
        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
            gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            playerTeamID = player.teamID;
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        IEnumerator RoundEnd(IGameModeHandler gm)
        {
            int[] roundWinners = gm.GetRoundWinners();
            bool isWinner = roundWinners.Contains(playerTeamID);

            if (isWinner)
            {
                gun.damage += 0.15f;
            }

            yield break;
        }
    }
}