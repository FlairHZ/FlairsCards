using System.Collections.Generic;
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
        private float totalRounds = 0;
        private List<TeamScore> currentScore = new List<TeamScore>();
        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
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
            gun.damage += (float)((totalRounds - 1) * 0.02);

            totalRounds--;

            yield break;
        }
    }
}