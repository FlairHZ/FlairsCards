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
    class TempAccursedClass : ClassHandler
    {
        internal static string name = "Speedster";

        public override IEnumerator Init()
        {
            while (!(TempAccursed.Card && UnluckySouls.Card && CursedDraw.Card)) yield return null;
            ClassesRegistry.Register(TempAccursed.Card, CardType.Entry);
            ClassesRegistry.Register(CursedDraw.Card, CardType.Card, TempAccursed.Card);
            ClassesRegistry.Register(UnluckySouls.Card, CardType.Gate, TempAccursed.Card);
        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}