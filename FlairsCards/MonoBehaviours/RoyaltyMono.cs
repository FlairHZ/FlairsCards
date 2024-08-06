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
        private CharacterData data;
        private Player player;
        private Gun gun;
        private WeaponHandler weaponHandler;
        private float totalPoints;
        private List<TeamScore> currentScore = new List<TeamScore>();
        public override void OnStart()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
            data = GetComponentInParent<CharacterData>();
            player = data.player;
            weaponHandler = data.weaponHandler;
            gun = weaponHandler.gun;
        }

        /*public void OnGameStart()
        {
            UnityEngine.GameObject.Destroy(this.gameObject);
        }*/

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
            totalPoints = 0;

            foreach (var score in currentScore)
            {
                totalPoints += score.points;
            }

            this.gun.damage = (float)(totalPoints * 2);
        }
    }
}
