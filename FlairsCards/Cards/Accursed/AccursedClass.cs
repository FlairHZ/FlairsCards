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
    class AccursedClass : ClassHandler
    {
        internal static string name = "Accursed";

        public override IEnumerator Init()
        {
            while (!(Accursed.Card && UnluckySouls.Card && CursedDraw.Card && UnholyCurse.Card)) yield return null;
            ClassesRegistry.Register(Accursed.Card, CardType.Entry);
            ClassesRegistry.Register(CursedDraw.Card, CardType.Card, Accursed.Card);
            ClassesRegistry.Register(UnholyCurse.Card, CardType.Card, Accursed.Card);
            ClassesRegistry.Register(UnluckySouls.Card, CardType.Gate, Accursed.Card);
            ClassesRegistry.Register(Sadist.Card, CardType.Gate, Accursed.Card);
        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}