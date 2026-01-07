using System;
using System.Collections.Generic;

namespace FilamentFrontier.MarketSim
{
    public enum SpoofBrand
    {
        Reality3,
        Prisma,
        PandaLabs,
        CrudeReality,
        JosefOrange,
        WishyWashy,
        Jungle,
        JunkVerse,
        PrintTable,
        Stuffz,
        ThreeD3DCollective,
    }

    public sealed class BrandProfile
    {
        public string DisplayName { get; }
        public string Trait { get; }
        public float ReliabilityModifier { get; }
        public float SpeedModifier { get; }
        public float CostMultiplier { get; }
        public float FireRisk { get; }
        public float CloudDependency { get; }
        public float ResaleValueMultiplier { get; }

        public BrandProfile(
            string displayName,
            string trait,
            float reliabilityModifier,
            float speedModifier,
            float costMultiplier,
            float fireRisk,
            float cloudDependency,
            float resaleValueMultiplier)
        {
            DisplayName = displayName;
            Trait = trait;
            ReliabilityModifier = reliabilityModifier;
            SpeedModifier = speedModifier;
            CostMultiplier = costMultiplier;
            FireRisk = fireRisk;
            CloudDependency = cloudDependency;
            ResaleValueMultiplier = resaleValueMultiplier;
        }
    }

    public static class BrandLore
    {
        public static readonly IReadOnlyDictionary<SpoofBrand, BrandProfile> Profiles =
            new Dictionary<SpoofBrand, BrandProfile>
            {
                {
                    SpoofBrand.Reality3,
                    new BrandProfile(
                        displayName: "Reality-3",
                        trait: "Thermal Runaway",
                        reliabilityModifier: 0.7f,
                        speedModifier: 0.9f,
                        costMultiplier: 0.6f,
                        fireRisk: 0.35f,
                        cloudDependency: 0.0f,
                        resaleValueMultiplier: 0.6f)
                },
                {
                    SpoofBrand.Prisma,
                    new BrandProfile(
                        displayName: "Prisma",
                        trait: "Cult Following",
                        reliabilityModifier: 1.3f,
                        speedModifier: 1.0f,
                        costMultiplier: 5.0f,
                        fireRisk: 0.05f,
                        cloudDependency: 0.0f,
                        resaleValueMultiplier: 2.0f)
                },
                {
                    SpoofBrand.PandaLabs,
                    new BrandProfile(
                        displayName: "Panda Labs",
                        trait: "Cloud Kill",
                        reliabilityModifier: 1.1f,
                        speedModifier: 1.6f,
                        costMultiplier: 2.5f,
                        fireRisk: 0.08f,
                        cloudDependency: 0.9f,
                        resaleValueMultiplier: 1.4f)
                },
                {
                    SpoofBrand.CrudeReality,
                    new BrandProfile(
                        displayName: "CrudeReality",
                        trait: "Budget Chaos",
                        reliabilityModifier: 0.6f,
                        speedModifier: 0.8f,
                        costMultiplier: 0.5f,
                        fireRisk: 0.25f,
                        cloudDependency: 0.0f,
                        resaleValueMultiplier: 0.5f)
                },
                {
                    SpoofBrand.JosefOrange,
                    new BrandProfile(
                        displayName: "Josef's Orange",
                        trait: "Gummy Bear Reliability",
                        reliabilityModifier: 1.4f,
                        speedModifier: 1.0f,
                        costMultiplier: 2.0f,
                        fireRisk: 0.03f,
                        cloudDependency: 0.0f,
                        resaleValueMultiplier: 1.6f)
                },
                {
                    SpoofBrand.WishyWashy,
                    new BrandProfile(
                        displayName: "WishyWashy",
                        trait: "Clone Flood",
                        reliabilityModifier: 0.8f,
                        speedModifier: 1.1f,
                        costMultiplier: 0.4f,
                        fireRisk: 0.15f,
                        cloudDependency: 0.1f,
                        resaleValueMultiplier: 0.4f)
                },
                {
                    SpoofBrand.Jungle,
                    new BrandProfile(
                        displayName: "The Jungle",
                        trait: "Fast Shipping, Wet Filament",
                        reliabilityModifier: 1.0f,
                        speedModifier: 1.0f,
                        costMultiplier: 1.1f,
                        fireRisk: 0.05f,
                        cloudDependency: 0.2f,
                        resaleValueMultiplier: 1.0f)
                },
                {
                    SpoofBrand.JunkVerse,
                    new BrandProfile(
                        displayName: "JunkVerse",
                        trait: "Broken STLs",
                        reliabilityModifier: 0.9f,
                        speedModifier: 1.0f,
                        costMultiplier: 0.8f,
                        fireRisk: 0.05f,
                        cloudDependency: 0.1f,
                        resaleValueMultiplier: 0.7f)
                },
                {
                    SpoofBrand.PrintTable,
                    new BrandProfile(
                        displayName: "PrintTable",
                        trait: "Verified Makers",
                        reliabilityModifier: 1.1f,
                        speedModifier: 1.0f,
                        costMultiplier: 1.2f,
                        fireRisk: 0.04f,
                        cloudDependency: 0.1f,
                        resaleValueMultiplier: 1.3f)
                },
                {
                    SpoofBrand.Stuffz,
                    new BrandProfile(
                        displayName: "Stuffz",
                        trait: "Algorithm Roulette",
                        reliabilityModifier: 0.95f,
                        speedModifier: 1.0f,
                        costMultiplier: 1.0f,
                        fireRisk: 0.06f,
                        cloudDependency: 0.2f,
                        resaleValueMultiplier: 0.9f)
                },
                {
                    SpoofBrand.ThreeD3DCollective,
                    new BrandProfile(
                        displayName: "The 3D3D Collective",
                        trait: "Shadowrun Fixers",
                        reliabilityModifier: 1.5f,
                        speedModifier: 1.3f,
                        costMultiplier: 3.0f,
                        fireRisk: 0.02f,
                        cloudDependency: 0.0f,
                        resaleValueMultiplier: 2.5f)
                },
            };
    }
}
