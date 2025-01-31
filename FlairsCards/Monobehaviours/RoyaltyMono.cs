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
        private float totalPoints;
        private List<TeamScore> currentScore = new List<TeamScore>();
        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
            gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPickEnd, PickEnd); 
            GameModeManager.RemoveHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }
        IEnumerator RoundEnd(IGameModeHandler gm)
        {
            totalPoints--;

            yield break;
        }


        IEnumerator PickEnd(IGameModeHandler gm)
        {
            currentScore.Clear();
            currentScore = PlayerManager.instance.players.Select(p => p.teamID).Distinct().Select(ID => GameModeManager.CurrentHandler.GetTeamScore(ID)).ToList();
            //totalPoints = 0;

            foreach (var score in currentScore)
            {
                totalPoints += score.rounds;
            }
            gun.damage += (float)(totalPoints * 0.05);

            yield break;
        }
    }
}