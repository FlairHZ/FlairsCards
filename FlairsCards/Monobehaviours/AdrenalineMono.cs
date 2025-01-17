using ModdingUtils.MonoBehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.GameModes;
using UnityEngine;
using static ModdingUtils.Utils.SortingController;

namespace FlairsCards.Monobehaviours
{
    class AdrenalineMono : MonoBehaviour
    {
        private Player player;
        private Coroutine effectCoroutine;
        private bool isActive = false;
        private float oldSpeed = 1.35f;
        private bool first = false;
        private void Start()
        {
            player = GetComponent<Player>();
            player.data.stats.WasDealtDamageAction += OnDamage;
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, PointEnd);
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);
        }
        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPointEnd, PointEnd);
            GameModeManager.RemoveHook(GameModeHooks.HookPickEnd, PickEnd);
            player.data.stats.WasDealtDamageAction -= OnDamage;
        }
        IEnumerator PointEnd(IGameModeHandler gm)
        {
            if (isActive)
            {
                isActive = false;
                if (player.data.stats.movementSpeed - 0.4f >= (oldSpeed - 0.1f))
                {
                    player.data.stats.movementSpeed -= 0.4f;
                }
            }
            if (effectCoroutine != null)
            {
                StopCoroutine(effectCoroutine);
                effectCoroutine = null;
            }
            yield break;
        }

        IEnumerator PickEnd(IGameModeHandler gm)
        {
            oldSpeed = player.data.stats.movementSpeed;
            yield break;
        }
        private void OnDamage(Vector2 damage, bool selfDamage)
        {
            if (!isActive)
            {
                effectCoroutine = StartCoroutine(RoundStartEffect());
            }
        }
        private IEnumerator RoundStartEffect()
        {
            isActive = true;
            player.data.stats.movementSpeed += 0.4f;
            if (player.data.health > 0f)
            {
                player.data.health = Mathf.Min(player.data.health + Mathf.Round((player.data.maxHealth / (20/3))), player.data.maxHealth);
            }

            try
            {
                yield return new WaitForSeconds(1);
            }
            finally
            {
                player.data.stats.movementSpeed -= 0.4f;
                player.data.health = Mathf.Max(player.data.health - Mathf.Round((player.data.maxHealth / (20/3))), 0f);
                if (player.data.health == 0f)
                {
                    player.data.health += 1f;
                }
                isActive = false;
                effectCoroutine = null;
            }
        }
    }
}
