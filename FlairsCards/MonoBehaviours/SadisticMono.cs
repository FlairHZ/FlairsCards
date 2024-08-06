using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;
using ModdingUtils.GameModes;
using static ModdingUtils.Utils.SortingController;

namespace FlairsCards.MonoBehaviours
{
    class SadisticMono : ReversibleEffect, IRoundStartHookHandler, IRoundEndHookHandler, IGameStartHookHandler
    {
        public override void OnStart()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
            applyImmediately = false;
        }

        public override void OnOnDestroy()
        {
            InterfaceGameModeHooksManager.instance.RemoveHooks(this);
        }

        public void OnRoundStart()
        {
            this.ClearModifiers();
            UpdateMultipliers();
            this.ApplyModifiers();
        }
        public void OnRoundEnd()
        {
            this.ClearModifiers();
        }

        public void OnGameStart()
        {
            UnityEngine.GameObject.Destroy(this);
        }

        private void UpdateMultipliers()
        {
            this.gunStatModifier.damage_add = (float)(player.data.stats.GetAdditionalData().curses * 0.2);
            this.characterStatModifiersModifier.movementSpeed_add = (float)(player.data.stats.GetAdditionalData().curses * 0.2);
        }
    }
}