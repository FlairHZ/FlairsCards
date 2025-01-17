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
        private void Start()
        {
            player = GetComponent<Player>();
            player.data.stats.WasDealtDamageAction += OnDamage;
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, PointEnd);
        }
        private void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPointEnd, PointEnd);
            player.data.stats.WasDealtDamageAction -= OnDamage;
        }
        IEnumerator PointEnd(IGameModeHandler gm)
        {
            if (effectCoroutine != null)
            {
                StopCoroutine(effectCoroutine);
                effectCoroutine = null;
                isActive = false;
                player.data.stats.movementSpeed -= 0.5f;
            }

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
            player.data.stats.movementSpeed += 0.5f;
            if (player.data.health >= 0f)
            {
                player.data.health = Mathf.Min(player.data.health + Mathf.Round((player.data.maxHealth / (20/3))), player.data.maxHealth);
            }

            try
            {
                yield return new WaitForSeconds(1);
            }
            finally
            {
                player.data.stats.movementSpeed -= 0.5f;
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
