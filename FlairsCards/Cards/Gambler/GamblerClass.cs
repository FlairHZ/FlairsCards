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
            while (!(Gambler.Card)) yield return null;
            ClassesRegistry.Register(Gambler.Card, CardType.Entry);
            ClassesRegistry.Register(Wildcard.Card, CardType.Card, Gambler.Card);


            ClassesRegistry.Register(Broke.Card, CardType.Card, Gambler.Card);
            ClassesRegistry.Register(Neutral.Card, CardType.Card, Gambler.Card);
            ClassesRegistry.Register(SmallBlind.Card, CardType.Card, Gambler.Card);
            ClassesRegistry.Register(BigBlind.Card, CardType.Card, Gambler.Card);
            ClassesRegistry.Register(Jackpot.Card, CardType.Card, Gambler.Card);
            ClassesRegistry.Register(_777.Card, CardType.Card, Gambler.Card);
        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}