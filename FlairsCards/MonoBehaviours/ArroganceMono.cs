using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlairsCards.MonoBehaviours
{
    class ArroganceMono : MonoBehaviour
    {
        private Player player;
        private Gun gun; 
        private float totalPoints;
        private List<TeamScore> currentScore = new List<TeamScore>();
        private float didLose = 0;
        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
            gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
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
            didLose = 0;
            yield break;
        }

        IEnumerator PointEnd(IGameModeHandler gm)
        {
            currentScore.Clear();
            currentScore = PlayerManager.instance.players.Select(p => p.teamID).Distinct().Select(ID => GameModeManager.CurrentHandler.GetTeamScore(ID)).ToList();
            totalPoints = 0;

            foreach (var score in currentScore)
            {
                totalPoints += score.points;
            }


            if (totalPoints != didLose)
            {
                gun.damage += 0.05f;
                player.data.stats.health += 0.05f;
                player.data.stats.movementSpeed += 0.05f;
            }
            else if (totalPoints == 0)
            {
                gun.damage -= 0.075f;
                player.data.stats.health -= 0.075f;
                player.data.stats.movementSpeed -= 0.075f;
            }
            else
            {
                gun.damage -= 0.075f;
                player.data.stats.health -= 0.075f;
                player.data.stats.movementSpeed -= 0.075f;
            }

            didLose = totalPoints;

            yield break;
        }
    }
}