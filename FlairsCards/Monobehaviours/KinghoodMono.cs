using FC.Extensions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnboundLib.GameModes;
using UnityEngine;

namespace FlairsCards.MonoBehaviours
{
    class KinghoodMono : MonoBehaviour
    {
        private Player player;
        private Gun gun;
        private float totalRounds;
        private bool damageAdded = false;
        private List<TeamScore> currentScore = new List<TeamScore>();
        private void Start()
        {
            player = GetComponent<Player>();
            gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookRoundEnd, RoundEnd);
        }

        IEnumerator RoundEnd(IGameModeHandler gm)
        {
            currentScore.Clear();
            currentScore = PlayerManager.instance.players.Select(player => player.teamID).Distinct().Select(ID => GameModeManager.CurrentHandler.GetTeamScore(ID)).ToList();

            foreach (var score in currentScore)
            {
                totalRounds += score.rounds;
            }

            if (totalRounds >= 3 && damageAdded == false)
            {
                gun.damage += 1f;
                damageAdded = true;
            }

            totalRounds = 0;

            yield break;
        }
    }
}