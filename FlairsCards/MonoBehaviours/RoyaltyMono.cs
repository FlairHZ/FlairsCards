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
    class RoyaltyMono : MonoBehaviour
    {
        private Player player;
        private Gun gun; 
        private float totalPoints;
        private List<TeamScore> currentScore = new List<TeamScore>();
        private void Start()
        {
            player = GetComponentInParent<Player>();
            gun = GetComponentInParent<Gun>();
            GameModeManager.AddHook(GameModeHooks.HookBattleStart, BattleStart);
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookBattleStart, BattleStart);
        }

        IEnumerator BattleStart(IGameModeHandler gm)
        {
            currentScore.Clear();
            currentScore = PlayerManager.instance.players.Select(p => p.teamID).Distinct().Select(ID => GameModeManager.CurrentHandler.GetTeamScore(ID)).ToList();
            totalPoints = 0;

            foreach (var score in currentScore)
            {
                totalPoints += score.points;
            }

            gun.damage = (float)(1 + (totalPoints * 0.1));

            yield break;
        }
    }
}