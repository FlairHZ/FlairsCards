using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using FlairsCards.Cards;
using HarmonyLib;
using RarityLib.Utils;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using WillsWackyManagers.Utils;
using UnityEngine;


namespace FlairsCards
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class FlairsCards : BaseUnityPlugin
    {
        private const string ModId = "com.My.Mod.Id";
        private const string ModName = "Flairs Cards";
        public const string Version = "1.0.0"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "FC";
        public static FlairsCards instance { get; private set; }

        void Awake()
        {
            RarityUtils.AddRarity("Legendary", 0.025f, new Color(1, 1, 0), new Color(0.7f, 0.7f, 0));
            RarityUtils.AddRarity("Masterwork", 0.01f, new Color(0.17f, 0.97f, 1), new Color(0.11f, 0.71f, 0.73f));
            new Harmony(ModId).PatchAll();
        }
        void Start()
        {
            CustomCard.BuildCard<AllOrNothing>();
            CustomCard.BuildCard<AntiBlock>();
            CustomCard.BuildCard<AntiGravity>();
            CustomCard.BuildCard<BattleAngel>();
            CustomCard.BuildCard<Cannonball>();
            CustomCard.BuildCard<CripplingBullets>();
            CustomCard.BuildCard<CursedDraw>();
            CustomCard.BuildCard<EvasiveManuvers>();
            CustomCard.BuildCard<ExtendedMag>();
            CustomCard.BuildCard<HeavenlyDraw>();
            CustomCard.BuildCard<ImmovableWall>();
            CustomCard.BuildCard<LevitatingBullets>();
            CustomCard.BuildCard<PrecisionShot>();
            CustomCard.BuildCard<RapidBlock>();
            CustomCard.BuildCard<SwiftKicks>();
            CustomCard.BuildCard<TankShredder>();
            CustomCard.BuildCard<Weightless>();

            CustomCard.BuildCard<BlindingSpeed>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<BucklingPressure>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });

            instance = this;
        }
    }
}