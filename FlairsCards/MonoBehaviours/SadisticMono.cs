using UnboundLib.GameModes;
using UnityEngine;
using System.Collections;
using ModdingUtils.MonoBehaviours;
using WillsWackyManagers.Utils;
using FC.Extensions;
using ModdingUtils.GameModes;
using static ModdingUtils.Utils.SortingController;
using ModdingUtils.Extensions;

/*namespace FlairsCards.MonoBehaviours
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
}*/

namespace FlairsCards.MonoBehaviours
{
    class SadisticMono : ReversibleEffect
    {
        public override void OnStart()
        {
            player = GetComponentInParent<Player>();
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        public override void OnOnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        IEnumerator PickEnd(IGameModeHandler gm)
        {
            /*ClearModifiers();
            gunStatModifier.damage_add = (float)(player.data.stats.GetAdditionalData().curses * 0.2);
            characterStatModifiersModifier.movementSpeed_add = (float)(player.data.stats.GetAdditionalData().curses * 0.2);
            ApplyModifiers();*/
            player.data.stats.movementSpeed += (player.data.stats.GetAdditionalData().curses);
            
            yield break;
        }
    }
}