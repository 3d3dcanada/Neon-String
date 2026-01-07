using System;
using System.Collections.Generic;

namespace FilamentFrontier.DarkWebSim
{
    public enum HackNodeType
    {
        OpenSourceNode,
        MotherboardCore,
        CloudBanAlarm,
        Relay,
        Firewall,
        Decoy,
    }

    public enum HackState
    {
        Idle,
        InProgress,
        Success,
        Failed,
        Bricked,
    }

    public sealed class HackNode
    {
        public string Id { get; }
        public HackNodeType Type { get; }
        public bool IsConnected { get; private set; }
        public IReadOnlyList<string> Links { get; }

        public HackNode(string id, HackNodeType type, IReadOnlyList<string> links)
        {
            Id = id;
            Type = type;
            Links = links;
        }

        public void Connect() => IsConnected = true;
    }

    public sealed class FirmwareHackingMinigame
    {
        private readonly Dictionary<string, HackNode> _nodes = new();
        private readonly HashSet<string> _visited = new();
        private readonly Random _random;

        public HackState State { get; private set; }
        public int AlertsTriggered { get; private set; }
        public int MaxAlerts { get; }

        public FirmwareHackingMinigame(IEnumerable<HackNode> nodes, int maxAlerts = 2, int? seed = null)
        {
            foreach (var node in nodes)
            {
                _nodes[node.Id] = node;
            }

            MaxAlerts = Math.Max(1, maxAlerts);
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
            State = HackState.Idle;
        }

        public void StartHack()
        {
            _visited.Clear();
            AlertsTriggered = 0;
            State = HackState.InProgress;
        }

        public bool ConnectNodes(string fromId, string toId)
        {
            if (State != HackState.InProgress)
            {
                return false;
            }

            if (!_nodes.TryGetValue(fromId, out var fromNode) || !_nodes.TryGetValue(toId, out var toNode))
            {
                return false;
            }

            if (!fromNode.Links.Contains(toId))
            {
                return false;
            }

            _visited.Add(fromId);
            _visited.Add(toId);
            toNode.Connect();

            if (toNode.Type == HackNodeType.CloudBanAlarm)
            {
                AlertsTriggered++;
                if (AlertsTriggered >= MaxAlerts)
                {
                    State = HackState.Failed;
                    return false;
                }
            }

            if (toNode.Type == HackNodeType.Firewall)
            {
                var brickRoll = _random.NextDouble();
                if (brickRoll < 0.25)
                {
                    State = HackState.Bricked;
                    return false;
                }
            }

            if (toNode.Type == HackNodeType.MotherboardCore && HasPathFromOpenSource())
            {
                State = HackState.Success;
            }

            return true;
        }

        public bool HasPathFromOpenSource()
        {
            foreach (var node in _nodes.Values)
            {
                if (node.Type == HackNodeType.OpenSourceNode && _visited.Contains(node.Id))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
