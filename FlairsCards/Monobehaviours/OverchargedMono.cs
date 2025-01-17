using FC.Extensions;
using System;
using UnboundLib.GameModes;
using UnityEngine;

namespace FlairsCards.MonoBehaviours
{
    class OverchargedMono : MonoBehaviour
    {
        private Player player;
        private Block block;
        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
            block = player.GetComponent<Block>();
        }
        void Update()
        {
            block.cdMultiplier = (float)(0.5 * (0.5 + Math.Pow(player.data.HealthPercentage - 0.2, 2) / 0.6534));
            player.data.stats.GetAdditionalData().overCharged = true; // Blanket solution, make it actually better later
        }
    }
}