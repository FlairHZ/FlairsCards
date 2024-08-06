﻿using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using FlairsCards.Cards;

namespace FlairsCards.Cards
{
    class RoyaltyClass : ClassHandler
    {
        internal static string name = "Survivalist";

        public override IEnumerator Init()
        {
            while (!(Royalty.Card)) yield return null;
            ClassesRegistry.Register(Royalty.Card, CardType.Entry);
        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}