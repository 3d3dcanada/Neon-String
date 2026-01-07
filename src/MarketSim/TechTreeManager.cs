using System;
using System.Collections.Generic;

namespace FilamentFrontier.MarketSim
{
    public enum TechNode
    {
        BlueTape,
        GlueStick,
        ManualLeveling,
        BugTouch,
        SquidNet,
        Blueberry,
        ConveyorBelts,
        RoboticArms,
        RecyclingExtruder,
        ClosedSourceEthics,
        PandaLabsPrinters,
        ThreeD3DProtocol,
        DistributedManufacturing,
    }

    public enum TechTier
    {
        Bedroom = 1,
        Garage = 2,
        Farm = 3,
        Shadowrun = 4,
    }

    public sealed class TechNodeData
    {
        public TechNode Node { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public TechTier Tier { get; }
        public IReadOnlyList<TechNode> Dependencies { get; }

        public TechNodeData(
            TechNode node,
            string displayName,
            string description,
            TechTier tier,
            IReadOnlyList<TechNode> dependencies)
        {
            Node = node;
            DisplayName = displayName;
            Description = description;
            Tier = tier;
            Dependencies = dependencies;
        }
    }

    public sealed class TechTreeManager
    {
        private readonly HashSet<TechNode> _unlocked = new();

        public IReadOnlyDictionary<TechNode, TechNodeData> Nodes { get; }

        public TechTreeManager()
        {
            Nodes = BuildNodes();
            Unlock(TechNode.BlueTape);
            Unlock(TechNode.GlueStick);
            Unlock(TechNode.ManualLeveling);
        }

        public bool IsUnlocked(TechNode node) => _unlocked.Contains(node);

        public bool CanUnlock(TechNode node)
        {
            if (!Nodes.TryGetValue(node, out var data))
            {
                return false;
            }

            foreach (var dependency in data.Dependencies)
            {
                if (!_unlocked.Contains(dependency))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Unlock(TechNode node)
        {
            if (!CanUnlock(node))
            {
                return false;
            }

            return _unlocked.Add(node);
        }

        public TechTier GetHighestUnlockedTier()
        {
            var highest = TechTier.Bedroom;
            foreach (var unlocked in _unlocked)
            {
                if (Nodes.TryGetValue(unlocked, out var data) && data.Tier > highest)
                {
                    highest = data.Tier;
                }
            }

            return highest;
        }

        public IReadOnlyList<TechNodeData> GetAvailableUnlocks()
        {
            var available = new List<TechNodeData>();
            foreach (var entry in Nodes)
            {
                if (_unlocked.Contains(entry.Key))
                {
                    continue;
                }

                if (CanUnlock(entry.Key))
                {
                    available.Add(entry.Value);
                }
            }

            return available;
        }

        private static IReadOnlyDictionary<TechNode, TechNodeData> BuildNodes()
        {
            return new Dictionary<TechNode, TechNodeData>
            {
                {
                    TechNode.BlueTape,
                    new TechNodeData(
                        TechNode.BlueTape,
                        "Blue Tape",
                        "When the bed hates you, tape becomes law.",
                        TechTier.Bedroom,
                        Array.Empty<TechNode>())
                },
                {
                    TechNode.GlueStick,
                    new TechNodeData(
                        TechNode.GlueStick,
                        "Glue Stick",
                        "The sacred ritual of stick-before-print.",
                        TechTier.Bedroom,
                        Array.Empty<TechNode>())
                },
                {
                    TechNode.ManualLeveling,
                    new TechNodeData(
                        TechNode.ManualLeveling,
                        "Manual Leveling",
                        "You are the auto-leveling probe now.",
                        TechTier.Bedroom,
                        Array.Empty<TechNode>())
                },
                {
                    TechNode.BugTouch,
                    new TechNodeData(
                        TechNode.BugTouch,
                        "BugTouch Probe",
                        "Auto-leveling with the personality of a toaster.",
                        TechTier.Garage,
                        new List<TechNode> { TechNode.ManualLeveling })
                },
                {
                    TechNode.SquidNet,
                    new TechNodeData(
                        TechNode.SquidNet,
                        "SquidNet",
                        "Remote monitoring so you can watch spaghetti happen live.",
                        TechTier.Garage,
                        new List<TechNode> { TechNode.Blueberry })
                },
                {
                    TechNode.Blueberry,
                    new TechNodeData(
                        TechNode.Blueberry,
                        "Blueberry",
                        "Tiny computer, giant cable nest.",
                        TechTier.Garage,
                        Array.Empty<TechNode>())
                },
                {
                    TechNode.ConveyorBelts,
                    new TechNodeData(
                        TechNode.ConveyorBelts,
                        "Conveyor Belts",
                        "Prints move themselves so you can sleep occasionally.",
                        TechTier.Farm,
                        new List<TechNode> { TechNode.SquidNet })
                },
                {
                    TechNode.RoboticArms,
                    new TechNodeData(
                        TechNode.RoboticArms,
                        "Robotic Arms",
                        "Robots do the scraping so your knuckles stay intact.",
                        TechTier.Farm,
                        new List<TechNode> { TechNode.ConveyorBelts })
                },
                {
                    TechNode.RecyclingExtruder,
                    new TechNodeData(
                        TechNode.RecyclingExtruder,
                        "Recycling Extruder",
                        "Turn failed prints back into filament. Karma, but hot.",
                        TechTier.Farm,
                        new List<TechNode> { TechNode.RoboticArms })
                },
                {
                    TechNode.ClosedSourceEthics,
                    new TechNodeData(
                        TechNode.ClosedSourceEthics,
                        "Closed Source Ethics",
                        "Decide how much telemetry you're willing to feed the Corp.",
                        TechTier.Shadowrun,
                        new List<TechNode> { TechNode.RecyclingExtruder })
                },
                {
                    TechNode.PandaLabsPrinters,
                    new TechNodeData(
                        TechNode.PandaLabsPrinters,
                        "Panda Labs Printers",
                        "Locked firmware, unlocked speed. Choose your poison.",
                        TechTier.Shadowrun,
                        new List<TechNode> { TechNode.ClosedSourceEthics })
                },
                {
                    TechNode.ThreeD3DProtocol,
                    new TechNodeData(
                        TechNode.ThreeD3DProtocol,
                        "The 3D3D Protocol",
                        "Handshake with the underground and get on their radar.",
                        TechTier.Shadowrun,
                        new List<TechNode> { TechNode.PandaLabsPrinters })
                },
                {
                    TechNode.DistributedManufacturing,
                    new TechNodeData(
                        TechNode.DistributedManufacturing,
                        "Distributed Manufacturing",
                        "Sync 100 printers before the Corp sees the power spike.",
                        TechTier.Shadowrun,
                        new List<TechNode> { TechNode.ThreeD3DProtocol })
                },
            };
        }
    }
}
