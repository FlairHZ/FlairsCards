using System.Collections;
using System.Linq;
using UnboundLib.GameModes;
using UnityEngine;

namespace FlairsCards.MonoBehaviours
{
    class SelfSacrificeMono : MonoBehaviour
    {
        private Player player;
        private Block block;
        private GeneralInput input;

        private void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
            block = player.GetComponent<Block>(); 
            input = player.GetComponent<GeneralInput>();
        }

        void Update()
        {
            if (block.IsOnCD() && player.data.HealthPercentage > 0.2 && input.shieldWasPressed)
            {
                player.data.health -= player.data.maxHealth / 5;
                block.RPCA_DoBlock(true);
            }
        }
    }
}
