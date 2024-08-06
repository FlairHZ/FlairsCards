using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using FlairsCards.Cards;

namespace FlairsCards.Cards
{
    class GamblerClass : ClassHandler
    {
        internal static string name = "Gambler";

        public override IEnumerator Init()
        {
            //while (!()) yield return null;
            yield return null;
        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}