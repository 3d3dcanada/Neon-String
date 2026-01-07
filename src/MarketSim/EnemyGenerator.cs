using System;
using System.Collections.Generic;

namespace FilamentFrontier.MarketSim
{
    public enum EnemyType
    {
        Karen,
        TemuBot,
        Scalper,
        KickstarterBacker,
        Reseller,
    }

    public sealed class EnemyProfile
    {
        public EnemyType Type { get; }
        public string DisplayName { get; }
        public IReadOnlyList<string> Attacks { get; }
        public IReadOnlyList<string> Grievances { get; }
        public string PassiveAbility { get; }

        public EnemyProfile(
            EnemyType type,
            string displayName,
            IReadOnlyList<string> attacks,
            IReadOnlyList<string> grievances,
            string passiveAbility)
        {
            Type = type;
            DisplayName = displayName;
            Attacks = attacks;
            Grievances = grievances;
            PassiveAbility = passiveAbility;
        }
    }

    public sealed class EnemyGenerator
    {
        private static readonly List<string> KarenGrievances = new()
        {
            "QA roulette: bed arrives warped, support says it's 'within spec'.",
            "Fanboy discourse: you didn't praise the brand enough, so you must be wrong.",
            "Cloud privacy panic: your printer reports your sleep schedule.",
            "Knockoff flood: your design shows up cheaper before you finish the listing.",
            "Spaghetti prints: you woke up to pasta again.",
            "Blob of death: the nozzle became modern art.",
            "Elephant's foot: first layer cosplayed as a pancake.",
            "Filament shattering: yesterday's spool is now confetti.",
            "Mystery layer shift: the belts voted for chaos.",
            "Support scars: post-processing looks like a crime scene.",
        };

        private static readonly IReadOnlyList<string> KarenAttacks = new List<string>
        {
            "I want a refund",
            "Let me speak to the manager",
            "It looks like plastic",
            "I found a cheaper clone",
        };

        private static readonly IReadOnlyList<string> TemuBotAttacks = new List<string>
        {
            "Clone Product",
            "Flash Sale Swarm",
            "Keyword Stuffing",
        };

        private static readonly IReadOnlyList<string> ScalperAttacks = new List<string>
        {
            "Inventory Wipe",
            "Bulk Buyout",
            "Price Gouge",
        };

        private static readonly IReadOnlyList<string> KickstarterBackerAttacks = new List<string>
        {
            "Where is my stretch goal?",
            "Chargeback",
            "Backer Update Rage",
        };

        private static readonly IReadOnlyList<string> ResellerAttacks = new List<string>
        {
            "STL Extraction",
            "Watermark Removal",
            "Race to the Bottom",
        };

        public EnemyProfile GenerateEnemy(EnemyType type, Random? random = null)
        {
            random ??= new Random();

            return type switch
            {
                EnemyType.Karen => new EnemyProfile(
                    type,
                    "The Karen",
                    KarenAttacks,
                    SampleGrievances(random, count: 4),
                    "Entitlement Shield"),
                EnemyType.TemuBot => new EnemyProfile(
                    type,
                    "Temu Bot",
                    TemuBotAttacks,
                    new List<string> { "Knockoff swarm detected.", "Listing undercut initiated." },
                    "Swarm Discount"),
                EnemyType.Scalper => new EnemyProfile(
                    type,
                    "The Scalper",
                    ScalperAttacks,
                    new List<string> { "Bought your entire stock in 0.2 seconds." },
                    "Market Lockdown"),
                EnemyType.KickstarterBacker => new EnemyProfile(
                    type,
                    "The Kickstarter Backer",
                    KickstarterBackerAttacks,
                    new List<string> { "Stretch goals were a promise, not a suggestion." },
                    "Backer Revolt"),
                EnemyType.Reseller => new EnemyProfile(
                    type,
                    "The Reseller",
                    ResellerAttacks,
                    new List<string> { "Your STL now has 400 listings and zero credit." },
                    "File Theft"),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown enemy type."),
            };
        }

        private static IReadOnlyList<string> SampleGrievances(Random random, int count)
        {
            var chosen = new List<string>();
            var available = new List<string>(KarenGrievances);

            for (var i = 0; i < count && available.Count > 0; i++)
            {
                var index = random.Next(available.Count);
                chosen.Add(available[index]);
                available.RemoveAt(index);
            }

            return chosen;
        }
    }
}
