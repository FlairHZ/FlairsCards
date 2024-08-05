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
    class SpeedsterClass : ClassHandler
    {
        internal static string name = "Speedster";

        public override IEnumerator Init()
        {
            while (!(Speedster.Card && Adrenaline.Card && EnergyDrink.Card && SupersonicCannon.Card && EnergyConverter.Card)) yield return null;
            ClassesRegistry.Register(Speedster.Card, CardType.Entry);
            ClassesRegistry.Register(Adrenaline.Card, CardType.Card, Speedster.Card);
            ClassesRegistry.Register(EnergyDrink.Card, CardType.Gate, Speedster.Card);
            ClassesRegistry.Register(SupersonicCannon.Card, CardType.Gate, Speedster.Card);
            ClassesRegistry.Register(EnergyConverter.Card, CardType.SubClass, new CardInfo[] { EnergyDrink.Card, SupersonicCannon.Card });
        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}