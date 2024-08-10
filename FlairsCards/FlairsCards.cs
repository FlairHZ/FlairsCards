using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using FlairsCards.Cards;
using HarmonyLib;
using RarityLib.Utils;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using WillsWackyManagers.Utils;
using UnityEngine;
using ClassesManagerReborn.Util;
using ClassesManagerReborn;

namespace FlairsCards
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class FlairsCards : BaseUnityPlugin
    {
        private const string ModId = "com.My.Mod.Id";
        private const string ModName = "Flairs Cards";
        public const string Version = "1.0.0"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "FC";
        public const string CursedModInitials = "FC Curse";

        public static FlairsCards instance { get; private set; }

        void Awake()
        {
            RarityUtils.AddRarity("Legendary", 0.025f, new Color(1, 1, 0), new Color(0.7f, 0.7f, 0));
            RarityUtils.AddRarity("Masterwork", 0.01f, new Color(0.17f, 0.97f, 1), new Color(0.11f, 0.71f, 0.73f));
            RarityUtils.AddRarity("Unobtainable", 0.0000001f, new Color(0.17f, 0.97f, 1), new Color(0.11f, 0.71f, 0.73f));
            new Harmony(ModId).PatchAll();
        }
        void Start()
        {
            CustomCard.BuildCard<AllOrNothing>();
            CustomCard.BuildCard<AntiBlock>();
            CustomCard.BuildCard<AntiGravity>();
            CustomCard.BuildCard<Cannonball>();
            CustomCard.BuildCard<CripplingBullets>();
            CustomCard.BuildCard<EvasiveManuvers>();
            CustomCard.BuildCard<ExtendedMag>();
            CustomCard.BuildCard<HeavenlyDraw>();
            CustomCard.BuildCard<ImmovableWall>();
            CustomCard.BuildCard<LevitatingBullets>();
            CustomCard.BuildCard<PrecisionShot>();
            CustomCard.BuildCard<RapidBlock>();
            CustomCard.BuildCard<TankShredder>();
            CustomCard.BuildCard<Trickshot>();
            CustomCard.BuildCard<Weightless>();

            //Accursed Class
            CustomCard.BuildCard<Accursed>((card) => Accursed.Card = card);
            CustomCard.BuildCard<CursedDraw>((card) => CursedDraw.Card = card);
            CustomCard.BuildCard<FallenAngel>((card) => FallenAngel.Card = card);
            CustomCard.BuildCard<Plaguebearer>((card) => Plaguebearer.Card = card);
            CustomCard.BuildCard<Sadistic>((card) => Sadistic.Card = card);
            CustomCard.BuildCard<UnholyCurse>((card) => UnholyCurse.Card = card);
            CustomCard.BuildCard<UnluckySouls>((card) => UnluckySouls.Card = card);

            //Gambler Class
            CustomCard.BuildCard<Coinflip>((card) => Coinflip.Card = card); 
            CustomCard.BuildCard<Gambler>((card) => Gambler.Card = card); 
            CustomCard.BuildCard<LuckyBuff>((card) => LuckyBuff.Card = card);
            CustomCard.BuildCard<NaturalLuck>((card) => NaturalLuck.Card = card);
            CustomCard.BuildCard<Wildcard>((card) => Wildcard.Card = card);
            CustomCard.BuildCard<Neutral>((card) => Neutral.Card = card); 

            //Royalty Class
            CustomCard.BuildCard<Royalty>((card) => Royalty.Card = card); 
            CustomCard.BuildCard<Arrogance>((card) => Arrogance.Card = card);
            CustomCard.BuildCard<PersonalBodyguard>((card) => PersonalBodyguard.Card = card);

            //Speedster Class
            CustomCard.BuildCard<Adrenaline>((card) => Adrenaline.Card = card); 
            CustomCard.BuildCard<CantTouchThis>((card) => CantTouchThis.Card = card);
            CustomCard.BuildCard<EnergyConverter>((card) => EnergyConverter.Card = card);
            CustomCard.BuildCard<EnergyDrink>((card) => EnergyDrink.Card = card); 
            CustomCard.BuildCard<Speedster>((card) => Speedster.Card = card);
            CustomCard.BuildCard<SupersonicCannon>((card) => SupersonicCannon.Card = card);

            //Curse Cards
            CustomCard.BuildCard<BattleScarred>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<BlindingSpeed>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<BucklingPressure>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<ClumsyFingers>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<Diseased>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<FracturedShield>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<RandomDebuff>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<WeakenedWill>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            instance = this;

        }
    }
}