using UnityEngine;
using UnboundLib.GameModes;
using System.Collections;
using ModdingUtils.GameModes;
using WillsWackyManagers.Utils;
using ModdingUtils;
using ModdingUtils.MonoBehaviours;
using System.Collections.Generic;
using System.Linq;

namespace FlairsCards.MonoBehaviours
{
    class RoyaltyMono : ReversibleEffect, IPointStartHookHandler, IPointEndHookHandler
    {
        private float totalPoints = 3;
        private List<TeamScore> currentScore = new List<TeamScore>();
        public void OnGameStart()
        {
            UnityEngine.GameObject.Destroy(this);
        }
        public override void OnStart()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
            applyImmediately = false;
            this.SetLivesToEffect(int.MaxValue);
        }

        public void OnPointStart()
        {
            this.ClearModifiers();
            UpdateEffects();
            this.ApplyModifiers();
        }
        public void OnPointEnd()
        {
            this.ClearModifiers();
        }
        public override void OnOnDestroy()
        {
            InterfaceGameModeHooksManager.instance.RemoveHooks(this);
        }
        private void UpdateEffects()
        {
            currentScore.Clear();
            currentScore = PlayerManager.instance.players.Select(p => p.teamID).Distinct().Select(ID => GameModeManager.CurrentHandler.GetTeamScore(ID)).ToList();

            foreach (var score in currentScore)
            {
                totalPoints += score.rounds;
            }

            this.gunStatModifier.damage_add = totalPoints;
        }
    }
}
