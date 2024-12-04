using ModsPlus;

// I would remove this card so the card pack is only classes
// But my friends really really love cannonball so it can stay

namespace FlairsCards.Cards
{
    public class Cannonball : SimpleCard
    {
        internal static CardInfo Card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Cannonball",
            Description = "",
            ModName = FlairsCards.ModInitials,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.DefensiveBlue,
            Art = FlairsCards.CardArtCannonball,
            Stats = new[]
                {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Projectile Size",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Projectile Speed",
                    amount = "-40%",
                    simepleAmount = CardInfoStat.SimpleAmount.smaller
                },
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectileSize = 1.4f;
            gun.damage = 1.4f;
            gun.projectileSpeed = 0.6f;
        }
    }
}