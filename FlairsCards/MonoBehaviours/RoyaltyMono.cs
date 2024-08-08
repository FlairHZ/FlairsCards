using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;
using System.Collections.Generic;
using System.Linq;
using FlairsCards.MonoBehaviours;

namespace FlairsCards.MonoBehaviours
{
    class RoyaltyMono : ReversibleEffect
    {
        private Player player;
        private Gun gun; 
        private float totalPoints;
        private List<TeamScore> currentScore = new List<TeamScore>();
        public override void OnStart()
        {
            player = GetComponentInParent<Player>();
            gun = GetComponentInParent<Gun>();
            GameModeManager.AddHook(GameModeHooks.HookPointStart, PointStart); 
            //GameModeManager.AddHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        public override void OnOnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPointStart, PointStart); 
            //GameModeManager.RemoveHook(GameModeHooks.HookBattleStart, RoundEnd);
        }

        IEnumerator PointStart(IGameModeHandler gm)
        {
            this.ApplyModifiers();
            currentScore.Clear();
            currentScore = PlayerManager.instance.players.Select(p => p.teamID).Distinct().Select(ID => GameModeManager.CurrentHandler.GetTeamScore(ID)).ToList();
            totalPoints = 0;

            foreach (var score in currentScore)
            {
                totalPoints += score.rounds;
            }

            this.gunStatModifier.damage_add = 2f;
            this.ClearModifiers();

            yield break;
        }
    }
}